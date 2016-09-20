using System;
using System.Collections.Generic;
using Kevins.Examples.Common.Enums;
using Kevins.Examples.Common.Extensions;
using log4net;
using NHibernate;
using Sunrise.Radiology.Messenger.Database.Common;

namespace Kevins.Examples.Database.Common
{
    public class MessengerRepository : DatabaseRepository, IMessengerRepository
    {
        private static readonly ILog Log =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static readonly string examAliasName = "exam";
        public static readonly string examAlias = examAliasName + ".";

        public static IEnumerable<int> EnabledDeliveryMethodIds
        {
            get
            {
                var enabledDeliveryMethodIds = new List<int>();
                if ("EnableEmail".RequiredSetting().ToBoolean())
                {
                    enabledDeliveryMethodIds.Add(DeliveryMethod.Email.AsInt());
                }
                if ("EnableFax".RequiredSetting().ToBoolean())
                {
                    enabledDeliveryMethodIds.Add(DeliveryMethod.Fax.AsInt());
                }
                if ("EnablePrint".RequiredSetting().ToBoolean())
                {
                    enabledDeliveryMethodIds.Add(DeliveryMethod.Print.AsInt());
                }
                if ("EnableBatchPrint".RequiredSetting().ToBoolean())
                {
                    enabledDeliveryMethodIds.Add(DeliveryMethod.BatchPrint.AsInt());
                }
                if ("EnableServerRequest".RequiredSetting().ToBoolean())
                {
                    enabledDeliveryMethodIds.Add(DeliveryMethod.ServerRequest.AsInt());
                }
                if ("EnableEmailNotices".RequiredSetting().ToBoolean())
                {
                    enabledDeliveryMethodIds.Add(DeliveryMethod.ServerRequest.AsInt());
                }
                return enabledDeliveryMethodIds;
            }
        }


        public static bool IsDatabaseConnected()
        {
            ISession session = null;

            try
            {
                session = MessengerRepositorySessionFactory.Instance.OpenSession();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return false;
            }
            finally
            {
                if (session != null)
                {
                    session.Close();
                }
            }
        }


        public MessengerRepository(ISession session)
            : base(session)
        {
        }



        public IEnumerable<T> GetAll<T>(int rows, ICriteria searchCriteria)
        {
            searchCriteria.SetMaxResults(rows);
            return searchCriteria.List<T>();
        }


/*
 *** I don't leave in commented code. This is just to show you that I can do some what complicated SQL 
 * 
 * 
        public IEnumerable<DeliveryQueueEntry> GetJobsToSend(IEnumerable<int> enabledDeliveryMethods)
        {
            // Status = ToBeSent
            // DeliveryMethod in (enabledDeliveryMethods)
            // Query Includes the following Priority Cases
            //case 1: TimeToSend has a valid hour to send, QueueEntryDT is today,            h of QueueEntryDT <  TimeToSend, h of GetDate() >= TimeToSend
            //case 2: TimeToSend has a valid hour to send, QueueEntryDT is yesterday,        h of QueueEntryDT < TimeToSend OR h of GetDate() >= TimeToSend
            //case 3: TimeToSend has a valid hour to send, QueueEntryDT is before yesterday
            //case 4: TimeToSend is an empty string or contains 0
            //case 5: TimeToSend is 25 (to be sent ASAP)
            var queryString =
                string.Format(@"Select Top " + "MaxConcurrentDeliveryQueueEntries".RequiredSetting() + @" dq.* from DeliveryQueue dq 
	                Left Outer Join Exams e ON e.examid = dq.examid
	                Left Outer Join Priority p ON p.PriorityNo = e.PriorityNo
	                Where Status = :status
		                AND (DeliveryMethod in (:deliveryMethodIds))
		                AND (Locked is null or Locked=0)
                        AND ({0})
		                AND (
			                (COALESCE(TimeToSend, '25')<'25' AND DateDiff(dd, QueueEntryDT, GetDate())=0 AND DatePart(hh, QueueEntryDT) < TimeToSend AND DatePart(hh, GetDate()) >= TimeToSend)
			                OR (COALESCE(TimeToSend, '25')<'25' AND DateDiff(dd, QueueEntryDT, GetDate())=1 AND (DatePart(hh, QueueEntryDT) < TimeToSend OR DatePart(hh, GetDate()) >= TimeToSend))
			                OR (COALESCE(TimeToSend, '25')<'25' AND DateDiff(dd, QueueEntryDT, GetDate())>1)
			                OR CAST(ISNULL(TimeToSend,'0') AS INT)=0
			                OR CAST(ISNULL(TimeToSend,'25') AS INT)=25
		                )
	                Order By
	                CASE WHEN dq.DeliveryMethod=4000 THEN 0 WHEN p.DICOMPriorityCode='S' THEN 1 ELSE 2 END,
	                CASE WHEN dq.DeliveryMethod=4000 THEN '' ELSE COALESCE(TimeToSend, '25') END,
	                CASE WHEN dq.DeliveryMethod=4000 THEN '' ELSE Priority END DESC", GetEmailNoticeClause());
            var query = HibernateSession.CreateSQLQuery(queryString);
            query.SetInt32("status", DeliveryStatus.ToBeSent.AsInt());
            query.SetParameterList("deliveryMethodIds", enabledDeliveryMethods.ToList());
            query.AddEntity("DeliveryQueueEntry", typeof (DeliveryQueueEntry));
            return query.List<DeliveryQueueEntry>();
        }


        public IEnumerable<DeliveryQueueEntry> GetUnProcessedBatchPrintJobs()
        {
            var unProcessedBatchPrintJobs = GetJobCriteria(false)
                .Add(Restrictions.Eq("DeliveryMethod", DeliveryMethod.BatchPrint.AsInt()))
                .Add(
                    Restrictions.Not(Restrictions.In("Status",
                        new List<int>
                        {
                            DeliveryStatus.Sent.AsInt(),
                            DeliveryStatus.ToBeSent.AsInt(),
                            DeliveryStatus.Failed.AsInt()
                        }.ToArray())));
            return unProcessedBatchPrintJobs.List<DeliveryQueueEntry>();
        }


        public int LockJobsBeingProcessed(IEnumerable<DeliveryQueueEntry> jobs)
        {
            if (jobs.Any())
            {
                var updateCommand =
                    HibernateSession.CreateSQLQuery(
                        @"UPDATE DeliveryQueue Set Locked = 1, ProcessedBy = :processedBy WHERE Id in (:jobIds)");
                updateCommand.SetString("processedBy", AppSettingsExtensions.Hostname);
                updateCommand.SetParameterList("jobIds", jobs.GetIds());
                var numberOfRows = updateCommand.ExecuteUpdate();
                HibernateSession.Flush();
                return numberOfRows;
            }
            return 0;
        }

    */

        public int MarkJobsAsToBeSent(string ids)
        {
            if (string.IsNullOrEmpty(ids))
            {
                return 0;
            }
            var jobIds = ids.Split(',');
            var updateCommand =
                HibernateSession.CreateSQLQuery(
                    @"UPDATE DeliveryQueue Set FailureDetails = '', Locked = 0, ProcessedBy = '', UnsuccessfulAttempts = 0, Status = :status WHERE Id in (:jobIds)");
            updateCommand.SetInt32("status", DeliveryStatus.ToBeSent.AsInt());
            updateCommand.SetParameterList("jobIds", jobIds);
            var numberOfRows = updateCommand.ExecuteUpdate();
            HibernateSession.Flush();
            return numberOfRows;
        }


        /*
         * 
         *  *** I don't leave in commented code. This is just to show you that I can do some what complicated HSQL 
 * 
 * 
        public int UpdateStatusForJobs(IEnumerable<DeliveryQueueEntry> jobs)
        {
            var updateCommand =
                HibernateSession.CreateSQLQuery(@"UPDATE DeliveryQueue Set Status = :status WHERE Id in (:jobIds)");
            updateCommand.SetInt32("status", DeliveryStatus.ToBeSent.AsInt());
            updateCommand.SetParameterList("jobIds", jobs.GetIds());
            var numberOfRows = updateCommand.ExecuteUpdate();
            HibernateSession.Flush();
            return numberOfRows;
        }


        public DeliveryQueueEntry UpdateFailedJob(DeliveryQueueEntry job, bool locked, int attempts, string failure)
        {
            var date = DateTime.Now;
            var failureDetails = failure.Replace("'", "''");
            var uodateString = string.Format(@"UPDATE DeliveryQueue Set Status = {0}, " +
                                             @"FailureDetails = '{1}', " +
                                             @"SentDT = null, " +
                                             @"LastAttemptDT = :date, " +
                                             @"Locked = {2} ," +
                                             @"UnsuccessfulAttempts = {3} " +
                                             @" where Id = {4}",
                DeliveryStatus.Failed.AsInt(), failureDetails, (locked ? 1 : 0), attempts, job.Id);
            var updateCommand = HibernateSession.CreateSQLQuery(uodateString);
            updateCommand.SetDateTime("date", date);
            if (updateCommand.ExecuteUpdate() == 0)
            {
                Log.Error(string.Format("The job was {0} was not updated", job.Id));
            }
            HibernateSession.Flush();
            return Get<DeliveryQueueEntry>(job.Id);
        }


        public DeliveryQueueEntry UpdateSuccessfullJob(DeliveryQueueEntry job)
        {
            var date = DateTime.Now;
            var uodateString = string.Format(@"UPDATE DeliveryQueue Set Status = {0}, " +
                                             @"FailureDetails = '', " +
                                             @"SentDT = :date, " +
                                             @"LastAttemptDT = :date, " +
                                             @"Locked = 0 ," +
                                             @"UnsuccessfulAttempts = 0" +
                                             @" where Id = {1}",
                DeliveryStatus.Sent.AsInt(), job.Id);
            var updateCommand = HibernateSession.CreateSQLQuery(uodateString);
            updateCommand.SetDateTime("date", date);
            if (updateCommand.ExecuteUpdate() == 0)
            {
                Log.Error(string.Format("The job was {0} was not updated", job.Id));
            }
            HibernateSession.Flush();
            return Get<DeliveryQueueEntry>(job.Id);
        }


    */


        private string GetEmailNoticeClause()
        {
            return ("EnableEmailNotices".RequiredSetting().ToBoolean()
                ? "Id != 0"
                : string.Format("JobType != {0}", DeliveryQueueType.EmailNotice.AsInt()));
        }
    }
}

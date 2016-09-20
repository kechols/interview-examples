using Kevins.Examples.Database.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kevins.Examples.Database.Tests.Integration.Common
{
    [TestClass]
    public class MessengerRepositoryTest : HibernateTestBase
    {
        [TestMethod]
        public void ShouldGetDeliveryQueueEntriesToSend()
        {
            WithSession(session =>
            {
                var repository = new MessengerRepository(session);
                /*
                var entriesToSend = repository.GetJobsToSend(MessengerRepository.EnabledDeliveryMethodIds);

                if (!entriesToSend.Any())
                {
                    Assert.Fail("There were no Delivery Queue Entries To Be Sent detected in the database");
                }

                // Make sure that we only get back MaxConcurrentDeliveryQueueEntries to send
                Assert.IsTrue(entriesToSend.Count() <= "MaxConcurrentDeliveryQueueEntries".RequiredSetting().AsInt());

                // Testing with a sample size of 5 jobs from the database
                foreach (var entry in entriesToSend)
                {
                    // Assert Status is To Be Sent
                    Assert.AreEqual(DeliveryStatus.ToBeSent, entry.StatusEnum);
                    // Assert Not Locked
                    Assert.IsFalse(entry.Locked);
                }
                */

            });
        }

    }
}

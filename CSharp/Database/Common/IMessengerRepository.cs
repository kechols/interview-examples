using System.Collections.Generic;
using NHibernate;

namespace Kevins.Examples.Database.Common
{
    public interface IMessengerRepository : IDatabaseRepository
    {
       // void AddAliases(ICriteria searchCriteria);
       // ICriteria GetJobCriteria(bool useAliases);
       // IEnumerable<T> GetAll<T>(string sortColumn, string sortDirection, int page, int rows, ICriteria searchCriteria, out int totalCount);
        IEnumerable<T> GetAll<T>(int rows, ICriteria searchCriteria);
        int MarkJobsAsToBeSent(string ids);
    }
}

using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;

namespace Kevins.Examples.Database.Common
{
    public interface IDatabaseRepository
    {
        void SaveOrUpdate(object entity);
        void Delete(object entity);
        T Get<T>(int? id);
        T Get<T>(string propertyName, object value);
        T Get<T>(IDictionary<string, object> parameters);

        IEnumerable<T> Get<T>(string idsCommaSeparated);

        ICollection<T> GetLike<T>(string propertyName, object value);

        ICollection<T> GetLike<T>(string propertyName, string value, MatchMode whereToMatch);

        void Flush();

        void Clear();

        IEnumerable<T> GetAllCaseInsensitiveSort<T>(string sortColumn, string sortDirection);

        IEnumerable<T> GetAllMaxOf<T>(int numberOfRows);

        object GetMax<T>(string propertyName);

        int GetTotal<T>();

        int GetTotal<T>(IDictionary<string, object> parameters);

        ICollection<T> GetAll<T>();

        IEnumerable<T> GetAll<T>(string propertyName, object value);

        IEnumerable<T> GetAll<T>(string sortColumn, string sortDirection, string propertyName, object value);

        ICollection<T> GetAll<T>(IDictionary<string, object> parameters);

        IEnumerable<T> GetAll<T>(string sortColumn, string sortDirection, IDictionary<string, object> parameters);

        ICriteria GetCriteria<T>();

        T Detach<T>(T obj);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using NHibernate.Util;


namespace Kevins.Examples.Database.Common
{
    public class DatabaseRepository : IDatabaseRepository, IDisposable
    {
        protected Dictionary<string, int> ColumnsIndexes;
        public const string Ascending = "asc";
        public const string Descending = "desc";

        public DatabaseRepository(ISession session)
        {
            HibernateSession = session;
        }


        protected ISession HibernateSession { get; private set; }


        public virtual void SaveOrUpdate(object entity)
        {
            HibernateSession.SaveOrUpdate(entity);
            Flush();
        }


        public virtual void Save(object entity)
        {
            HibernateSession.Save(entity);
            Flush();
        }


        protected IList<object> GetIds(string query)
        {
            return HibernateSession.CreateSQLQuery(query).List<object>();
        }

        public void BeginTransaction()
        {
            HibernateSession.BeginTransaction();
        }


        public void Rollback()
        {
            HibernateSession.Transaction.Rollback();
        }


        public void Commit()
        {
            HibernateSession.Transaction.Commit();
        }


        public virtual void Delete(object entity)
        {
            HibernateSession.Delete(entity);
            Flush();
        }


        public void DeleteBy<T>(DetachedCriteria criteria)
        {
            var entities = FindAll<T>(criteria);
            entities.ForEach(e => Delete(e));
        }


        public ICollection<T> GetAll<T>(DetachedCriteria criteria)
        {
            return FindAll<T>(criteria).ToList();
        }


        public T Get<T>(DetachedCriteria criteria)
        {
            return FindOne<T>(criteria);
        }


        public T Get<T>(int? id)
        {
            return HibernateSession.Get<T>(id);
        }


        public IEnumerable<T> GetAll<T>(string propertyName, object value)
        {
            return GetSinglePropertyCriteria<T>(propertyName, value).List<T>();
        }


        public IEnumerable<T> GetAll<T, TReturn>(Expression<Func<T, TReturn>> expression, object value)
        {
            return GetAll<T>(NameOf.Property(expression), value);
        }


        public IEnumerable<TReturn> GetAllIn<TReturn>(string propertyName, IEnumerable<int> ids)
        {
            return GetAllIn<int, TReturn>(propertyName, ids);
        }


        public IEnumerable<TReturn> GetAllIn<TReturn>(string propertyName, IEnumerable<object> ids)
        {
            return GetAllIn<object, TReturn>(propertyName, ids);
        }

        public IEnumerable<TReturn> GetAllIn<T, TReturn>(string propertyName, IEnumerable<T> ids)
        {
            var result = new List<TReturn>();
            DbUtils.InBatches(ids, batchedIds =>
            {
                var criteria = HibernateSession.CreateCriteria(typeof(TReturn));
                criteria.Add(Restrictions.In(propertyName, batchedIds.ToArray()));
                criteria.SetResultTransformer(Transformers.DistinctRootEntity);
                result.AddRange(criteria.List<TReturn>());
            });
            return result;
        }


        public ICollection<T> GetAll<T>(IDictionary<string, object> parameters)
        {
            var criteria = HibernateSession.CreateCriteria(typeof(T));
            parameters.ForEach(p => criteria.Add(Restrictions.Eq(p.Key, p.Value)));
            return criteria.List<T>();
        }


        public IEnumerable<T> GetAll<T>(string sortColumn, string sortDirection, IDictionary<string, object> parameters)
        {
            var criteria = HibernateSession.CreateCriteria(typeof(T));
            parameters.ForEach(p => criteria.Add(Restrictions.Eq(p.Key, p.Value)));
            criteria.AddOrder(sortDirection == Ascending ? Order.Asc(sortColumn) : Order.Desc(sortColumn));
            return criteria.List<T>();
        }


        private ICriteria GetSinglePropertyCriteria<T>(string propertyName, object value)
        {
            var criteria = HibernateSession.CreateCriteria(typeof(T));
            criteria.Add(Restrictions.Eq(propertyName, value));
            return criteria;
        }


        public T Get<T>(string propertyName, object value)
        {
            return GetSinglePropertyCriteria<T>(propertyName, value).UniqueResult<T>();
        }


        public IEnumerable<T> Get<T>(string idsCommaSeparated)
        {
            var ids = idsCommaSeparated.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList().Select(int.Parse).ToArray();

            // NHibernate can't put more than 1000 records in a single "in" clause
            if (ids.Count() > 1000)
            {
                var firstThousand = Get<T>(String.Join(",", ids.Take(1000))).ToList();
                var restOfList = Get<T>(String.Join(",", ids.Skip(1000))).ToList();
                firstThousand.AddRange(restOfList);
                return firstThousand;
            }

            var criteria = HibernateSession.CreateCriteria(typeof(T));
            criteria.Add(Restrictions.In("Id", ids));
            return criteria.List<T>();
        }



        public ICollection<T> GetLike<T>(string propertyName, object value)
        {
            var criteria = HibernateSession.CreateCriteria(typeof(T));
            criteria.Add(Restrictions.Like(propertyName, value).IgnoreCase());
            return criteria.List<T>();
        }


        public ICollection<T> GetLike<T>(string propertyName, string value, MatchMode whereToMatch)
        {
            var criteria = HibernateSession.CreateCriteria(typeof(T));
            criteria.Add(new LikeExpression(propertyName, value, whereToMatch));
            return criteria.List<T>();
        }


        public T Get<T>(IDictionary<string, object> parameters)
        {
            var criteria = HibernateSession.CreateCriteria(typeof(T));
            parameters.ForEach(p => criteria.Add(Restrictions.Eq(p.Key, p.Value)));
            return criteria.UniqueResult<T>();
        }


        public IEnumerable<T> GetAllCaseInsensitiveSort<T>(string sortColumn, string sortDirection)
        {
            var criteria = HibernateSession.CreateCriteria(typeof(T));

            var projection = Projections.SqlFunction("lower",
                                         NHibernateUtil.String,
                                         Projections.Property(sortColumn));

            criteria.AddOrder(sortDirection == Ascending ? Order.Asc(projection) : Order.Desc(projection));
            return criteria.List<T>();
        }


        public IEnumerable<T> GetAll<T>(string sortColumn, string sortDirection, string propertyName, object value)
        {
            ICriteria criteria = GetSinglePropertyCriteria<T>(propertyName, value);
            criteria.AddOrder(sortDirection == Ascending ? Order.Asc(sortColumn) : Order.Desc(sortColumn));
            return criteria.List<T>();
        }


        public IEnumerable<T> GetAllMaxOf<T>(int numberOfObjects)
        {
            var criteria = HibernateSession.CreateCriteria(typeof(T));

            criteria.SetMaxResults(numberOfObjects);
            return criteria.List<T>();
        }


        public int GetTotal<T>()
        {
            var criteria = HibernateSession.CreateCriteria(typeof(T)).SetProjection(Projections.RowCount());
            return (int)criteria.UniqueResult();
        }


        public int GetTotal<T>(IDictionary<string, object> parameters)
        {
            var criteria = HibernateSession.CreateCriteria(typeof(T)).SetProjection(Projections.RowCount());
            parameters.ForEach(p => criteria.Add(Restrictions.Eq(p.Key, p.Value)));
            return (int)criteria.UniqueResult();
        }


        public object GetMax<T>(string propertyName)
        {
            var criteria = HibernateSession.CreateCriteria(typeof(T)).SetProjection(Projections.Max(propertyName));
            return criteria.UniqueResult();
        }


        public object GetMaxStartingLike<T>(string propertyName, string likeValue)
        {
            return HibernateSession.CreateCriteria(typeof(T))
                        .Add(Restrictions.Like(propertyName, likeValue, MatchMode.Start))
                        .SetProjection(Projections.Max(propertyName))
                        .UniqueResult();
        }


        public ICollection<T> GetAll<T>()
        {
            var criteria = HibernateSession.CreateCriteria(typeof(T));
            return criteria.List<T>();
        }


        public IEnumerable<T> FindAll<T>(DetachedCriteria criteria)
        {
            return criteria.GetExecutableCriteria(HibernateSession).List<T>();
        }


        public T FindOne<T>(DetachedCriteria criteria)
        {
            return criteria.GetExecutableCriteria(HibernateSession).UniqueResult<T>();
        }


        public void Flush()
        {
            HibernateSession.Flush();
        }


        public void Clear()
        {
            HibernateSession.Clear();
        }


        public T Detach<T>(T obj)
        {
            HibernateSession.Evict(obj);
            return obj;
        }


        public void Dispose()
        {
            if (HibernateSession.IsOpen)
                HibernateSession.Close();
            HibernateSession.Dispose();
        }


        public ICriteria GetCriteria<T>()
        {
            return HibernateSession.CreateCriteria(typeof(T));
        }
    }
}

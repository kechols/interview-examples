using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Kevins.Examples.Database.Common;
using NHibernate;
using NHibernate.Criterion;
using Sunrise.Radiology.Messenger.Database;

namespace Kevins.Examples.Database.Tests.Integration.Common
{
    public static class IntegrationTestHelper
    {
        public static T GetRandom<T>(ISession session, string idColumnName) where T : class, new()
        {
            // TODO: Figure out why I had to put the schema in below. I never had to do that before. Hmmm strange -- kte
            var query = session.CreateSQLQuery(@"Select " + idColumnName + " from " + MessengerRepositorySessionFactory.TableName<T>());
            var id = GetRandomFromList(query.List<int>());
            var repository = new DatabaseRepository(session);
            return repository.Get<T>(id);
        }


        public static T GetRandom<T>(DatabaseRepository repository, DetachedCriteria criteria)
        {
            var elements = repository.GetAll<T>(criteria).ToList();
            return GetRandomFromList(elements);
        }


        public static T GetRandomFromList<T>(IList<T> elements)
        {
            var range = (elements.Count - 1);
            var randomizer = new Random();
            var radomIndex = randomizer.Next(0, range);
            return elements.ElementAt(radomIndex);
        }


        public static Printer RandomPrinter(ISession session)
        {
            return GetRandom<Printer>(session, ColumnName<Printer>(o => o.Id));
        }


        public static BillMethod RandomBillMethod(ISession session)
        {
            return GetRandom<BillMethod>(session, ColumnName<BillMethod>(o => o.Id));
        }


        public static User RandomUser(ISession session)
        {
            return GetRandom<User>(session, ColumnName<User>(o => o.Id));
        }

        public static string ColumnName<T>(Expression<Func<T, object>> property)
        where T : class, new()
        {
            return MessengerRepositorySessionFactory.ColumnName(property);
        }
    }
}

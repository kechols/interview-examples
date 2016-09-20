using System;
using Kevins.Examples.Database.Common;
using NHibernate;

namespace Kevins.Examples.Database.Tests.Integration.Common
{
    public abstract class HibernateTestBase
    {
        public static void WithSession(Action<ISession> action)
        {
            InvokeAction(MessengerRepositorySessionFactory.Instance, action);
        }

        private static void InvokeAction(ISessionFactory sessionFactory, Action<ISession> action)
        {
            var session = sessionFactory.OpenSession();
            using (session)
            {
                action.Invoke(session);
            }
        }
    }
}

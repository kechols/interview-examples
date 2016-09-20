using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using FluentNHibernate.Cfg;
using Kevins.Examples.Common.Extensions;
using NHibernate;
using NHibernate.Mapping;
using Configuration = NHibernate.Cfg.Configuration;
using Environment = NHibernate.Cfg.Environment;

namespace Kevins.Examples.Database.Common
{
    public class MessengerRepositorySessionFactory
    {
        public static ISessionFactory Instance = CreateSessionFactory(Assembly.GetExecutingAssembly());

        public static Configuration Configuration = AddFluentMappings(GetConfiguration()).BuildConfiguration();

        public MessengerRepositorySessionFactory()
        {
            Instance = CreateSessionFactory(Assembly.GetExecutingAssembly());
        }

        public static ISessionFactory CreateSessionFactory(Assembly assembly)
        {
            Configuration cfg = GetConfiguration();
            FluentConfiguration fluentConfiguration = AddFluentMappings(cfg);
            return fluentConfiguration.BuildSessionFactory();
        }

        private static Configuration GetConfiguration()
        {
            var connectionString = String.Format("Database={0}; Server={1}; Password={2}; User ID={3}",
                                      "Database".RequiredSetting(), "Server".RequiredSetting(), "Password".Setting(), "User ID".RequiredSetting());
            return BuildConfiguration(connectionString);
        }

        private static FluentConfiguration AddFluentMappings(Configuration cfg)
        {
            FluentConfiguration fluentConfiguration = Fluently.Configure(cfg);
            fluentConfiguration.Mappings(x => x.FluentMappings.AddFromAssemblyOf<BillMethod>());
            return fluentConfiguration;
        }


        public static Configuration BuildConfiguration(string connectionString)
        {
            var configuration = new Configuration();
            configuration.SetProperty(Environment.ConnectionProvider, "NHibernate.Connection.DriverConnectionProvider");
            configuration.SetProperty(Environment.Dialect, "NHibernate.Dialect.MsSql2012Dialect");
            configuration.SetProperty(Environment.ConnectionString, connectionString);
            configuration.SetProperty(Environment.ReleaseConnections, "on_close");

            return configuration;
        }

        public static string TableName<T>() where T : class, new()
        {
            return Configuration.GetClassMapping(typeof(T)).Table.Name;
        }

        public static string ColumnName<T>(Expression<Func<T, object>> property)
        where T : class, new()
        {
            var accessor = FluentNHibernate.Utils.Reflection.ReflectionHelper.GetAccessor(property);

            var names = accessor.Name.Split('.');

            var classMapping = Configuration.GetClassMapping(typeof(T));
            return WalkPropertyChain(classMapping.GetProperty(names.First()), 0, names);
        }

        private static string WalkPropertyChain(Property property, int index, string[] names)
        {
            if (property.IsComposite)
            {
                return WalkPropertyChain(((Component)property.Value).GetProperty(names[++index]), index, names);
            }

            return property.ColumnIterator.First().Text;
        }

    }
}

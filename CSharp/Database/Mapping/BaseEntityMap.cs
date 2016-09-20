using FluentNHibernate.Mapping;
using Kevins.Examples.Common.Extensions;

namespace Kevins.Examples.Database.Mapping
{
    public abstract class BaseEntityMap<T> : ClassMap<T>
    {
        public BaseEntityMap()
        {
            Schema("Schema".RequiredSetting());

        }
    }
}

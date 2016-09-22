namespace Kevins.Examples.Database.Mapping
{
    public class UserPreferenceMap : BaseEntityMap<UserPreference>
    {
        public UserPreferenceMap()
        {
            Table("UserPrefs");

            Id(o => o.Id, "Id").Not.Nullable();
            Map(o => o.Key, "KeyName").Nullable();
            Map(o => o.Value, "KeyValue").Nullable();

            References(o => o.User, "User_No").Cascade.None().LazyLoad();
        }
    }
}

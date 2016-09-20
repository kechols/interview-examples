using Sunrise.Radiology.Messenger.Database;

namespace Kevins.Examples.Database
{
    public class UserPreference : BaseEntity<UserPreference>
    {
        public virtual User User { get; set; }
        public virtual string Key { get; set; }
        public virtual string Value { get; set; }
    }
}
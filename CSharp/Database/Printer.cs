using Kevins.Examples.Database;

namespace Sunrise.Radiology.Messenger.Database
{
    public class Printer : BaseEntity<Printer>
    {
        public virtual string PrinterNickname { get; set; }
        public virtual string PrinterAlias { get; set; }
        public virtual string BillingFacilityNumber { get; set; }
        public virtual string Notes { get; set; }
    }
}
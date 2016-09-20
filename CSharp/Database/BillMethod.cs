namespace Kevins.Examples.Database
{
    public class BillMethod : BaseEntity<BillMethod>
    {
        public virtual string Option { get; set; }
        public virtual string Notes { get; set; }
        public virtual int FeeScheduleToUse { get; set; }
        public virtual int PercentageOfFeeScheduleToApply { get; set; }
        public virtual string Formula { get; set; }
        public virtual string ExportCategory { get; set; }
        public virtual string Description { get; set; }
        public virtual string BillingCategory { get; set; }
        public virtual string FinancialClass { get; set; }
        public virtual string Modifier { get; set; }
        public virtual bool Retired { get; set; }
        public virtual bool IssueWarningWhenIcd9Empty { get; set; }
        public virtual bool ExportTechFees { get; set; }
        public virtual bool ExportSupplyFees { get; set; }
        public virtual bool ExportProfessionalFees { get; set; }
    }
}

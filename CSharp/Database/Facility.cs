namespace Kevins.Examples.Database
{
    public class Facility : BaseEntity<Facility>
    {
        public virtual string EnglishDescription { get; set; }
        public virtual string FrenchDescription { get; set; }
        public virtual string BillingFacilityNumber { get; set; }
        public virtual string Notes { get; set; }
        public virtual Printer Printer { get; set; }
        public virtual string LogoFileName { get; set; }
        public virtual string EmailFailedFaxNotifications { get; set; }
        public virtual string TextForAcs { get; set; }
        public virtual string Map { get; set; }
        public virtual int TimeZoneRelativeToServers { get; set; }
        public virtual bool Retired { get; set; }
        public virtual Printer MammoLetterPrinter { get; set; }
        public virtual bool ReportsForPhysician { get; set; }
        public virtual bool ExtraCopies { get; set; }
        public virtual bool ExtraCopiesNursingUnit { get; set; }
        public virtual bool FollowUpLetters { get; set; }
        public virtual bool Mammography { get; set; }
        public virtual int PrinterForPhysician { get; set; }
        public virtual int PrinterForExtraCopies { get; set; }
        public virtual int PrntrForXtraCopiesNursingUnit { get; set; }
        public virtual int PrinterForMammography { get; set; }
        public virtual int PrinterForFollowUpLetters { get; set; }
        public virtual string SortedBy { get; set; }
        public virtual string PhysicianTimeToPrint1 { get; set; }
        public virtual string PhysicianTimeToPrint2 { get; set; }
        public virtual string PhysicianTimeToPrint3 { get; set; }
        public virtual string ExtraCopiesTimeT0Print1 { get; set; }
        public virtual string ExtraCopiesTimeT0Print2 { get; set; }
        public virtual string ExtraCopiesTimeT0Print3 { get; set; }
        public virtual string ExtraCopiesNuTimeT0Print1 { get; set; }
        public virtual string ExtraCopiesNuTimeT0Print2 { get; set; }
        public virtual string ExtraCopiesNuTimeT0Print3 { get; set; }
        public virtual string MammographyTimeT0Print1 { get; set; }
        public virtual string MammographyTimeT0Print2 { get; set; }
        public virtual string MammographyTimeT0Print3 { get; set; }
        public virtual string FollowupLetterTimeT0Print1 { get; set; }
        public virtual string FollowupLetterTimeT0Print2 { get; set; }
        public virtual string FollowupLetterTimeT0Print3 { get; set; }
        public virtual bool NursingUnitPrinter { get; set; }
        public virtual string FollowUpLetterTemplate { get; set; }
        public virtual string ExternalId { get; set; }
    }
}

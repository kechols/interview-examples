namespace Kevins.Examples.Database.Mapping
{
    public class FacilityMap : BaseEntityMap<Facility>
    {
        public FacilityMap()
        {
            Table("Facility");

            Id(o => o.Id, "FacilityNo");
            Map(o => o.EnglishDescription, "Desc_E");
            Map(o => o.FrenchDescription, "Desc_F");
            Map(o => o.BillingFacilityNumber, "BillingFacilityNumber");
            Map(o => o.Notes, "Notes");
            Map(o => o.LogoFileName, "LogoFileName");
            Map(o => o.EmailFailedFaxNotifications, "EmailFailedFaxNotifications");
            Map(o => o.TextForAcs, "TextForACS");
            Map(o => o.Map, "Map");
            Map(o => o.TimeZoneRelativeToServers, "TimeZoneRelativeToServers");
            Map(o => o.Retired, "Retired");
            Map(o => o.ReportsForPhysician, "ReportsForPhysician");
            Map(o => o.ExtraCopies, "ExtraCopies");
            Map(o => o.ExtraCopiesNursingUnit, "ExtraCopiesNursingUnit");
            Map(o => o.FollowUpLetters, "FollowUpLetters");
            Map(o => o.Mammography, "Mammography");
            Map(o => o.PrinterForPhysician, "PrinterForPhysician");
            Map(o => o.PrinterForExtraCopies, "PrinterForExtraCopies");
            Map(o => o.PrntrForXtraCopiesNursingUnit, "PrntrForXtraCopiesNursingUnit");
            Map(o => o.PrinterForMammography, "PrinterForMammography");
            Map(o => o.PrinterForFollowUpLetters, "PrinterForFollowUpLetters");
            Map(o => o.SortedBy, "SortedBy");
            Map(o => o.PhysicianTimeToPrint1, "PhysicianTimeToPrint1");
            Map(o => o.PhysicianTimeToPrint2, "PhysicianTimeToPrint2");
            Map(o => o.PhysicianTimeToPrint3, "PhysicianTimeToPrint3");
            Map(o => o.ExtraCopiesTimeT0Print1, "ExtraCopiesTimeT0Print1");
            Map(o => o.ExtraCopiesTimeT0Print2, "ExtraCopiesTimeT0Print2");
            Map(o => o.ExtraCopiesTimeT0Print3, "ExtraCopiesTimeT0Print3");
            Map(o => o.ExtraCopiesNuTimeT0Print1, "ExtraCopiesNUTimeT0Print1");
            Map(o => o.ExtraCopiesNuTimeT0Print2, "ExtraCopiesNUTimeT0Print2");
            Map(o => o.ExtraCopiesNuTimeT0Print3, "ExtraCopiesNUTimeT0Print3");
            Map(o => o.MammographyTimeT0Print1, "MammographyTimeT0Print1");
            Map(o => o.MammographyTimeT0Print2, "MammographyTimeT0Print2");
            Map(o => o.MammographyTimeT0Print3, "MammographyTimeT0Print3");
            Map(o => o.FollowupLetterTimeT0Print1, "FollowupLetterTimeT0Print1");
            Map(o => o.FollowupLetterTimeT0Print2, "FollowupLetterTimeT0Print2");
            Map(o => o.FollowupLetterTimeT0Print3, "FollowupLetterTimeT0Print3");
            Map(o => o.NursingUnitPrinter, "NursingUnitPrinter");
            Map(o => o.FollowUpLetterTemplate, "FollowUpLetterTemplate");
            Map(o => o.ExternalId, "ExternalID");

            References(o => o.MammoLetterPrinter, "MammoLetterPrinterNo").Cascade.All();
            References(o => o.Printer, "PrinterNo").Cascade.All();
        }
    }
}

namespace Kevins.Examples.Database.Mapping
{
    public class PrinterMap : BaseEntityMap<Printer>
    {
        public PrinterMap()
        {
            Table("Printers");

            Id(o => o.Id, "PrinterNo");
            Map(o => o.PrinterNickname, "PrinterNickname");
            Map(o => o.PrinterAlias, "PrinterAlias");
            Map(o => o.Notes, "Notes");
        }
    }
}

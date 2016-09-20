namespace Kevins.Examples.Database.Mapping
{
    public class DepartmentMap : BaseEntityMap<Department>
    {
        public DepartmentMap()
        {
            Table("Depts");

            Id(o => o.Id, "Dept_no");
            Map(o => o.EnglishDescription, "Desc_E");
            Map(o => o.FrenchDescription, "Desc_F");
            Map(o => o.LetDeptMembersSeeAllExams, "LetDeptMembersSeeAllExams");
            Map(o => o.LetDeptMembersSeeOtherExams, "LetDeptMembersSeeOtherExams");
            Map(o => o.JpegFileName, "JpgFileName");

            References(o => o.Printer, "PrinterNo").Fetch.Join();
        }
    }
}

namespace Kevins.Examples.Database
{
    public class Department : BaseEntity<Department>
    {
        public virtual string EnglishDescription { get; set; }
        public virtual string FrenchDescription { get; set; }
        public virtual bool LetDeptMembersSeeAllExams { get; set; }
        public virtual bool LetDeptMembersSeeOtherExams { get; set; }
        public virtual Printer Printer { get; set; }
        public virtual string JpegFileName { get; set; }
    }
}
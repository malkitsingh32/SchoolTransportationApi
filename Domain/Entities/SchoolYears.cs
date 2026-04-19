namespace Domain.Entities
{
    public class SchoolYears
    {
        public int? Id { get; set; } 
        public int SchoolYear { get; set; }
        public int NumberOfStudents { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
    }
}

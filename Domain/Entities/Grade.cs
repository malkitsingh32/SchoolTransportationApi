namespace Domain.Entities
{
    public class Grade
    {
        public int? Id { get; set; } 
        public string? GradeName { get; set; } 
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
    }
}

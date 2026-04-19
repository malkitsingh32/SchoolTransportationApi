namespace Domain.Entities
{
    public class Schools
    {
        public int? Id { get; set; }  
        public string SchoolName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public int? AreaId { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
    }
}

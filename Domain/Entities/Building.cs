namespace Domain.Entities
{
    public class Building
    {
        public int? Id { get; set; }  
        public string Address { get; set; }
        public int? SchoolId { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string Name { get; set; }
    }
}

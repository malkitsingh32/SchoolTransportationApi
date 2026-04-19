namespace Domain.Entities
{
    public class Family
    {
        public int? Id { get; set; }  
        public string? LastName { get; set; }
        public string? FatherFirstName { get; set; }
        public string? MotherFirstName { get; set; }
        public string? Address { get; set; }
        public string? HomeNumber { get; set; }
        public string? FatherCell { get; set; }
        public string? MotherCell { get; set; }
        public int? State { get; set; }
        public int? City { get; set; }
        public int? Zipcode { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
    }
}

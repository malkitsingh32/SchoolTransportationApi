namespace Domain.Entities
{
    public class CC
    {
        public int? Id { get; set; } 
        public int CardnoxId { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public int? FamilyId { get; set; }
    }
}

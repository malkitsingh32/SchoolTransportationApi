namespace Domain.Entities
{
    public class ChargeStructure
    {
        public int? Id { get; set; }
        public int? DistrictId { get; set; }
        public bool IsIDBlank { get; set; }
        public int? NtId { get; set; }
        public bool ChargeMonthly { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public decimal? Price { get; set; }
    }
}

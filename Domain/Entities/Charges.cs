namespace Domain.Entities
{
    public class Charges
    {
        public int? ChargeID { get; set; } 
        public int? StudentID { get; set; }
        public decimal? ChargeAmount { get; set; }
        public DateTime? ChargeDate { get; set; }
        public string? ChargeStatus { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
    }
}

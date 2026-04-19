namespace Domain.Entities
{
    public class Payments
    {
        public int? PaymentId { get; set; } 
        public int? ChargeId { get; set; }
        public int? CardId { get; set; }
        public string? PaymentStatus { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string? PaymentType { get; set; }
    }
}

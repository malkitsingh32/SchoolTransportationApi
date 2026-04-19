namespace Domain.Entities
{
    public class PayrollDeductionAmount
    {
        public int? Id { get; set; } 
        public decimal? Amount { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
    }
}

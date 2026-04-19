namespace Domain.Entities
{
    public class ReservedAccountBalance
    {
        public int? Id { get; set; }  
        public int? DriverId { get; set; }
        public int? RouteId { get; set; }
        public int? AssignedDriverId { get; set; }
        public decimal? Balance { get; set; }
        public decimal? DeductedAmount { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string? TransactionType { get; set; }  
        public Guid? RouteGroupId { get; set; }
        public DateTime? RouteDate { get; set; }
    }
}

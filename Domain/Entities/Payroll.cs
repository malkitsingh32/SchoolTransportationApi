namespace Domain.Entities
{
    public class Payroll
    {
        public int? PayrollID { get; set; }  
        public int? DriverID { get; set; }
        public decimal? Amount { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime Date { get; set; }  
        public int TotalRoutes { get; set; }
        public int RegularRoutes { get; set; }
        public int PickedUpRoutes { get; set; }
        public int AbsentRoutes { get; set; }
    }
}

namespace DTO.Response.Payroll
{
    public class GetPayrollResponseDto
    {
        public int? PayrollId { get; set; }
        public int DriverId { get; set; }
        //public DateTime Date { get; set; }  // Use DateTime for date fields
        public DateTime WeekStart { get; set; }
        public DateTime WeekEnd { get; set; }
        public decimal Amount { get; set; }
        public int TotalRoutes { get; set; }
        public int RegularRoutes { get; set; }
        public int PickedUpRoutes { get; set; }
        public int AbsentRoutes { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public decimal ReservedAccountBalance { get; set; }
    }
}

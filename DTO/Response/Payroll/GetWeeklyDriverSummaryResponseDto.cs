namespace DTO.Response.Payroll
{
    public class GetWeeklyDriverSummaryResponseDto
    {
        public int? DriverId { get; set; }
        public string TodaysDriver { get; set; }
        public DateTime WeekStart { get; set; }
        public DateTime WeekEnd { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal PayRate { get; set; }
        public int? RegularRoutes { get; set; }
        public int? PickupRoutes { get; set; }
        public int? AbsentRoutes { get; set; }
        public int? TotalRoutesDriven { get; set; }
        public int? DriverTypeId { get; set; }
        public decimal DeductionAmount { get; set; }
        public decimal Amount { get; set; }
        public decimal ExclusivePay { get; set; }

    }
}

namespace DTO.Response.Payroll
{
    public class GetPayrollForAllDriversByDateResponseDto
    {
        public string RouteName { get; set; }
        public DateTime Date { get; set; }
        public int DefaultDriverId { get; set; }
        public string Type { get; set; }
        public bool ExclusivelyPay { get; set; }
        public bool IsCredit { get; set; }
        public decimal Amount { get; set; }
        public decimal? DeductionAmount { get; set; }
        public decimal? PayRate { get; set; }
        public int DriverId { get; set; }
        public string DriverName { get; set; }
        public string RouteFor { get; set; }
        public bool IsPickedRoute { get; set; }
        public int RouteOwnerDriverId { get; set; }
        public int? ActualDriverId { get; set; }
        public int? DriverTypeId { get; set; }
    }
}

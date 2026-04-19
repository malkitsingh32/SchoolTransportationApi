namespace DTO.Response.Payroll
{
    public class GetPayrollByDateAndDriverIdResponseDto
    {
        public DateTime Date { get; set; }
        public int DriverId { get; set; }
        public int DefaultDriverId { get; set; }
        public string RouteName { get; set; }
        public decimal Amount { get; set; }
        public decimal PayRate { get; set; }
        public string Type { get; set; }
        public bool exclusivelyPay { get; set; }
        public bool IsCredit { get; set; }
        public bool IsPickedRoute { get; set; }
        public decimal DeductionAmount { get; set; }
    }
}

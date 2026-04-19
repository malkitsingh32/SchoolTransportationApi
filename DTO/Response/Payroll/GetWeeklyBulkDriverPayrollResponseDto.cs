namespace DTO.Response.Payroll
{
    public class GetWeeklyBulkDriverPayrollResponseDto
    {
        public string TodaysDriver { get; set; }
        public DateTime WeekStart { get; set; }
        public DateTime WeekEnd { get; set; }
        public Decimal? ExclusivePay { get; set; }
        public Decimal? PayRate { get; set; }
        public Decimal? DeductionAmount { get; set; }
        public Decimal? Amount { get; set; }
        public int? DriverTypeId { get; set; }
    }
}

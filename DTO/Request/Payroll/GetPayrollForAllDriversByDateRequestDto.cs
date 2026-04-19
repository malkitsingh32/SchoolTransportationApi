namespace DTO.Request.Payroll
{
    public class GetPayrollForAllDriversByDateRequestDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}

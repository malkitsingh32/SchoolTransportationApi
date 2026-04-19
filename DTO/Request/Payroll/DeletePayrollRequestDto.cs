namespace DTO.Request.Payroll
{
    public class DeletePayrollRequestDto
    {
        public int DriverId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}

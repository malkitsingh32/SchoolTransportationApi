namespace DTO.Request.Drivers
{
    public class DeductReservedAccountBalanceRequestDto
    {
        public int DriverId { get; set; }
        public int AssignedDriverId { get; set; }
        public int RouteId { get; set; }
        public Guid RouteGroupId { get; set; }
        public int UserId { get; set; }
        public DateTime RouteDate { get; set; }
        public decimal deductionAmount { get; set; }
        public bool isCreditToTodayDriver { get; set; }
        public int? RouteDetailId { get; set; }
    }
}

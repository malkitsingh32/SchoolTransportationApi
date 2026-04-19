namespace DTO.Response.Driver
{
    public class GetDriversBalanceHistoryResponseDto
    {
        public int DriverId { get; set; }
        public int AssignedDriverId { get; set; }
        public int RouteId { get; set; }
        public decimal Balance { get; set; }
        public decimal DeductedAmount { get; set; }
        public DateTime CreatedDate { get; set; }
        public string AssignedDriverFirstName { get; set; }
        public string AssignedDriverLastName { get; set; }
        public string RouteNumber { get; set; }
        public string TransactionType { get; set; }
    }
}

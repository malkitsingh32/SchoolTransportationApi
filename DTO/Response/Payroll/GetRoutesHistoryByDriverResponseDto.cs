namespace DTO.Response.Payroll
{
    public class GetRoutesHistoryByDriverResponseDto
    {
        public int DriverId { get; set; }       // Driver's ID
        public int RouteId { get; set; }        // Route ID
        public DateTime Date { get; set; }      // Date of record
        public string RouteName { get; set; }   // Name of the Route
        public string RouteType { get; set; }   // Type of Route
        public decimal Amount { get; set; }
        public int DefaultDriverId { get; set; }
    }
}

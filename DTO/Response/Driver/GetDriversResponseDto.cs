namespace DTO.Response.Driver
{
    public class GetDriversResponseDto
    {
        public int DriverID { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string? Status { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public string BusNumber { get; set; }
        public string BusName { get; set; }
        public string RouteId { get; set; }
        public int BusId { get; set; }
        public int TotalRoute { get; set; }
        public decimal ReservedAccountBalance { get; set; }
        public int DefaultRouteId { get; set; }
        public string DriverType { get; set; }
        public int DriverTypeId { get; set; }
        public decimal PayRate { get; set; }
        public DateTime TempBusStartTime { get; set; }
        public DateTime TempBusEndTime { get; set; }
        public int TempBus { get; set; }
        public string? RunType { get; set; }


    }
}

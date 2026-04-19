namespace DTO.Response.Bus
{
    public class GetBusesResponseDto
    {
        public int BusDetailId { get; set; }
        public string BusNumber { get; set; }
        public string BusName { get; set; }
        public string Station { get; set; }
        public string DefaultDriver { get; set; }
        public string DefaultDriverContact { get; set; }
        public string DriverFullName { get; set; }
        public string Route { get; set; }
        public string RouteId { get; set; }
        public int StudentCount { get; set; }
        public string? YearOfManufacture { get; set; }
        public string DriverFirstName { get; set; }
        public string DriverLastName { get; set; }
        public int Status { get; set; }
        public string StatusName { get; set; }
        public string RunType { get; set; }
        public string TempDefaultDriver { get; set; }
        public string TempDefaultDriverContact { get; set; }
        public string TempDriverFullName { get; set; }
        public string TempRunType { get; set; }

    }
}

namespace DTO.Request.BusDetails
{
    public class AddUpdateBusDetailsRequestDto
    {
        public int BusDetailID { get; set; }
        public string? BusName { get; set; }
        public string? Station { get; set; }
        public string? BusNumber { get; set; }
        public string? YearOfManufacture { get; set; }
        public int UserId { get; set; }
        public int? Route { get; set; }
        public string? DefaultDriver { get; set; }
        public bool IsDelete { get; set; }
        public int StudentCount { get; set; }
        public int? Status { get; set; }
        public bool IsOverWrite { get; set; }
        public bool IsTempOverWrite { get; set; }
        public int PreviousBusId { get; set; }
        public string RunType { get; set; }
        public string? TempDefaultDriver { get; set; }
        public string? TempRunType { get; set; }

    }
}

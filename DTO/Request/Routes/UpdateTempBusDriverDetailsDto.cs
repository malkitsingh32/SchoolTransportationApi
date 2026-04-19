namespace DTO.Request.Routes
{
    public class UpdateTempBusDriverDetailsDto
    {
        public int DriverID { get; set; }
        public int? TempBus { get; set; }
        public string? RunType { get; set; }
        public DateTime? TempBusStartTime { get; set; }
        public DateTime? TempBusEndTime { get; set; }
    }
}

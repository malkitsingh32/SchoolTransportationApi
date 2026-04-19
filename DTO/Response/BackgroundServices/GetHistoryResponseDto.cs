namespace DTO.Response.BackgroundServices
{
    public class GetHistoryResponseDto
    {
        public DateTime? Date { get; set; }
        public int? RouteId { get; set; }
        public int? BusDetailId { get; set; }
        public int? DefaultDriverId { get; set; }
        public int? DriverId { get; set; }
    }
}

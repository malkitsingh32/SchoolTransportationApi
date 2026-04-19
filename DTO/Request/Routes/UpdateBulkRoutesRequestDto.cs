namespace DTO.Request.Routes
{
    public class UpdateBulkRoutesRequestDto
    {
        public RouteInfo[] Route { get; set; }
        public string? Time { get; set; }
        public int? TodaysDriver { get; set; }
        public int? RouteType { get; set; }
    }

    public class RouteInfo
    {
        public DateTime? RouteDate { get; set; }
        public Guid? RouteGroupId { get; set; }
    }
}

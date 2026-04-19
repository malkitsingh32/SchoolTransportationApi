namespace DTO.Request.Routes
{
    public class AssignRouteToDriverRequestDto
    {
        public int? DriverId { get; set; }
        public int? RouteId { get; set; }
        public DateTime? RouteDate { get; set; }
    }
}

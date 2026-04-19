namespace DTO.Request.Routes
{
    public class UpdateRouteGroupDto
    {
        public int RouteId { get; set; }
        public int NewDriverId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}

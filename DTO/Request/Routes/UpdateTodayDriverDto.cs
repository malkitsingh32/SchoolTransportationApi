namespace DTO.Request.Routes
{
    public class UpdateTodayDriverDto
    {
        public int DriverID { get; set; }
        public List<RouteDetailListDto> RouteJson { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
    public class RouteDetailListDto
    {
        public int RouteID { get; set; }
    }
}

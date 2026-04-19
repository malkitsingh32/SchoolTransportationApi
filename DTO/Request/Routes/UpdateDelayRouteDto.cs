namespace DTO.Request.Routes
{
    public class UpdateDelayRouteDto
    {
        public string School { get; set; } 
        public string Time { get; set; } 
        public string Gender { get; set; } 
        public string Grade { get; set; }
        public string RouteId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}

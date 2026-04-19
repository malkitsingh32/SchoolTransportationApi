namespace DTO.Response.Routes
{
    public class GetRoutesListResponseDto
    {
        public int RouteID { get; set; }
        public int? DefaultDriver { get; set; }
        public string RouteNumber { get; set; }
        public TimeSpan? Time { get; set; }
        public string? Mosdos { get; set; }
        public string? Grade { get; set; }
        public string RouteName { get; set; }
        public int Type { get; set; }
        public string Days { get; set; }
        public int? TodaysDriver { get; set; }
        public int? PickUp { get; set; }
        public string? DropOffBuilding { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }       
        public string? DropOffBuildingName { get; set; }
        public string RouteType { get; set; }
        public DateTime? RouteDate { get; set; }
        public Guid RouteGroupID { get; set; }
        public bool IsMatched { get; set; }
    }
}

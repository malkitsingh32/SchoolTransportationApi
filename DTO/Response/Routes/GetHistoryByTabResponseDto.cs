namespace DTO.Response.Routes
{
    public class GetHistoryByTabResponseDto
    {
        public int HistoryId { get; set; }
        public int RouteID { get; set; }
        public string RouteName { get; set; }
        public string RouteNumber { get; set; }
        public int BusDetailId { get; set; }
        public string BusName { get; set; }
        public string BusNumber { get; set; }
        public int DriverID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime Date { get; set; } 
    }
}

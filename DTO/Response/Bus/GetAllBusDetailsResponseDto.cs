namespace DTO.Response.Bus
{
    public class GetAllBusDetailsResponseDto
    {
        public int? BusDetailID { get; set; }
        public string BusType { get; set; }
        public string BusName { get; set; }
        public string Station { get; set; }
        public int RouteId { get; set; }

    }
}

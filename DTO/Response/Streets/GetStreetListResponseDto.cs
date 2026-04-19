namespace DTO.Response.Streets
{
    public class GetStreetListResponseDto
    {
        public int Id { get; set; }
        public string? StreetName { get; set; }
        public string? AreaName { get; set; }
        public int? Area { get; set; }
        public int? FromNumber { get; set; }
        public int? ToNumber { get; set; }
        public int? OfRoutes { get; set; }
        public string? RouteId { get; set; }
    }
}

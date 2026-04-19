namespace DTO.Response.Students
{
    public class SearchLocationResponseDto
    {
        public string Address { get; set; }
        public string StreetNumber { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}

namespace DTO.Request.BackgroundServices
{
    public class BusLocationResponseDto
    {
        public List<VehicleData> Data { get; set; }
    }

    public class VehicleData
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public GpsData Gps { get; set; }
    }

    public class GpsData
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Time { get; set; }
        public ReverseGeoData ReverseGeo { get; set; }
    }

    public class ReverseGeoData
    {
        public string FormattedLocation { get; set; }
    }
}

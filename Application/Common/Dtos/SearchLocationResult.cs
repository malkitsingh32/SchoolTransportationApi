namespace Application.Common.Dtos
{
    public class SearchLocationResult
    {
        public bool IsMap { get; set; }
        public string Address { get; set; }
        public string Cross { get; set; }
        public string GooglePlaceId { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string FormattedAddress { get; set; }
        public string Name { get; set; }
        public string InternationalPhoneNumber { get; set; }
        public string Url { get; set; }
        public string Website { get; set; }
        public List<string> Types { get; set; }
        public List<AddressComponent> AddressComponents { get; set; }
        public string AddressMask { get; set; }
        public string StreetNumberMask { get; set; }
    }
}

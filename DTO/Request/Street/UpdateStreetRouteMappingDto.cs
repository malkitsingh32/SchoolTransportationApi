namespace DTO.Request.Street
{
    public class UpdateStreetRouteMappingDto
    {
        public UpdateStreetRouteMappingDto()
        {
            StreetList = new List<StreetReq>();
        }
        public List<StreetReq> StreetList { get; set; }
    }

    public class StreetReq
    {
        public int RouteId { get; set; }
        public int? StudentID { get; set; }
        public int? StreetId { get; set; }

        public int? RowNumber { get; set; }
        public string? BusStopLatLong { get; set; }
        public string? Address { get; set; }
        public int StreetNumber { get; set; }
    }

}

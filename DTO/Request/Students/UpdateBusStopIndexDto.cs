namespace DTO.Request.Students
{
    public class UpdateBusStopIndexDto
    {
        public UpdateBusStopIndexDto()
        {
            BusStopList = new List<BusStopReq>();
        }
        public List<BusStopReq> BusStopList { get; set; }
    }
    public class BusStopReq
    {
        public int? StudentID { get; set; }
        public int RowNumber { get; set; }
        public string? BusNumber { get; set; }
        public string? BusStopLatLong { get; set; }
        public string? UniqueId { get; set; }
        public int UserId { get; set; } 
        public string? Address { get; set; }
        public int StreetNumber { get; set; }


    }
}

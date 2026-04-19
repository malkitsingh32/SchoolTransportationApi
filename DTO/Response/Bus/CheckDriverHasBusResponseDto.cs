namespace DTO.Response.Bus
{
    public class CheckDriverHasBusResponseDto
    {
        public int Id { get; set; }
        public int DriverId { get; set; }
        public string BusName { get; set; }
        public string BusNumber { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
    }
}

namespace DTO.Response.Driver
{
    public class GetDriversListResponseDto
    {
        public int DriverID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Status { get; set; }
        public string Address { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string BusId { get; set; }
        public decimal DeductionAmount { get; set; }
        public string RunType { get; set; }
    }
}

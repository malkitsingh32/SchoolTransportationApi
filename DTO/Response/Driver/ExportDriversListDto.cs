namespace DTO.Response.Driver
{
    public class ExportDriversListDto
    {
        public int DriverID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Status { get; set; }
        public string BusName { get; set; }
        public int TotalRoute { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string DriverType { get; set; }
        public int DriverTypeId { get; set; }
    }
}

namespace DTO.Response.Routes
{
    public class DownloadPrintOrderResponseDto
    {
        public int Step { get; set; }
        public int StreetNumber { get; set; }
        public string StreetName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DropoffBuilding { get; set; }
        public string BuildingAddress { get; set; }
        public string Note { get; set; }
        public string Grade { get; set; }
        public int TotalStudent { get; set; }
        //public TimeOnly Time { get; set; }
    }

}

namespace DTO.Response.Admin
{
    public class ExportFamilyListDto
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FatherFirstName { get; set; }
        public string MotherFirstName { get; set; }
        public string Address { get; set; }
        public string HomeNumber { get; set; }
        public string FatherCell { get; set; }
        public string MotherCell { get; set; }
        public int State { get; set; }
        public int City { get; set; }
        public int Zipcode { get; set; }
        public string StateName { get; set; }
        public string CityName { get; set; }
        public int Area { get; set; }
        public string AreaName { get; set; }
        public int District { get; set; }
        public string DistrictName { get; set; }
        public bool IsTracking { get; set; }
        public int StreetNumber { get; set; }

    }
}

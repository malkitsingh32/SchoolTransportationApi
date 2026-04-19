namespace DTO.Request.Admin
{
    public class AddStreetsAreaMappingDto
    {
        public int Id { get; set; }
        public string StreetName { get; set; }
        public int? AreaId { get; set; }
        public int? UserId { get; set; }
        public int DistrictId { get; set; }
    }
}

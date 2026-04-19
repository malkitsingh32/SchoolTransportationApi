namespace DTO.Request.Street
{
    public class AddSchoolBuildingBranchMappingDto
    {
        public AddSchoolBuildingBranchMappingDto()
        {
            schoolList = new List<AddSchoolReq>();
        }
        public List<AddSchoolReq> schoolList { get; set; }
    }
    public class AddSchoolReq
    {
        public int? RouteId { get; set; }
        public int? SchoolId { get; set; }
        public int? BuildingId { get; set; }

    }
}

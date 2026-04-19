namespace DTO.Request.Routes
{
    public class UpdateSchoolBuildingBranchMappingDto
    {
        public UpdateSchoolBuildingBranchMappingDto()
        {
            SchoolBuildingBranchList = new List<SchoolBuildingBranchReq>();
            RouteIdList = new List<RouteIdReq>();
        }
        public List<SchoolBuildingBranchReq> SchoolBuildingBranchList { get; set; }
        public List<RouteIdReq> RouteIdList { get; set; }
    }

    public class SchoolBuildingBranchReq
    {
        public int? SchoolId { get; set; }
        public int? BuildingId { get; set; }
        public int RowNumber { get; set; }
    }

    public class RouteIdReq
    {
        public int? RouteId { get; set; }
    }
}

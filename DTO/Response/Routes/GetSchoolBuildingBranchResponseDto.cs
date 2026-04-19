namespace DTO.Response.Routes
{
    public class GetSchoolBuildingBranchResponseDto
    {
        public int Id { get; set; }
        public int RouteID { get; set; }
        public int SchoolId { get; set; }
        public string SchoolName { get; set; }
        public int BuildingId { get; set; }
        public string BuildingName { get; set; }
        public string BuildingAddress { get; set; }
        public int RowNumber { get; set; }
    }
}

namespace DTO.Response.Routes
{
    public class GetSchoolBuildingBranchDetailsResponseDto
    {
        public int? BuildingId { get; set; }
        public string BuildingName { get; set; }
        public string BuildingAddress { get; set; }
        public int SchoolId { get; set; }
        public string SchoolName { get; set; }
        public int? BranchId { get; set; }
        public string BranchName { get; set; }
        public bool isBoy { get; set; }
        public bool isGirl { get; set; }
        public bool isBoyBuilding { get; set; }
        public bool isGirlBuilding { get; set; }
        public bool isBoyBranch { get; set; }
        public bool isGirlBranch { get; set; }
        public string? GradeIds { get; set; }
        public string? GradeNames { get; set; }
    }
}

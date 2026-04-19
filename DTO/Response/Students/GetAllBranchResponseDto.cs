

namespace DTO.Response.Students
{
    public class GetAllBranchResponseDto
    {
        public int Id { get; set; }
        public int? BuildingId { get; set; }
        public string BranchName { get; set; }
        public int? GenderId { get; set; }
        public string GradeId { get; set; }
        public string SchoolId { get; set; }
    }
}

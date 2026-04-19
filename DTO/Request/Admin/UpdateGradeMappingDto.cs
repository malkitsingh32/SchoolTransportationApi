namespace DTO.Request.Admin
{
    public class UpdateGradeMappingDto
    {
        public UpdateGradeMappingDto()
        {
            GradeList = new List<GradeReq>();
        }
        public List<GradeReq> GradeList { get; set; }
    }

    public class GradeReq
    {
        public int GradeId { get; set; }
        public int? GenderId { get; set; }
        public List<int>? SchoolId { get; set; }
        public int RowNumber { get; set; }
    }
}

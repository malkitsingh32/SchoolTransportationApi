namespace DTO.Response.SystemValues
{
    public class GetAllGradeResponseDto
    {
        public int Id { get; set; }
        public string GradeName { get; set; }
        public int GenderId { get; set; }
        public string GenderName { get; set; }
        public string SchoolId { get; set; }
        //public string SchoolName { get; set; }
        public int RowNumber { get; set; }
    }
}

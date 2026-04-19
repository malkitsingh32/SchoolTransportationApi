namespace DTO.Request.Routes
{
    public class GetAreasBySchoolAndGradeRequestDto
    {
        public string? School { get; set; }
        public string? Grade { get; set; }
        public string? Building { get; set; }
        public string? Branch { get; set; }
        public int? Gender { get; set; }
    }
}

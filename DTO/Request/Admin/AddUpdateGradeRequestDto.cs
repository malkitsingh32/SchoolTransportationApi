namespace DTO.Request.Admin
{
    public class AddUpdateGradeRequestDto
    {
        public int Id { get; set; }
        public string GradeName { get; set; }
        public int UserId { get; set; }
        public int Gender { get; set; }
        public List<int>? SchoolId { get; set; }
    }
}

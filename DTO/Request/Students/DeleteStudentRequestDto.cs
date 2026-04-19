namespace DTO.Request.Students
{
    public class DeleteStudentRequestDto
    {
        public int Id { get; set; }
        public bool IsFromRoute { get; set; }
        public int Type { get; set; }
    }
}

namespace DTO.Request.Students
{
    public class UpdateStudentRouteNoteDto
    {
        public int StudentId { get; set; }
        public int RouteId { get; set; }
        public string Note { get; set; }
    }
}

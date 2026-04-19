namespace DTO.Request.Students
{
    public class AssignRouteToStudentRequestDto
    {
        public List<int> RouteId { get; set; }
        public List<AssignRouteStudentDto> Students { get; set; }
    }
    public class AssignRouteStudentDto
    {
        public int StudentId { get; set; }
        public string StreetNumber { get; set; }
        public string Address { get; set; }
    }
}

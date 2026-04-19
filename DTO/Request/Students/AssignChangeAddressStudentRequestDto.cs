namespace DTO.Request.Students
{
    public class AssignChangeAddressStudentRequestDto
    {
        public List<int> RouteId { get; set; }
        public List<AssignRouteChangeAddressStudentDto> Students { get; set; }
        public bool IsAssignStudent { get; set; }
    }
    public class AssignRouteChangeAddressStudentDto
    {
        public int StudentId { get; set; }
        public string StreetNumber { get; set; }
        public string Address { get; set; }
    }
}

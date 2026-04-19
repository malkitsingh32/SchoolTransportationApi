namespace DTO.Request.Routes
{
    public class CheckRouteTypeStudentRequestDto
    {       
        public int Type { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public List<StudentBusDto> Students { get; set; }
    }


    public class RouteTypeStudentResponseDto
    {
        public int? StudentId { get; set; }
        public int? RouteID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? RouteGroupID { get; set; }
    }

}

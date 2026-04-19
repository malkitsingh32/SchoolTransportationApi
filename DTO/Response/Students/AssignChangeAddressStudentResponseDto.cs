namespace DTO.Response.Students
{
    public class AssignChangeAddressStudentResponseDto
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }

        public int? OldRouteId { get; set; }
        public int? NewRouteId { get; set; }

        public string RouteNumber { get; set; }   // from Routes
        public string RouteName { get; set; }     // from Routes
        public TimeSpan? Time { get; set; }       // or string, based on DB
        public string BusName { get; set; }    // from Routes

        public string Status { get; set; }         // EXISTS | NEW_ASSIGNED

    }
}

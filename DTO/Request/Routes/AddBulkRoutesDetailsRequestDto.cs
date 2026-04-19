namespace DTO.Request.Routes
{
    public class AddBulkRoutesDetailsRequestDto
    {
        public int? RouteId { get; set; }
        public string? RouteNumber { get; set; }
        public string? Times { get; set; }
        public string? School { get; set; }
        public string? Grade { get; set; }
        public string? Branch { get; set; }
        public string? TotalBuses { get; set; }
        public string? DropOffBuilding { get; set; }
        public int? CreatedBy { get; set; }
        public string? Days { get; set; }    
        public string? EndDate { get; set; }            
        public int? Gender { get; set; }               
        public string? RouteName { get; set; }
        public string? Areas { get; set; }
        public string? StartDate { get; set; }        
        public int? Type { get; set; }
        public List<StudentBusDto> Students { get; set; }
        public int? PickUp { get; set; }
        public int? SeasonFolderId { get; set; }
        public bool? IsDraftRoute { get; set; }
        public string? BusTeacherName { get; set; }
        public string? BusTeacherPhone { get; set; }
        public string? Color { get; set; }
        public bool ExclusivelyPay { get; set; }
        public List<OverrideStudentDto> OverrideStudentList { get; set; }
    }

    public class StudentBusDto
    {
        public int? StudentId { get; set; }
        public int? BusID { get; set; }
        public int? RowNumber { get; set; }
        public int? StreetNumber { get; set; }
        public string? BusStopLatLong { get; set; }
        public string? Address { get; set; }

    } 
    
    public class InsertedRouteResponseDto
    {
        public string? RouteID { get; set; }
    }
    public class OverrideStudentDto
    {
        public int RouteID { get; set; }
        public int StudentId { get; set; }
    }


}

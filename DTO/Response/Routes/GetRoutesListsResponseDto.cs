namespace DTO.Response.Routes
{
    public class GetRoutesListsResponseDto
    {
        public int RouteID { get; set; }
        public int? DefaultDriver { get; set; }
        public string? RouteNumber { get; set; }
        public TimeSpan? Time { get; set; }
        public string? Mosdos { get; set; }
        public decimal Price { get; set; }
        public string? Grade { get; set; }
        public string? GradeId { get; set; }
        public string? RouteName { get; set; }
        public string? PhoneNumber { get; set; }
        public int? Type { get; set; }
        public string? Days { get; set; }
        public string? DropOffBuilding { get; set; }
        public int? TodaysDriver { get; set; }
        public string? DropOffBuildingName { get; set; }
        public string RouteType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime? RouteDate { get; set; }
        public Guid RouteGroupID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? DriverFullName { get; set; }
        public string? TodayDriverFirstName { get; set; }
        public string? TodayDriverLastName { get; set; }
        public string? TodayDriverFullName { get; set; }
        public string? branch { get; set; }
        public string? BusName { get; set; }
        public string? BranchName { get; set; }
        public string? SchoolName { get; set; }
        public string? Areas { get; set; }
        public int? PickUp { get; set; }
        public string? DriverNotes { get; set; }
        public string? busNumber { get; set; }
        public int? TotalStudents { get; set; }
        public int? TodaysBus { get; set; }
        public int? DefaultBus { get; set; }
        public string? DefaultBusName { get; set; }
        public bool ExclusivelyPay { get; set; }
        public int RouteDetailID { get; set; }
        public bool IsDraftRoute { get; set; }
        public string? TodaysBusName { get; set; }
        public string? BusStopLatLong { get; set; }
        public int? Gender { get; set; }
        public int? SeasonFolderId { get; set; }
    }
}

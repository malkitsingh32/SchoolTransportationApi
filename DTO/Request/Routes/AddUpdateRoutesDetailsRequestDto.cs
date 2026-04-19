namespace DTO.Request.Routes
{
    public class AddUpdateRoutesDetailsRequestDto
    {
     
        public int? RouteID { get; set; }
        public Guid? RouteGroupID { get; set; }
        public string? RouteNumber { get; set; }
        public string? Time { get; set; }
        public string? Mosdos { get; set; }
        public decimal? Price { get; set; }
        public string? Grade { get; set; }
        public string? RouteName { get; set; }
        public int? Type { get; set; }
        public string? Days { get; set; }
        public int? TodaysDriver { get; set; }        
        public int? DefaultDriver { get; set; }
        public string? DropOffBuilding { get; set; }
        public int? UserId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? RouteDate { get; set; }
        public string? branch { get; set; }
        public string? BranchName { get; set; }
        public string? SchoolName { get; set; }
        public string? PhoneNumber { get; set; }
        public int? PickUp { get; set; }
        public string? DriverNotes { get; set; }
        public int? TodaysBus { get; set; }
        public bool? ConfirmRoute { get; set; }
        public bool? IsFuture { get; set; }
        public int? SeasonFolderId { get; set; }
        public string? Areas { get; set; }
        public string? BusTeacherName { get; set; }
        public string? BusTeacherPhone { get; set; }
        public bool? ExclusivelyPay { get; set; }
        public string? Color { get; set; }
    }

}


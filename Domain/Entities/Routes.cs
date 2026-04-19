namespace Domain.Entities
{
    public class Routes
    {
        public int? RouteID { get; set; } 
        public int? DefaultDriver { get; set; }
        public string? RouteNumber { get; set; }
        public TimeSpan? Time { get; set; }
        public string? Mosdos { get; set; }
        public string? Grade { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string? RouteName { get; set; }
        public int? Type { get; set; }
        public string? Days { get; set; }
        public int? TodaysDriver { get; set; }
        public int? DropOffBuilding { get; set; }
        public decimal? Price { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? RouteDate { get; set; }
        public Guid? RouteGroupID { get; set; }
        public int? Branch { get; set; }
    }
}

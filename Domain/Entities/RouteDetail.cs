namespace Domain.Entities
{
    public class RouteDetail
    {
        public int? RouteDetailID { get; set; }  
        public Guid RouteGroupID { get; set; }
        public DateTime RouteDate { get; set; }
        public int? TodaysDriver { get; set; }
        public bool? IsActive { get; set; }
        public TimeSpan? Time { get; set; }
        public int? Type { get; set; }
    }
}

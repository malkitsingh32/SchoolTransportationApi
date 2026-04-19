namespace Domain.Entities
{
    public class History
    {
        public int? HistoryId { get; set; }  
        public int RouteId { get; set; }
        public int? BusId { get; set; }
        public int? DriverId { get; set; }
        public DateTime? Date { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? DefaultDriverId { get; set; }
        public Guid? RouteGroupID { get; set; }
    }
}

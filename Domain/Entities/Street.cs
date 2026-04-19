namespace Domain.Entities
{
    public class Street
    {
        public int? ID { get; set; }  
        public string? StreetName { get; set; }
        public int? Area { get; set; }
        public int? FromNumber { get; set; }
        public int? ToNumber { get; set; }
        public int? OfRoutes { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string? RouteId { get; set; }
    }
}

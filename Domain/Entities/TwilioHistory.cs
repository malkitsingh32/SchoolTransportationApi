namespace Domain.Entities
{
    public class TwilioHistory
    {
        public int? Id { get; set; } 
        public string BusId { get; set; } = null!;
        public string RouteId { get; set; } = null!;
        public string RouteTime { get; set; } = null!;
        public string Time { get; set; } = null!;
        public bool IsMessageSent { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDelete { get; set; }
    }
}

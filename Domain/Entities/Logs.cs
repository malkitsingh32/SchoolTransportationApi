namespace Domain.Entities
{
    public class Logs
    {
        public long? LogId { get; set; }  
        public string? Message { get; set; }
        public string? From { get; set; }
        public string? MessageType { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
    }
}

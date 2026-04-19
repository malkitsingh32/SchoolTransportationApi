namespace DTO.Request.Notification
{
    public class SaveBulkMessageRequestDto
    {
        public string RecipientType { get; set; }     
        public string MessageBody { get; set; }
        public string MessageType { get; set; }       
        public DateTime? ScheduledDateTime { get; set; }
        public int CreatedBy { get; set; }
    }
}

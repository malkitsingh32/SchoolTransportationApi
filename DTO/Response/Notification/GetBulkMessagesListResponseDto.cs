namespace DTO.Response.Notification
{
    public class GetBulkMessagesListResponseDto
    {
        public int MessageId { get; set; }
        public string RecipientType { get; set; }
        public string MessageBody { get; set; }
        public string MessageType { get; set; }
        public DateTime? ScheduledDateTime { get; set; }
        public DateTime CreatedAt { get; set; }
        public string PhoneNumber { get; set; }
    }
}

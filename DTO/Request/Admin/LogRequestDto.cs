namespace DTO.Request.Admin
{
    public class LogRequestDto
    {
        public long LogId { get; set; }
        public string LogLevel { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Module { get; set; }
        public string RequestData { get; set; }
        public string ResponseData { get; set; }
    }
}

namespace DTO.Request.Twilio
{
    public class TwilioRequest
    {
        public string? From { get; set; }
        public string? To { get; set; }
        public string? CallSid { get; set; }
        public string? Digits { get; set; }
    }
}

namespace DTO.Response.Driver
{
    public class SendLinkToResetDriverPasswordDto
    {
        public int DriverId { get; set; }
        public string? Email { get; set; }
    }
}

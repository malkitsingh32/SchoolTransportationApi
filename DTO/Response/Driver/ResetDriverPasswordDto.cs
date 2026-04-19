namespace DTO.Response.Driver
{
    public class ResetDriverPasswordDto
    {
        public int DriverId { get; set; }
        public string? Email { get; set; }
        public string Password { get; set; }
    }
}

namespace DTO.Request.Admin
{
    public class AddDriverTypeRequestDto
    {
        public int Id { get; set; }
        public string DriverType { get; set; }
        public decimal PayRate { get; set; }
        public int UserId { get; set; }
    }
}

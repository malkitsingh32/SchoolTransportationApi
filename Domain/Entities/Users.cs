namespace Domain.Entities
{
    public class Users
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? RoleId { get; set; }
        public string Role { get; set; }
        public string? PhoneNumber { get; set; }
        public string? UserName { get; set; }
        public bool? Status { get; set; }
        public int? Id { get; set; }
        public bool isGoogleLogin { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? OtpCode { get; set; }
    }
}

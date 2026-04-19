namespace Application.Handler.User.Dtos
{
    public class AuthenticateDto
    {
        public int? UserId { get; set; }
        public int? RoleId { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? TokenExpire { get; set; }
        public string FirstName { get; set; }
        public string? UserName { get; set; }
        public string LastName { get; set; }
    }
}

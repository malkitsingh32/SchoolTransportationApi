using DTO.Response;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Handler.User.Command.AddUser
{
    public class AddUserCommand  : IRequest<CommonResultResponseDto<string>>
    {

        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Password { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public int RoleId { get; set; }
        public string? Role { get; set; }
        public string? PhoneNumber { get; set; }
        public string? UserName { get; set; }
        public bool? Status { get; set; }
        public int UserId { get; set; }
        public int? Id { get; set; }
        public bool isGoogleLogin { get; set; }

    }
}

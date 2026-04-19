using Application.Handler.User.Dtos;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Handler.User.Command.AuthenticateUser
{
    public class AuthenticateUserCommand : IRequest<AuthenticateDto>
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}

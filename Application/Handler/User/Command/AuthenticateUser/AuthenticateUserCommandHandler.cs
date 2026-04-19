using Application.Abstraction.Services;
using Application.Handler.User.Dtos;
using MediatR;

namespace Application.Handler.User.Command.AuthenticateUser
{
    public class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateUserCommand, AuthenticateDto>
    {
        private readonly IUserService _userService;


        public AuthenticateUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<AuthenticateDto> Handle(AuthenticateUserCommand authenticateUserCommand, CancellationToken cancellationToken)
        {
            var user = await _userService.GetUserByCredentials(authenticateUserCommand.Email, authenticateUserCommand.Password, null);
            return await Task.FromResult(user);
        }
    }
}

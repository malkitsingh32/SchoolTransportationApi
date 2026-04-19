using Application.Abstraction.Services;
using DTO.Response;
using MediatR;

namespace Application.Handler.User.Command.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, CommonResultResponseDto<string>>
    {
        private readonly IUserService _userService;
        public DeleteUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<CommonResultResponseDto<string>> Handle(DeleteUserCommand deleteUserCommand, CancellationToken cancellationToken)
        {
            return await _userService.DeleteUser(deleteUserCommand.Id, deleteUserCommand.Role);
        }
    }
}

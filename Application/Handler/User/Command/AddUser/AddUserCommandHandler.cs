using Application.Abstraction.Services;
using Domain.Entities;
using DTO.Response;
using Mapster;
using MediatR;

namespace Application.Handler.User.Command.AddUser
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, CommonResultResponseDto<string>>
    {
        private readonly IUserService _userService;

        public AddUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<CommonResultResponseDto<string>> Handle(AddUserCommand addUserCommand, CancellationToken cancellationToken)
        {
            //await _userService.AddUser(addUserCommand.Adapt<Users>(), addUserCommand.Password);
            //return await Task.FromResult(Unit.Value);
            var user = await _userService.AddUser(addUserCommand.Adapt<Users>(), addUserCommand.Password);
            return user;
        }
    }
}

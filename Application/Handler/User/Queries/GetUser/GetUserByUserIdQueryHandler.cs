using Application.Abstraction.Services;
using Application.Handler.User.Dtos;
using Mapster;
using MediatR;

namespace Application.Handler.User.Queries.GetUser
{
    public class GetUserByUserIdQueryHandler : IRequestHandler<GetUserByUserIdQuery, UserDto>
    {
        private readonly IUserService _userService;
        public GetUserByUserIdQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<UserDto> Handle(GetUserByUserIdQuery getUsersByUserIdQuery, CancellationToken cancellationToken)
        {
            var user = await _userService.GetUserByUserId(getUsersByUserIdQuery.UserId);
            return user.Adapt<UserDto>();
        }
    }
}

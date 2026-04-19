using Application.Handler.User.Dtos;
using MediatR;

namespace Application.Handler.User.Queries.GetUser
{
    public class GetUserByUserIdQuery : IRequest<UserDto>
    {
        public int UserId { get; set; }
    }
}

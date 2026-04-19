using DTO.Response;
using MediatR;

namespace Application.Handler.User.Command.DeleteUser
{
    public class DeleteUserCommand : IRequest<CommonResultResponseDto<string>>
    {
        public int Id { get; set; }
        public string Role { get; set; }
    }
}

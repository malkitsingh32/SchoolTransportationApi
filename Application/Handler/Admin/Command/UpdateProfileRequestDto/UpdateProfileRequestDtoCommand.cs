using DTO.Response;
using MediatR;

namespace Application.Handler.Admin.Command.UpdateProfileRequestDto
{
    public class UpdateProfileRequestDtoCommand : IRequest<CommonResultResponseDto<string>>
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string? Password { get; set; }
    }
}

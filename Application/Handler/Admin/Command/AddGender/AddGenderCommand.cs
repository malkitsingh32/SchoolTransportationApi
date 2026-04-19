using DTO.Response;
using MediatR;

namespace Application.Handler.Admin.Command.AddGender
{
    public class AddGenderCommand : IRequest<CommonResultResponseDto<string>>
    {
        public int Id { get; set; }
        public string Gender { get; set; }
        public int UserId { get; set; }
    }
}

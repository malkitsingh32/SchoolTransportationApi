using DTO.Response;
using MediatR;

namespace Application.Handler.Admin.Command.DeleteGender
{
    public class DeleteGenderCommand : IRequest<CommonResultResponseDto<string>>
    {
        public int Id { get; set; }
        public bool IsDelete { get; set; }
    }
}

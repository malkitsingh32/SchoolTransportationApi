using DTO.Response;
using MediatR;

namespace Application.Handler.Admin.Command.DeleteCC
{
    public class DeleteCCCommand : IRequest<CommonResultResponseDto<string>>
    {
        public int Id { get; set; }
    }
}

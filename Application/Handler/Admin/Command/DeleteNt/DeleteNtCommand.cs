using DTO.Response;
using MediatR;

namespace Application.Handler.Admin.Command.DeleteNt
{
    public class DeleteNtCommand : IRequest<CommonResultResponseDto<string>>
    {
        public int Id { get; set; }
        public bool IsDelete { get; set; }
    }
}

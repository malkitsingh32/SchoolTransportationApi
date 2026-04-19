using DTO.Response;
using MediatR;

namespace Application.Handler.Admin.Command.DeleteDriverType
{
    public class DeleteDriverTypeCommand : IRequest<CommonResultResponseDto<string>>
    {
        public int Id { get; set; }
        public bool IsDelete { get; set; }
    }
}

using DTO.Response;
using MediatR;

namespace Application.Handler.Admin.Command.DeleteBusType
{
    public class DeleteBusTypeCommand : IRequest<CommonResultResponseDto<string>>
    {
        public int Id { get; set; }
        public bool IsDelete { get; set; }
    }
}

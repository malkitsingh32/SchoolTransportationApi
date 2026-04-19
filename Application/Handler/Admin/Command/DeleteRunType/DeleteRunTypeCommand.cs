using DTO.Response;
using MediatR;

namespace Application.Handler.Admin.Command.DeleteRunType
{
    public class DeleteRunTypeCommand : IRequest<CommonResultResponseDto<string>>
    {
        public int Id { get; set; }
    }
}

using DTO.Response;
using MediatR;

namespace Application.Handler.Admin.Command.DeleteChargeStructure
{
    public class DeleteChargeStructureCommand : IRequest<CommonResultResponseDto<string>>
    {
        public int Id { get; set; }
    }
}

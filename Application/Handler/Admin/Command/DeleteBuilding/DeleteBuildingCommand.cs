using DTO.Response;
using MediatR;

namespace Application.Handler.Admin.Command.DeleteBuilding
{
    public class DeleteBuildingCommand : IRequest<CommonResultResponseDto<string>>
    {
        public int Id { get; set; }
        public bool IsDelete { get; set; }
    }
}

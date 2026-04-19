using Application.Abstraction.Services;
using DTO.Response;
using MediatR;

namespace Application.Handler.Admin.Command.DeleteBuilding
{
    public class DeleteBuildingCommandHandler : IRequestHandler<DeleteBuildingCommand, CommonResultResponseDto<string>>
    {
        private readonly IAdminService _adminService;
        public DeleteBuildingCommandHandler(IAdminService adminService)
        {
            _adminService = adminService;
        }
        public async Task<CommonResultResponseDto<string>> Handle(DeleteBuildingCommand deleteBuildingCommand, CancellationToken cancellationToken)
        {
            return await _adminService.DeleteBuilding(deleteBuildingCommand.Id,deleteBuildingCommand.IsDelete);
        }
    }
}

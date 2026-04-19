using Application.Abstraction.Services;
using DTO.Response;
using MediatR;

namespace Application.Handler.Admin.Command.AddBuilding
{
    public class AddBuildingCommandHandler : IRequestHandler<AddBuildingCommand, CommonResultResponseDto<string>>
    {
        private readonly IAdminService _adminService;
        public AddBuildingCommandHandler(IAdminService adminService)
        {
            _adminService = adminService;
        }
        public async Task<CommonResultResponseDto<string>> Handle(AddBuildingCommand addBuildingCommand, CancellationToken cancellationToken)
        {
            var user = await _adminService.AddBuilding(addBuildingCommand.Id, addBuildingCommand.Address, addBuildingCommand.SchoolId, addBuildingCommand.UserId,addBuildingCommand.BuildingName);
            return await Task.FromResult(user);
        }
    }
}

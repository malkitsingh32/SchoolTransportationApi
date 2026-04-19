using Application.Abstraction.Services;
using DTO.Response;
using MediatR;

namespace Application.Handler.Admin.Command.AddStreetsAreaMapping
{
    public class AddStreetsAreaMappingCommandHandler : IRequestHandler<AddStreetsAreaMappingCommand, CommonResultResponseDto<string>>
    {
        private readonly IAdminService _adminService;
        public AddStreetsAreaMappingCommandHandler(IAdminService adminService)
        {
            _adminService = adminService;
        }
        public async Task<CommonResultResponseDto<string>> Handle(AddStreetsAreaMappingCommand addStreetsAreaMappingCommand, CancellationToken cancellationToken)
        {
            var user = await _adminService.AddStreetsAreaMapping(addStreetsAreaMappingCommand.Id, addStreetsAreaMappingCommand.StreetName , addStreetsAreaMappingCommand.AreaId , addStreetsAreaMappingCommand.UserId, addStreetsAreaMappingCommand.DistrictId);
            return await Task.FromResult(user);
        }
    }
}

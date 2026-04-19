using Application.Abstraction.Services;
using DTO.Response;
using MediatR;

namespace Application.Handler.Admin.Command.AddAreas
{
    public class AddAreasCommandHandler : IRequestHandler<AddAreasCommand ,CommonResultResponseDto<string>>
    {
        private readonly IAdminService _adminService;
        public AddAreasCommandHandler(IAdminService adminService)
        {
            _adminService = adminService;
        }
        public async Task<CommonResultResponseDto<string>> Handle(AddAreasCommand addAreasCommand, CancellationToken cancellationToken)
        {
            var user = await _adminService.AddAreas(addAreasCommand.Id,addAreasCommand.AreaName, addAreasCommand.UserId,addAreasCommand.ShortName);
            return await Task.FromResult(user);
        }
    }
}

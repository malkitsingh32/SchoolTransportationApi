using Application.Abstraction.Services;
using DTO.Request.Admin;
using DTO.Response;
using Mapster;
using MediatR;

namespace Application.Handler.Admin.Command.UpdateFamilyTracking
{
    public class UpdateFamilyTrackingCommandHandler : IRequestHandler<UpdateFamilyTrackingCommand, CommonResultResponseDto<string>>
    {
        private readonly IAdminService _adminService;
        public UpdateFamilyTrackingCommandHandler(IAdminService adminService)
        {
            _adminService = adminService;
        }
        public async Task<CommonResultResponseDto<string>> Handle(UpdateFamilyTrackingCommand updateFamilyTrackingCommand, CancellationToken cancellationToken)
        {
            var user = await _adminService.UpdateFamilyTracking(updateFamilyTrackingCommand.Adapt<UpdateFamilyTrackingRequestDto>());
            return await Task.FromResult(user);
        }
    }
}

using Application.Abstraction.Services;
using DTO.Request.Admin;
using DTO.Response;
using Mapster;
using MediatR;

namespace Application.Handler.Admin.Command.UpdateTrackingTime
{
    public class UpdateTrackingTimeCommandHandler : IRequestHandler<UpdateTrackingTimeCommand, CommonResultResponseDto<string>>
    {
        private readonly IAdminService _adminService;
        public UpdateTrackingTimeCommandHandler(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<CommonResultResponseDto<string>> Handle(UpdateTrackingTimeCommand updateTrackingTimeCommand, CancellationToken cancellationToken)
        {
            return await _adminService.UpdateTrackingTime(updateTrackingTimeCommand.Adapt<UpdateTrackingTimeRequestDto>());
        }
    }
}

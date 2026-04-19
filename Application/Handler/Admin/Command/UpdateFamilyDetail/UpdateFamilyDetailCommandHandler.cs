

using Application.Abstraction.Services;
using DTO.Request.Admin;
using DTO.Response;
using Mapster;
using MediatR;

namespace Application.Handler.Admin.Command.UpdateFamilyDetail
{
    public class UpdateFamilyDetailCommandHandler : IRequestHandler<UpdateFamilyDetailCommand, CommonResultResponseDto<string>>
    {
        private readonly IAdminService _adminService;
        public UpdateFamilyDetailCommandHandler(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<CommonResultResponseDto<string>> Handle(UpdateFamilyDetailCommand updateFamilyDetailCommand, CancellationToken cancellationToken)
        {
            return await _adminService.UpdateFamilyDetail(updateFamilyDetailCommand.Adapt<UpdateFamilyDetailRequestDto>());
        }
    }
}

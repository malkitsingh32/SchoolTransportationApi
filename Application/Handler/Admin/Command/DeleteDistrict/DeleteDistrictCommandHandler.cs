using Application.Abstraction.Services;
using DTO.Response;
using MediatR;

namespace Application.Handler.Admin.Command.DeleteDistrict
{
    public class DeleteDistrictCommandHandler : IRequestHandler<DeleteDistrictCommand, CommonResultResponseDto<string>>
    {
        private readonly IAdminService _adminService;
        public DeleteDistrictCommandHandler(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<CommonResultResponseDto<string>> Handle(DeleteDistrictCommand deleteDistrictCommand, CancellationToken cancellationToken)
        {
            return await _adminService.DeleteDistrict(deleteDistrictCommand.Id,deleteDistrictCommand.IsDelete);
        }
    }
}

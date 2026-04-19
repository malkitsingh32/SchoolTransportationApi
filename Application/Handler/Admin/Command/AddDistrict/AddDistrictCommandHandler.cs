using Application.Abstraction.Services;
using DTO.Response;
using MediatR;

namespace Application.Handler.Admin.Command.AddDistrict
{
    public class AddDistrictCommandHandler : IRequestHandler<AddDistrictCommand, CommonResultResponseDto<string>>
    {
        private readonly IAdminService _adminService;
        public AddDistrictCommandHandler(IAdminService adminService)
        {
            _adminService = adminService;
        }
        public async Task<CommonResultResponseDto<string>> Handle(AddDistrictCommand addDistrictCommand, CancellationToken cancellationToken)
        {
            var user = await _adminService.AddDistrict(addDistrictCommand.Id,addDistrictCommand.DistrictName, addDistrictCommand.UserId);
            return await Task.FromResult(user);
        }
    }
}

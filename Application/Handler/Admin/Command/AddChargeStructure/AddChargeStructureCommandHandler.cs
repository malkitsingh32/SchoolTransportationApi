using Application.Abstraction.Services;
using DTO.Response;
using MediatR;

namespace Application.Handler.Admin.Command.AddChargeStructure
{
    public class AddChargeStructureCommandHandler : IRequestHandler<AddChargeStructureCommand, CommonResultResponseDto<string>>
    {
        private readonly IAdminService _adminService;
        public AddChargeStructureCommandHandler(IAdminService adminService)
        {
            _adminService = adminService;
        }
        public async Task<CommonResultResponseDto<string>> Handle(AddChargeStructureCommand addChargeStructureCommand, CancellationToken cancellationToken)
        {
            var user = await _adminService.AddChargeStructure(addChargeStructureCommand.Id,addChargeStructureCommand.DistrictId, addChargeStructureCommand.NtId, addChargeStructureCommand.IsFunded, addChargeStructureCommand.UseId, addChargeStructureCommand.Price, addChargeStructureCommand.SchoolId);
            return await Task.FromResult(user);
        }
    }
}

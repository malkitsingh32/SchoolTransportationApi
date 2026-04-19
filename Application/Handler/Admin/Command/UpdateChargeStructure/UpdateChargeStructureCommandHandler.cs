using Application.Abstraction.Services;
using DTO.Response;
using MediatR;

namespace Application.Handler.Admin.Command.UpdateChargeStructure
{
    public class UpdateChargeStructureCommandHandler : IRequestHandler<UpdateChargeStructureCommand, CommonResultResponseDto<string>>
    {
        private readonly IAdminService _adminService;
        public UpdateChargeStructureCommandHandler(IAdminService adminService)
        {
            _adminService = adminService;
        }
        public async Task<CommonResultResponseDto<string>> Handle(UpdateChargeStructureCommand updateChargeStructureCommand, CancellationToken cancellationToken)
        {
            return await _adminService.UpdateChargeStructure(updateChargeStructureCommand.Id, updateChargeStructureCommand.IsFunded);
        }
    }
}

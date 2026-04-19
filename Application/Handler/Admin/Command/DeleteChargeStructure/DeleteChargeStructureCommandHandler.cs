using Application.Abstraction.Services;
using DTO.Response;
using MediatR;

namespace Application.Handler.Admin.Command.DeleteChargeStructure
{
    public class DeleteChargeStructureCommandHandler : IRequestHandler<DeleteChargeStructureCommand, CommonResultResponseDto<string>>
    {
        private readonly IAdminService _adminService;
        public DeleteChargeStructureCommandHandler(IAdminService adminService)
        {
            _adminService = adminService;
        }
        public async Task<CommonResultResponseDto<string>> Handle(DeleteChargeStructureCommand deleteChargeStructureCommand, CancellationToken cancellationToken)
        {
            return await _adminService.DeleteChargeStructure(deleteChargeStructureCommand.Id);
        }
    }
}

using Application.Abstraction.Services;
using DTO.Response;
using MediatR;

namespace Application.Handler.Admin.Command.DeleteCC
{
    public class DeleteCCCommandHandler : IRequestHandler<DeleteCCCommand, CommonResultResponseDto<string>>
    {
        private readonly IAdminService _adminService;
        public DeleteCCCommandHandler(IAdminService adminService)
        {
            _adminService = adminService;
        }
        public async Task<CommonResultResponseDto<string>> Handle(DeleteCCCommand deleteCCCommand, CancellationToken cancellationToken)
        {
            return await _adminService.DeleteCC(deleteCCCommand.Id);
        }
    }
}

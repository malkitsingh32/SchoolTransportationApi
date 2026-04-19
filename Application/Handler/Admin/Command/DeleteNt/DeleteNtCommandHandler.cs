using Application.Abstraction.Services;
using DTO.Response;
using MediatR;

namespace Application.Handler.Admin.Command.DeleteNt
{
    public class DeleteNtCommandHandler : IRequestHandler<DeleteNtCommand, CommonResultResponseDto<string>>
    {
        private readonly IAdminService _adminService;
        public DeleteNtCommandHandler(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<CommonResultResponseDto<string>> Handle(DeleteNtCommand deleteNtCommand, CancellationToken cancellationToken)
        {
            return await _adminService.DeleteNt(deleteNtCommand.Id, deleteNtCommand.IsDelete);
        }
    }
}

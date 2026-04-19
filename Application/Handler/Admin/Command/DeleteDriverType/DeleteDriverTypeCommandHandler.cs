using Application.Abstraction.Services;
using DTO.Response;
using MediatR;

namespace Application.Handler.Admin.Command.DeleteDriverType
{
    public class DeleteDriverTypeCommandHandler : IRequestHandler<DeleteDriverTypeCommand, CommonResultResponseDto<string>>
    {
        private readonly IAdminService _adminService;
        public DeleteDriverTypeCommandHandler(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<CommonResultResponseDto<string>> Handle(DeleteDriverTypeCommand deleteDriverTypeCommand, CancellationToken cancellationToken)
        {
            return await _adminService.DeleteDriverType(deleteDriverTypeCommand.Id, deleteDriverTypeCommand.IsDelete);
        }
    }
}

using Application.Abstraction.Services;
using DTO.Response;
using MediatR;

namespace Application.Handler.Admin.Command.DeleteAreas
{
    public class DeleteAreasCommandHandler : IRequestHandler<DeleteAreasCommand, CommonResultResponseDto<string>>
    {
        private readonly IAdminService _adminService;
        public DeleteAreasCommandHandler(IAdminService adminService)
        {
            _adminService = adminService;
        }
        public async Task<CommonResultResponseDto<string>> Handle(DeleteAreasCommand deleteAreasCommand, CancellationToken cancellationToken)
        {
            return await _adminService.DeleteArea(deleteAreasCommand.Id, deleteAreasCommand.IsDelete);
        }
    }
}

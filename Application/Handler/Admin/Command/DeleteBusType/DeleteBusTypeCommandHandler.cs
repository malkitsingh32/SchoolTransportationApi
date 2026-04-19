using Application.Abstraction.Services;
using DTO.Response;
using MediatR;

namespace Application.Handler.Admin.Command.DeleteBusType
{
    public class DeleteBusTypeCommandHandler : IRequestHandler<DeleteBusTypeCommand, CommonResultResponseDto<string>>
    {
        private readonly IAdminService _adminService;
        public DeleteBusTypeCommandHandler(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<CommonResultResponseDto<string>> Handle(DeleteBusTypeCommand deleteBusTypeCommand, CancellationToken cancellationToken)
        {
            return await _adminService.DeleteBusType(deleteBusTypeCommand.Id,deleteBusTypeCommand.IsDelete);
        }
    }
}

using Application.Abstraction.Services;
using DTO.Request.Admin;
using DTO.Response;
using Mapster;
using MediatR;

namespace Application.Handler.Admin.Command.DeleteRunType
{
    public class DeleteRunTypeCommandHandler : IRequestHandler<DeleteRunTypeCommand, CommonResultResponseDto<string>>
    {
        private readonly IAdminService _adminService;
        public DeleteRunTypeCommandHandler(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<CommonResultResponseDto<string>> Handle(DeleteRunTypeCommand deleteRunTypeCommand, CancellationToken cancellationToken)
        {
            return await _adminService.DeleteRunType(deleteRunTypeCommand.Adapt<DeleteRunTypeRequestDto>());
        }
    }
}

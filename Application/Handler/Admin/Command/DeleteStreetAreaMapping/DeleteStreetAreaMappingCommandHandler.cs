using Application.Abstraction.Services;
using DTO.Response;
using MediatR;

namespace Application.Handler.Admin.Command.DeleteStreetAreaMapping
{
    public class DeleteStreetAreaMappingCommandHandler : IRequestHandler<DeleteStreetAreaMappingCommand, CommonResultResponseDto<string>>
    {
        private readonly IAdminService _adminService;
        public DeleteStreetAreaMappingCommandHandler(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<CommonResultResponseDto<string>> Handle(DeleteStreetAreaMappingCommand deleteStreetAreaMappingCommand, CancellationToken cancellationToken)
        {
            return await _adminService.DeleteStreetAreaMapping(deleteStreetAreaMappingCommand.Id);
        }
    }
}

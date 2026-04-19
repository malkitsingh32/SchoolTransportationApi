using Application.Abstraction.Services;
using DTO.Request.Admin;
using DTO.Response;
using Mapster;
using MediatR;

namespace Application.Handler.Admin.Command.AddSeasonFolder
{
    public class AddSeasonFolderCommandHandler : IRequestHandler<AddSeasonFolderCommand, CommonResultResponseDto<string>>
    {
        private readonly IAdminService _adminService;
        public AddSeasonFolderCommandHandler(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<CommonResultResponseDto<string>> Handle(AddSeasonFolderCommand addSeasonFolderCommand, CancellationToken cancellationToken)
        {
            return await _adminService.AddSeasonFolder(addSeasonFolderCommand.Adapt<AddSeasonFolderRequestDto>());
        }
    }
}

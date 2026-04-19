using Application.Abstraction.Services;
using DTO.Request.Admin;
using DTO.Response;
using Mapster;
using MediatR;

namespace Application.Handler.Admin.Command.DeleteSeasonFolder
{
    public class DeleteSeasonFolderCommandHandler : IRequestHandler<DeleteSeasonFolderCommand, CommonResultResponseDto<string>>
    {
        private readonly IAdminService _adminService;
        public DeleteSeasonFolderCommandHandler(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<CommonResultResponseDto<string>> Handle(DeleteSeasonFolderCommand deleteSeasonFolderCommand, CancellationToken cancellationToken)
        {
            return await _adminService.DeleteSeasonFolder(deleteSeasonFolderCommand.Adapt<DeleteSeasonFolderRequestDto>());
        }
    }
}

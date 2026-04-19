using Application.Abstraction.Services;
using DTO.Response;
using MediatR;

namespace Application.Handler.Admin.Command.ImportKml
{
    public class ImportKmlCommandHandler : IRequestHandler<ImportKmlCommand, CommonResultResponseDto<string>>
    {
        private readonly IAdminService _adminService;
        public ImportKmlCommandHandler(IAdminService adminService)
        {
            _adminService = adminService;
        }
        public async Task<CommonResultResponseDto<string>> Handle(ImportKmlCommand importKmlCommand, CancellationToken cancellationToken)
        {
            return await _adminService.ImportKml(importKmlCommand);
        }
    }
}

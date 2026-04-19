using Application.Abstraction.Services;
using DTO.Response;
using DTO.Response.SystemValues;
using MediatR;

namespace Application.Handler.Admin.Queries.GetAllSeasonFolder
{
    internal class GetAllSeasonFolderQueryHandler : IRequestHandler<GetAllSeasonFolderQuery, CommonResultResponseDto<IList<GetSeasonFolderResponseDto>>>
    {
        private readonly IAdminService _adminService;

        public GetAllSeasonFolderQueryHandler(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<CommonResultResponseDto<IList<GetSeasonFolderResponseDto>>> Handle(GetAllSeasonFolderQuery request, CancellationToken cancellationToken)
        {
            return await _adminService.GetAllSeasonFolder();
        }
    }
}

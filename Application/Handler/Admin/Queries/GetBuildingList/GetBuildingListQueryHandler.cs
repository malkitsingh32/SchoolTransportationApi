using Application.Abstraction.Services;
using DTO.Response;
using DTO.Response.Admin;
using MediatR;

namespace Application.Handler.Admin.Queries.GetBuildingList
{
    public class GetBuildingListQueryHandler : IRequestHandler<GetBuildingListQuery, CommonResultResponseDto<IList<GetBuildingListResponseDto>>>
    {
        private readonly IAdminService _adminService;

        public GetBuildingListQueryHandler(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<CommonResultResponseDto<IList<GetBuildingListResponseDto>>> Handle(GetBuildingListQuery request, CancellationToken cancellationToken)
        {
            return await _adminService.GetBuildingList();
        }
    }
}

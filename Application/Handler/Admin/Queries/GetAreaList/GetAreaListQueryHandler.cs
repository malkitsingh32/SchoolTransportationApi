using Application.Abstraction.Services;
using DTO.Response;
using DTO.Response.Admin;
using MediatR;

namespace Application.Handler.Admin.Queries.GetAreaList
{
    public class GetAreaListQueryHandler : IRequestHandler<GetAreaListQuery, CommonResultResponseDto<IList<GetAreaListResponseDto>>>
    {
        private readonly IAdminService _adminService;

        public GetAreaListQueryHandler(IAdminService adminService)
        {
            _adminService = adminService;
        }
        public async Task<CommonResultResponseDto<IList<GetAreaListResponseDto>>> Handle(GetAreaListQuery request, CancellationToken cancellationToken)
        {
            return await _adminService.GetAreaList();
        }
    }
}

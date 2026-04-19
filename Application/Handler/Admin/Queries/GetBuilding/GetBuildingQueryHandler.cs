using Application.Abstraction.Services;
using Application.Common.Interfaces.Common;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.SystemValues;
using MediatR;

namespace Application.Handler.Admin.Queries.GetBuilding
{
    public class GetBuildingQueryHandler : IRequestHandler<GetBuildingQuery, CommonResultResponseDto<PaginatedList<GetBuildingResponseDto>>>
    {

        private readonly IAdminService _adminService;
        private readonly IRequestBuilder _requestBuilder;
        public GetBuildingQueryHandler(IAdminService adminService, IRequestBuilder requestBuilder)
        {
            _adminService = adminService;
            _requestBuilder = requestBuilder;
        }

        public async Task<CommonResultResponseDto<PaginatedList<GetBuildingResponseDto>>> Handle(GetBuildingQuery request, CancellationToken cancellationToken)
        {
            var filterModel = _requestBuilder.GetRequestBuilder(request.CommonRequest);
            return await _adminService.GetBuilding(filterModel.GetFilters(), request.CommonRequest, filterModel.GetSorts());
        }
    }
}

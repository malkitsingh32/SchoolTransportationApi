

using Application.Abstraction.Services;
using Application.Common.Interfaces.Common;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.Streets;
using MediatR;

namespace Application.Handler.Routes.Queries.GetSchoolBuildingBranchList
{
    public class GetSchoolBuildingBranchListQueryHandler : IRequestHandler<GetSchoolBuildingBranchListQuery, CommonResultResponseDto<PaginatedList<GetSchoolBuildingBranchListResponseDto>>>
    {
        private readonly IRoutesService _routesService;
        private readonly IRequestBuilder _requestBuilder;
        public GetSchoolBuildingBranchListQueryHandler(IRoutesService routesService, IRequestBuilder requestBuilder)
        {
            _routesService = routesService;
            _requestBuilder = requestBuilder;
        }
        public async Task<CommonResultResponseDto<PaginatedList<GetSchoolBuildingBranchListResponseDto>>> Handle(GetSchoolBuildingBranchListQuery request, CancellationToken cancellationToken)
        {
            var filterModel = _requestBuilder.GetRequestBuilder(request.CommonRequest);
            return await _routesService.GetSchoolBuildingBranchList(filterModel.GetFilters(), request.CommonRequest, filterModel.GetSorts());
        }
    }
}

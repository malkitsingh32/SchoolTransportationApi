using Application.Abstraction.Services;
using Application.Common.Dtos;
using Application.Common.Interfaces.Common;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.Routes;
using MediatR;

namespace Application.Handler.Routes.Queries.GetSchoolBuildingBranchByRouteId
{
    public class GetSchoolBuildingBranchByRouteIdQueryHandler : IRequestHandler<GetSchoolBuildingBranchByRouteIdQuery, CommonResultResponseDto<PaginatedList<GetSchoolBuildingBranchResponseDto>>>
    {
        private readonly IRoutesService _routesService;
        private readonly IRequestBuilder _requestBuilder;

        public GetSchoolBuildingBranchByRouteIdQueryHandler(IRoutesService routesService, IRequestBuilder requestBuilder)
        {
            _routesService = routesService;
            _requestBuilder = requestBuilder;

        }

        public async Task<CommonResultResponseDto<PaginatedList<GetSchoolBuildingBranchResponseDto>>> Handle(GetSchoolBuildingBranchByRouteIdQuery request, CancellationToken cancellationToken)
        {
            var filterModel = _requestBuilder.GetRequestBuilder(request.CommonRequest);
            return await _routesService.GetSchoolBuildingBranchByRouteId(filterModel.GetFilters(), request.CommonRequest, filterModel.GetSorts(), request.RouteId);
        }

        public class GetSchoolBuildingBranchByRouteIdRequestDto
        {
            public ServerRowsRequest CommonRequest { get; set; }
            public int RouteId { get; set; }

        }
    }
}

using Application.Abstraction.Services;
using Application.Common.Interfaces.Common;
using Application.Common.Response;
using Application.Handler.Routes.Queries.GetRoutes;
using DTO.Response;
using DTO.Response.Routes;
using Mapster;
using MediatR;

namespace Application.Handler.Routes.Queries.GetRoutesByTabs
{
    public class GetRoutesByTabsQueryHandler : IRequestHandler<GetRoutesByTabsQuery, CommonResultResponseDto<PaginatedList<GetRoutesResponseDto>>>
    {
        private readonly IRoutesService _routesService;
        private readonly IRequestBuilder _requestBuilder;

        public GetRoutesByTabsQueryHandler(IRoutesService routesService, IRequestBuilder requestBuilder)
        {
            _routesService = routesService;
            _requestBuilder = requestBuilder;

        }

        public async Task<CommonResultResponseDto<PaginatedList<GetRoutesResponseDto>>> Handle(GetRoutesByTabsQuery request, CancellationToken cancellationToken)
        {
            var filterModel = _requestBuilder.GetRequestBuilder(request.CommonRequest);
            return await _routesService.GetRoutesByTabs(filterModel.GetFilters(), request.CommonRequest, filterModel.GetSorts(), request.Adapt<GetRoutesRequestDto>());
        }

    }
}

using Application.Abstraction.Services;
using Application.Common.Dtos;
using Application.Common.Interfaces.Common;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.Routes;
using MediatR;

namespace Application.Handler.Routes.Queries.GetRoutesDetailsByTypeId
{
    public class GetRoutesDetailsByTypeIdQueryHandler : IRequestHandler<GetRoutesDetailsByTypeIdQuery, CommonResultResponseDto<PaginatedList<GetRoutesResponseDto>>>
    {
        private readonly IRoutesService _routesService;
        private readonly IRequestBuilder _requestBuilder;

        public GetRoutesDetailsByTypeIdQueryHandler(IRoutesService routesService, IRequestBuilder requestBuilder)
        {
            _routesService = routesService;
            _requestBuilder = requestBuilder;

        }

        public async Task<CommonResultResponseDto<PaginatedList<GetRoutesResponseDto>>> Handle(GetRoutesDetailsByTypeIdQuery request, CancellationToken cancellationToken)
        {
            var filterModel = _requestBuilder.GetRequestBuilder(request.CommonRequest);
            return await _routesService.GetRoutesDetailsByTypeId(filterModel.GetFilters(), request.CommonRequest, filterModel.GetSorts(), request.RouteTypeId);
        }

    }
    public class GetRoutesDetailsByTypeIdRequestDto
    {
        public ServerRowsRequest CommonRequest { get; set; }
        public int RouteTypeId { get; set; }

    }
}

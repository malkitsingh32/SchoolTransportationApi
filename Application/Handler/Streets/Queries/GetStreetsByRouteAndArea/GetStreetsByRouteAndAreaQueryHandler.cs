using Application.Abstraction.Services;
using Application.Common.Interfaces.Common;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.Streets;
using MediatR;

namespace Application.Handler.Streets.Queries.GetStreetsByRouteAndArea
{
    public class GetStreetsByRouteAndAreaQueryHandler : IRequestHandler<GetStreetsByRouteAndAreaQuery, CommonResultResponseDto<PaginatedList<GetStreetsResponseDto>>>
    {
        private readonly IStreetsService _streetsService;
        private readonly IRequestBuilder _requestBuilder;
        public GetStreetsByRouteAndAreaQueryHandler(IStreetsService streetsService, IRequestBuilder requestBuilder)
        {
            _streetsService = streetsService;
            _requestBuilder = requestBuilder;
        }

        public async Task<CommonResultResponseDto<PaginatedList<GetStreetsResponseDto>>> Handle(GetStreetsByRouteAndAreaQuery request, CancellationToken cancellationToken)
        {
            var filterModel = _requestBuilder.GetRequestBuilder(request.CommonRequest);
            return await _streetsService.GetStreetsByRouteAndArea(filterModel.GetFilters(), request.CommonRequest, filterModel.GetSorts(), request.RouteId);
        }
    }
}

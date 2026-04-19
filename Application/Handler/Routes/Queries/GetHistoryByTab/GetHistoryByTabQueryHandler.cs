using Application.Abstraction.Services;
using Application.Common.Dtos;
using Application.Common.Interfaces.Common;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.Routes;
using MediatR;

namespace Application.Handler.Routes.Queries.GetHistoryByTab
{
    public class GetHistoryByTabQueryHandler : IRequestHandler<GetHistoryByTabQuery, CommonResultResponseDto<PaginatedList<GetHistoryByTabResponseDto>>>
    {
        private readonly IRoutesService _routesService;
        private readonly IRequestBuilder _requestBuilder;
        public GetHistoryByTabQueryHandler(IRoutesService routesService, IRequestBuilder requestBuilder)
        {
            _routesService = routesService;
            _requestBuilder = requestBuilder;
        }

        public async Task<CommonResultResponseDto<PaginatedList<GetHistoryByTabResponseDto>>> Handle(GetHistoryByTabQuery request, CancellationToken cancellationToken)
        {
            var filterModel = _requestBuilder.GetRequestBuilder(request.CommonRequest);
            return await _routesService.GetHistoryByTab(filterModel.GetFilters(), request.CommonRequest, filterModel.GetSorts(), request.Tab, request.Id);
        }
    }

    public class GetHistoryByTabRequestDto
    {
        public ServerRowsRequest CommonRequest { get; set; }
        public string Tab { get; set; }
        public int Id { get; set; }

    }
}

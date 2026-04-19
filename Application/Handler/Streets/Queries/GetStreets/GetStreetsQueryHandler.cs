using Application.Abstraction.Services;
using Application.Common.Dtos;
using Application.Common.Interfaces.Common;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.Streets;
using MediatR;

namespace Application.Handler.Streets.Queries.GetStreets
{
    public class GetStreetsQueryHandler : IRequestHandler<GetStreetsQuery, CommonResultResponseDto<PaginatedList<GetStreetsResponseDto>>>
    {
        private readonly IStreetsService _streetsService;
        private readonly IRequestBuilder _requestBuilder;
        public GetStreetsQueryHandler(IStreetsService streetsService, IRequestBuilder requestBuilder)
        {
            _streetsService = streetsService;
            _requestBuilder = requestBuilder;
        }
        public async Task<CommonResultResponseDto<PaginatedList<GetStreetsResponseDto>>> Handle(GetStreetsQuery request, CancellationToken cancellationToken)
        {
            var filterModel = _requestBuilder.GetRequestBuilder(request.CommonRequest);
            return await _streetsService.GetStreets(filterModel.GetFilters(), request.CommonRequest, filterModel.GetSorts(), request.RouteId);
        }
    }

    public class GetStreetRequestDto
    {
        public ServerRowsRequest CommonRequest { get; set; }
        public int RouteId { get; set; }
        //public string? AreaId { get; set; }

    }
}

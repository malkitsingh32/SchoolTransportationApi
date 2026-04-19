using Application.Abstraction.Services;
using Application.Common.Dtos;
using Application.Common.Interfaces.Common;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.Driver;
using MediatR;

namespace Application.Handler.Drivers.Queries.GetDrivers
{
    public class GetDriversQueryHandler : IRequestHandler<GetDriversQuery, CommonResultResponseDto<PaginatedList<GetDriversResponseDto>>>
    {
        private readonly IDriversService _driversService;
        private readonly IRequestBuilder _requestBuilder;

        public GetDriversQueryHandler(IDriversService driversService, IRequestBuilder requestBuilder)
        {
            _driversService = driversService;
            _requestBuilder = requestBuilder;

        }
        public async Task<CommonResultResponseDto<PaginatedList<GetDriversResponseDto>>> Handle(GetDriversQuery request, CancellationToken cancellationToken)
        {
            var filterModel = _requestBuilder.GetRequestBuilder(request.CommonRequest);
            return await _driversService.GetDrivers(filterModel.GetFilters(), request.CommonRequest, filterModel.GetSorts(), request.RouteId);
        }
    }

    public class GetDriversRequestDto
    {
        public ServerRowsRequest CommonRequest { get; set; }
        public int RouteId { get; set; }

    }
}

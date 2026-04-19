using Application.Abstraction.Services;
using Application.Common.Dtos;
using Application.Common.Interfaces.Common;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.Driver;
using MediatR;

namespace Application.Handler.Drivers.Queries.GetDriversBalanceHistory
{
    public class GetDriversBalanceHistoryQueryHandler : IRequestHandler<GetDriversBalanceHistoryQuery, CommonResultResponseDto<PaginatedList<GetDriversBalanceHistoryResponseDto>>>
    {
        private readonly IDriversService _driversService;
        private readonly IRequestBuilder _requestBuilder;

        public GetDriversBalanceHistoryQueryHandler(IDriversService driversService, IRequestBuilder requestBuilder)
        {
            _driversService = driversService;
            _requestBuilder = requestBuilder;

        }
        public async Task<CommonResultResponseDto<PaginatedList<GetDriversBalanceHistoryResponseDto>>> Handle(GetDriversBalanceHistoryQuery request, CancellationToken cancellationToken)
        {
            var filterModel = _requestBuilder.GetRequestBuilder(request.CommonRequest);
            return await _driversService.GetDriversBalanceHistory(filterModel.GetFilters(), request.CommonRequest, filterModel.GetSorts(), request.DriverId);
        }
    }

    public class GetDriversBalanceHistoryRequestDto
    {
        public ServerRowsRequest CommonRequest { get; set; }
        public int DriverId { get; set; }

    }
}

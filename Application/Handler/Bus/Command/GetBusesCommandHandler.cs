using Application.Abstraction.Services;
using Application.Common.Dtos;
using Application.Common.Interfaces.Common;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.Bus;
using MediatR;

namespace Application.Handler.Bus.Command
{
    public class GetBusesCommandHandler : IRequestHandler<GetBusesCommand, CommonResultResponseDto<PaginatedList<GetBusesResponseDto>>>
    {
        private readonly IBusDetailsService _busService;
        private readonly IRequestBuilder _requestBuilder;
        public GetBusesCommandHandler(IBusDetailsService busService, IRequestBuilder requestBuilder)
        {
            _busService = busService;
            _requestBuilder = requestBuilder;
        }

        public async Task<CommonResultResponseDto<PaginatedList<GetBusesResponseDto>>> Handle(GetBusesCommand request, CancellationToken cancellationToken)
        {
            var filterModel = _requestBuilder.GetRequestBuilder(request.CommonRequest);
            return await _busService.GetBuses(filterModel.GetFilters(), request.CommonRequest, filterModel.GetSorts(), request.RouteId);
        }
    }
    public class GetBusesDetailsRequestDto
    {
        public ServerRowsRequest CommonRequest { get; set; }
        public int RouteId { get; set; }

    }
}

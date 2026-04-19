using Application.Abstraction.Services;
using DTO.Response;
using DTO.Response.BusStatus;
using MediatR;

namespace Application.Handler.Bus.Queries.GetBusStatus
{
    public class GetBusStatusQueryHandler : IRequestHandler<GetBusStatusQuery, CommonResultResponseDto<IList<GetBusStatusResponseDto>>>
    {
        private readonly IBusDetailsService _busDetailsService;

        public GetBusStatusQueryHandler(IBusDetailsService busDetailsService)
        {
            _busDetailsService = busDetailsService;
        }

        public async Task<CommonResultResponseDto<IList<GetBusStatusResponseDto>>> Handle(GetBusStatusQuery request, CancellationToken cancellationToken)
        {
            return await _busDetailsService.GetBusStatus();
        }
    }
}

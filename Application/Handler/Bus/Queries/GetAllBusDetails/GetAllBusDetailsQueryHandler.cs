using Application.Abstraction.Services;
using DTO.Response;
using DTO.Response.Bus;
using MediatR;

namespace Application.Handler.Bus.Queries.GetAllBusDetails
{
    public class GetAllBusDetailsQueryHandler : IRequestHandler<GetAllBusDetailsQuery, CommonResultResponseDto<IList<GetBusesResponseDto>>>
    {
        private readonly IBusDetailsService _busDetailsService;

        public GetAllBusDetailsQueryHandler(IBusDetailsService busDetailsService)
        {
            _busDetailsService = busDetailsService;
        }
        public async Task<CommonResultResponseDto<IList<GetBusesResponseDto>>> Handle(GetAllBusDetailsQuery request, CancellationToken cancellationToken)
        {
            return await _busDetailsService.GetAllBusDetails();
        }
    }
}

using Application.Abstraction.Services;
using DTO.Response;
using DTO.Response.Routes;
using MediatR;

namespace Application.Handler.Routes.Queries.GetAddress
{
    public class GetAddressQueryHandler : IRequestHandler<GetAddressQuery, CommonResultResponseDto<IList<GetAddressResponseDto>>>
    {
        private readonly IRoutesService _routesService;

        public GetAddressQueryHandler(IRoutesService routesService)
        {
            _routesService = routesService;


        }
        public async Task<CommonResultResponseDto<IList<GetAddressResponseDto>>> Handle(GetAddressQuery request, CancellationToken cancellationToken)
        {
            return await _routesService.GetAddress();
        }
    }
}

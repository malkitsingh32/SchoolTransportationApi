using Application.Abstraction.Services;
using DTO.Response;
using DTO.Response.Routes;
using MediatR;

namespace Application.Handler.Routes.Queries.GetRoutesList
{
    public class GetRoutesListQueryHandler : IRequestHandler<GetRoutesListQuery, CommonResultResponseDto<IList<GetRoutesListResponseDto>>>
    {
        private readonly IRoutesService _routesService;

        public GetRoutesListQueryHandler(IRoutesService routesService)
        {
            _routesService = routesService;
        }

        public async Task<CommonResultResponseDto<IList<GetRoutesListResponseDto>>> Handle(GetRoutesListQuery request, CancellationToken cancellationToken)
        {
            return await _routesService.GetRoutesList();
        }
    }
}

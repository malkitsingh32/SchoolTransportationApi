using Application.Abstraction.Services;
using DTO.Response;
using DTO.Response.Routes;
using MediatR;

namespace Application.Handler.Routes.Queries.GetRoutesDetailList
{
    public class GetRoutesDetailListQueryHandler : IRequestHandler<GetRoutesDetailListQuery, CommonResultResponseDto<IList<GetRoutesDetailListResponseDto>>>
    {
        private readonly IRoutesService _routesService;

        public GetRoutesDetailListQueryHandler(IRoutesService routesService)
        {
            _routesService = routesService;
        }

        public async Task<CommonResultResponseDto<IList<GetRoutesDetailListResponseDto>>> Handle(GetRoutesDetailListQuery request, CancellationToken cancellationToken)
        {
            return await _routesService.GetRoutesDetailList();
        }
    }
}

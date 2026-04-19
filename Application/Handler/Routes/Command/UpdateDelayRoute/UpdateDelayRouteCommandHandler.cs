using Application.Abstraction.Services;
using DTO.Request.Routes;
using DTO.Response;
using Mapster;
using MediatR;

namespace Application.Handler.Routes.Command.UpdateDelayRoute
{
    public class UpdateDelayRouteCommandHandler : IRequestHandler<UpdateDelayRouteCommand, CommonResultResponseDto<string>>
    {
        private readonly IRoutesService _routesService;
        public UpdateDelayRouteCommandHandler(IRoutesService routesService)
        {
            _routesService = routesService;
        }

        public async Task<CommonResultResponseDto<string>> Handle(UpdateDelayRouteCommand updateDelayRouteCommand, CancellationToken cancellationToken)
        {
            return await _routesService.UpdateDelayRoute(updateDelayRouteCommand.Adapt<UpdateDelayRouteDto>());

        }
    }
}

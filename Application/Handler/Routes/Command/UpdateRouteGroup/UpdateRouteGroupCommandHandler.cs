using Application.Abstraction.Services;
using DTO.Request.Routes;
using DTO.Response;
using Mapster;
using MediatR;

namespace Application.Handler.Routes.Command.UpdateRouteGroup
{
    public class UpdateRouteGroupCommandHandler : IRequestHandler<UpdateRouteGroupCommand, CommonResultResponseDto<string>>
    {
        private readonly IRoutesService _routesService;
        public UpdateRouteGroupCommandHandler(IRoutesService routesService)
        {
            _routesService = routesService;
        }

        public async Task<CommonResultResponseDto<string>> Handle(UpdateRouteGroupCommand updateRouteGroupCommand, CancellationToken cancellationToken)
        {
            return await _routesService.UpdateRouteGroup(updateRouteGroupCommand.Adapt<UpdateRouteGroupDto>());
        }
    }
}

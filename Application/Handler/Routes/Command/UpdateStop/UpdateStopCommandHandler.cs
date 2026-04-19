using Application.Abstraction.Services;
using DTO.Request.Routes;
using DTO.Response;
using Mapster;
using MediatR;

namespace Application.Handler.Routes.Command.UpdateStop
{
    public class UpdateStopCommandHandler : IRequestHandler<UpdateStopCommand, CommonResultResponseDto<string>>
    {
        private readonly IRoutesService _routesService;
        public UpdateStopCommandHandler(IRoutesService routesService)
        {
            _routesService = routesService;
        }

        public async Task<CommonResultResponseDto<string>> Handle(UpdateStopCommand updateStopCommand, CancellationToken cancellationToken)
        {
            return await _routesService.UpdateStop(updateStopCommand.Adapt<UpdateStopDto>());
        }
    }
}

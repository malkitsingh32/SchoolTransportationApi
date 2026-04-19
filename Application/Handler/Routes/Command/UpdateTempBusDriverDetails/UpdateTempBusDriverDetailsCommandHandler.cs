using Application.Abstraction.Services;
using DTO.Request.Routes;
using DTO.Response;
using Mapster;
using MediatR;

namespace Application.Handler.Routes.Command.UpdateTempBusDriverDetails
{
    public class UpdateTempBusDriverDetailsCommandHandler : IRequestHandler<UpdateTempBusDriverDetailsCommand, CommonResultResponseDto<string>>
    {
        private readonly IRoutesService _routesService;
        public UpdateTempBusDriverDetailsCommandHandler(IRoutesService routesService)
        {
            _routesService = routesService;
        }

        public async Task<CommonResultResponseDto<string>> Handle(UpdateTempBusDriverDetailsCommand updateTempBusDriverDetailsCommand, CancellationToken cancellationToken)
        {
            return await _routesService.UpdateTempBusDriverDetails(updateTempBusDriverDetailsCommand.Adapt<UpdateTempBusDriverDetailsDto>());
        }
    }
}

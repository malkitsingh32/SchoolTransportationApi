using Application.Abstraction.Services;
using DTO.Request.Street;
using DTO.Response;
using Mapster;
using MediatR;

namespace Application.Handler.Streets.Command.UpdateStreetRouteMapping
{
    public class UpdateStreetRouteMappingCommandHandler : IRequestHandler<UpdateStreetRouteMappingCommand, CommonResultResponseDto<string>>
    {
        private readonly IStreetsService _streetsService;

        public UpdateStreetRouteMappingCommandHandler(IStreetsService streetsService)
        {
            _streetsService = streetsService;
        }

        public async Task<CommonResultResponseDto<string>> Handle(UpdateStreetRouteMappingCommand updateStreetRouteMappingCommand, CancellationToken cancellationToken)
        {
            return await _streetsService.UpdateStreetRouteMapping(updateStreetRouteMappingCommand.Adapt<UpdateStreetRouteMappingDto>());
        }
    }
}

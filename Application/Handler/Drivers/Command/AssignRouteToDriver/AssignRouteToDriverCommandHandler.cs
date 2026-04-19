using Application.Abstraction.Services;
using DTO.Request.Routes;
using DTO.Response;
using Mapster;
using MediatR;

namespace Application.Handler.Drivers.Command.AssignRouteToDriver
{
    public class AssignRouteToDriverCommandHandler : IRequestHandler<AssignRouteToDriverCommand, CommonResultResponseDto<string>>
    {
        private readonly IDriversService _driversService;
        public AssignRouteToDriverCommandHandler(IDriversService driversService)
        {
            _driversService = driversService;
        }

        public async Task<CommonResultResponseDto<string>> Handle(AssignRouteToDriverCommand assignRouteToDriverCommand, CancellationToken cancellationToken)
        {
            return await _driversService.AssignRouteToDriver(assignRouteToDriverCommand.Adapt<AssignRouteToDriverRequestDto>());
          
        }
    }
}

using Application.Abstraction.Services;
using DTO.Response;
using DTO.Response.Driver;
using Mapster;
using MediatR;

namespace Application.Handler.Drivers.Command.DeleteBusAndDriverRoute
{
    public class DeleteBusAndDriverRouteCommandHandler : IRequestHandler<DeleteBusAndDriverRouteCommand, CommonResultResponseDto<string>>
    {
        private readonly IDriversService _driversService;
        public DeleteBusAndDriverRouteCommandHandler(IDriversService driversService)
        {
            _driversService = driversService;
        }

        public async Task<CommonResultResponseDto<string>> Handle(DeleteBusAndDriverRouteCommand deleteBusAndDriverRouteCommand, CancellationToken cancellationToken)
        {
            return await _driversService.DeleteBusAndDriverRoute(deleteBusAndDriverRouteCommand.Adapt<DeleteBusAndDriverRouteRequestDto>());
        }
    }
}

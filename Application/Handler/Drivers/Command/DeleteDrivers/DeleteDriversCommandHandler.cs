using Application.Abstraction.Services;
using DTO.Response;
using MediatR;

namespace Application.Handler.Drivers.Command.DeleteDrivers
{
    public class DeleteDriversCommandHandler : IRequestHandler<DeleteDriversCommand, CommonResultResponseDto<string>>
    {
        private readonly IDriversService _driversService;
        public DeleteDriversCommandHandler(IDriversService driversService)
        {
            _driversService = driversService;
        }
        public async Task<CommonResultResponseDto<string>> Handle(DeleteDriversCommand deleteDriversCommand, CancellationToken cancellationToken)
        {
            return await _driversService.DeleteDrivers(deleteDriversCommand.Id,deleteDriversCommand.IsFromRoute,deleteDriversCommand.RouteId);
        }
    }
}

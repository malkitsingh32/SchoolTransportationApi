using Application.Abstraction.Services;
using DTO.Response;
using MediatR;

namespace Application.Handler.Routes.Command.UndoRoutes
{
    public class UndoRoutesCommandHandler : IRequestHandler<UndoRoutesCommand, CommonResultResponseDto<string>>
    {
        private readonly IRoutesService _routesService;
        public UndoRoutesCommandHandler(IRoutesService routesService)
        {
            _routesService = routesService;
        }
        public async Task<CommonResultResponseDto<string>> Handle(UndoRoutesCommand undoRoutesCommand, CancellationToken cancellationToken)
        {
            return await _routesService.UndoRoutes(undoRoutesCommand.Id, undoRoutesCommand.RouteDate);
        }
    }
}

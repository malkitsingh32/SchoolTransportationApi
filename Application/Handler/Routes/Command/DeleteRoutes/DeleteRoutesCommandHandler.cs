using Application.Abstraction.Services;
using DTO.Response;
using MediatR;

namespace Application.Handler.Routes.Command.DeleteRoutes
{
    public class DeleteRoutesCommandHandler : IRequestHandler<DeleteRoutesCommand, CommonResultResponseDto<string>>
    {
        private readonly IRoutesService _routesService;
        public DeleteRoutesCommandHandler(IRoutesService routesService)
        {
            _routesService = routesService;
        }
        public async Task<CommonResultResponseDto<string>> Handle(DeleteRoutesCommand deleteRoutesCommand, CancellationToken cancellationToken)
        {
            return await _routesService.DeleteRoutes(deleteRoutesCommand.Id,deleteRoutesCommand.DeleteAll,deleteRoutesCommand.RouteDate);
        }
    }
}

using Application.Abstraction.Services;
using DTO.Response;
using MediatR;

namespace Application.Handler.Routes.Command.DeleteStudentFromRoute
{
    public class DeleteStudentFromRouteCommandHandler : IRequestHandler<DeleteStudentFromRouteCommand, CommonResultResponseDto<string>>
    {
        private readonly IRoutesService _routesService;
        public DeleteStudentFromRouteCommandHandler(IRoutesService routesService)
        {
            _routesService = routesService;
        }
        public async Task<CommonResultResponseDto<string>> Handle(DeleteStudentFromRouteCommand deleteStudentFromRouteCommand, CancellationToken cancellationToken)
        {
            return await _routesService.DeleteStudentFromRoute(deleteStudentFromRouteCommand.StudentId, deleteStudentFromRouteCommand.RouteId);
        }
    }
}

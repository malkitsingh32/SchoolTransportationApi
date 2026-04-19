using Application.Abstraction.Services;
using DTO.Request.Routes;
using DTO.Response;
using Mapster;
using MediatR;

namespace Application.Handler.Routes.Command.CheckRouteTypeStudent
{
    public class CheckRouteTypeStudentCommandHandler : IRequestHandler<CheckRouteTypeStudentCommand, CommonResultResponseDto<IList<RouteTypeStudentResponseDto>>>
    {
        private readonly IRoutesService _routesService;
        public CheckRouteTypeStudentCommandHandler(IRoutesService routesService)
        {
            _routesService = routesService;
        }

        public async Task<CommonResultResponseDto<IList<RouteTypeStudentResponseDto>>> Handle(CheckRouteTypeStudentCommand checkRouteTypeStudentCommand, CancellationToken cancellationToken)
        {
            var user = await _routesService.CheckRouteTypeStudent(checkRouteTypeStudentCommand.Adapt<CheckRouteTypeStudentRequestDto>());
            return await Task.FromResult(user);
        }
    }
}

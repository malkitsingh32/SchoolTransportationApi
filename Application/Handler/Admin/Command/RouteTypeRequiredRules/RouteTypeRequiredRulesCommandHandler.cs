using Application.Abstraction.Services;
using DTO.Request.Admin;
using DTO.Response;
using Mapster;
using MediatR;

namespace Application.Handler.Admin.Command.RouteTypeRequiredRules
{
    public class RouteTypeRequiredRulesCommandHandler : IRequestHandler<RouteTypeRequiredRulesCommand, CommonResultResponseDto<string>>
    {
        private readonly IAdminService _adminService;
        public RouteTypeRequiredRulesCommandHandler(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<CommonResultResponseDto<string>> Handle(RouteTypeRequiredRulesCommand routeTypeRequiredRulesCommand, CancellationToken cancellationToken)
        {
            return await _adminService.SetRouteTypeRequiredRules(routeTypeRequiredRulesCommand.Adapt<RouteTypeRequiredRulesDto>());
        }
    }
}

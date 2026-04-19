using Application.Abstraction.Services;
using DTO.Response;
using DTO.Response.SystemValues;
using MediatR;

namespace Application.Handler.SystemValues.Queries.GetRouteTypeRequiredRules
{
    public class GetRouteTypeRequiredRulesQueryHandler : IRequestHandler<GetRouteTypeRequiredRulesQuery, CommonResultResponseDto<IList<GetRouteTypeRequiredRulesResponseDto>>>
    {

        private readonly IAdminService _adminService;

        public GetRouteTypeRequiredRulesQueryHandler(IAdminService adminService)
        {
            _adminService = adminService;
        }
        public async Task<CommonResultResponseDto<IList<GetRouteTypeRequiredRulesResponseDto>>> Handle(GetRouteTypeRequiredRulesQuery getRouteTypeRequiredRulesQuery, CancellationToken cancellationToken)
        {
            return await _adminService.GetRouteTypeRequiredRules(getRouteTypeRequiredRulesQuery.RouteTypeId);
        }
    }
}

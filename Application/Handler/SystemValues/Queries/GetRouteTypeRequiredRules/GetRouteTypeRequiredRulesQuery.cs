using DTO.Response;
using DTO.Response.SystemValues;
using MediatR;

namespace Application.Handler.SystemValues.Queries.GetRouteTypeRequiredRules
{
    public class GetRouteTypeRequiredRulesQuery : IRequest<CommonResultResponseDto<IList<GetRouteTypeRequiredRulesResponseDto>>>
    {
        public int? RouteTypeId { get; set; }
    }
}

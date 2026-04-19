using DTO.Response;
using MediatR;

namespace Application.Handler.Admin.Command.RouteTypeRequiredRules
{
    public class RouteTypeRequiredRulesCommand : IRequest<CommonResultResponseDto<string>>
    {
        public int RouteTypeId { get; set; }
        public List<RouteTypeRule> Rules { get; set; } = new List<RouteTypeRule>();
        public int CreatedBy { get; set; }
    }

    public class RouteTypeRule
    {
        public int SchoolId { get; set; }
        public int GenderId { get; set; }
        public int GradeId { get; set; }
    }
}

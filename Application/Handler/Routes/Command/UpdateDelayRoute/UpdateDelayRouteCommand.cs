using DTO.Response;
using MediatR;

namespace Application.Handler.Routes.Command.UpdateDelayRoute
{
    public class UpdateDelayRouteCommand : IRequest<CommonResultResponseDto<string>>
    {
        public string School { get; set; }
        public string Time { get; set; }
        public string Gender { get; set; }
        public string Grade { get; set; }
        public string RouteId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}

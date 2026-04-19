using DTO.Request.Routes;
using DTO.Response;
using MediatR;

namespace Application.Handler.Routes.Command.CheckRouteTypeStudent
{
    public class CheckRouteTypeStudentCommand : IRequest<CommonResultResponseDto<IList<RouteTypeStudentResponseDto>>>
    {
        public int Type { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public List<StudentBusDto> Students { get; set; }
    }
}

using DTO.Request.Routes;
using DTO.Response;
using MediatR;

namespace Application.Handler.Routes.Command.UpdateTodayDriver
{
    public class UpdateTodayDriverCommand : IRequest<CommonResultResponseDto<string>>
    {
        public int DriverID { get; set; }
        public List<RouteDetailListDto> RouteJson { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}

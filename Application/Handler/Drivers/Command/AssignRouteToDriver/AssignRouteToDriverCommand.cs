using DTO.Response;
using MediatR;

namespace Application.Handler.Drivers.Command.AssignRouteToDriver
{
    public class AssignRouteToDriverCommand : IRequest<CommonResultResponseDto<string>>
    {
        public int? DriverId { get; set; }
        public int? RouteId { get; set; }
        public DateTime? RouteDate { get; set; }
    }
}

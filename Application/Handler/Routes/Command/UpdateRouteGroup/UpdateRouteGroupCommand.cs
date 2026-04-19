using DTO.Response;
using MediatR;

namespace Application.Handler.Routes.Command.UpdateRouteGroup
{
    public class UpdateRouteGroupCommand : IRequest<CommonResultResponseDto<string>>
    {
        public int RouteId { get; set; }
        public int NewDriverId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

    }
}

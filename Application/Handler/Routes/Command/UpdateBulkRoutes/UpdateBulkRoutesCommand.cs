using DTO.Response;
using MediatR;

namespace Application.Handler.Routes.Command.UpdateBulkRoutes
{
    public class UpdateBulkRoutesCommand : IRequest<CommonResultResponseDto<string>>
    {
        public RouteInfo[] Route { get; set; }
        public string? Time { get; set; }
        public int? TodaysDriver { get; set; }
        public int? RouteType { get; set; }
    }

    public class RouteInfo
    {
        public DateTime? RouteDate { get; set; }
        public Guid? RouteGroupId { get; set; }
    }
}

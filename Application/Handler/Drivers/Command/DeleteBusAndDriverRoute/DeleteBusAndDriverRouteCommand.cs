using DTO.Response;
using MediatR;

namespace Application.Handler.Drivers.Command.DeleteBusAndDriverRoute
{
    public class DeleteBusAndDriverRouteCommand : IRequest<CommonResultResponseDto<string>>
    {
        public int RouteDetailID { get; set; }
        public bool IsDeleteDriver { get; set; }
    }
}

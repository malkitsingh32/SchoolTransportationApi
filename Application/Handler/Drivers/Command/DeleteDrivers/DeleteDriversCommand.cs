using DTO.Response;
using MediatR;

namespace Application.Handler.Drivers.Command.DeleteDrivers
{
    public class DeleteDriversCommand : IRequest<CommonResultResponseDto<string>>
    {
        public int Id { get; set; }
        public bool IsFromRoute { get; set; }
        public int? RouteId { get; set; }
    }
}

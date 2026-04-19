using DTO.Response;
using MediatR;

namespace Application.Handler.Routes.Command.UndoRoutes
{
    public class UndoRoutesCommand : IRequest<CommonResultResponseDto<string>>
    {
        public int Id { get; set; }
        public DateTime? RouteDate { get; set; }
    }
}

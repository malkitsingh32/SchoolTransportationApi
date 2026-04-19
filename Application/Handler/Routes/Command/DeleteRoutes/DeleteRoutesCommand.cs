using DTO.Response;
using MediatR;

namespace Application.Handler.Routes.Command.DeleteRoutes
{
    public class DeleteRoutesCommand : IRequest<CommonResultResponseDto<string>>
    {
        public int Id { get; set; }
        public int? DeleteAll { get; set; }
        public DateTime? RouteDate { get; set; }
    }
}

using DTO.Response;
using MediatR;

namespace Application.Handler.Routes.Command.DeleteStudentFromRoute
{
    public class DeleteStudentFromRouteCommand : IRequest<CommonResultResponseDto<string>>
    {
        public int StudentId { get; set; }
        public int RouteId { get; set; }
    }
}


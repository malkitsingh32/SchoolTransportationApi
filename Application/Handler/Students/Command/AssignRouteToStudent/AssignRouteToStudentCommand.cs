using DTO.Request.Students;
using DTO.Response;
using MediatR;

namespace Application.Handler.Students.Command.AssignRouteToStudent
{
    public class AssignRouteToStudentCommand : IRequest<CommonResultResponseDto<string>>
    {
        public List<int> RouteId { get; set; }
        public List<AssignRouteStudentDto> Students { get; set; }
    }
}

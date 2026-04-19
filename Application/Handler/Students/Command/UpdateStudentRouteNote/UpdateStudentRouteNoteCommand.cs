using DTO.Response;
using MediatR;

namespace Application.Handler.Students.Command.UpdateStudentRouteNote
{
    public class UpdateStudentRouteNoteCommand : IRequest<CommonResultResponseDto<string>>
    {
        public int StudentId { get; set; }
        public int RouteId { get; set; }
        public string Note { get; set; }
    }
}

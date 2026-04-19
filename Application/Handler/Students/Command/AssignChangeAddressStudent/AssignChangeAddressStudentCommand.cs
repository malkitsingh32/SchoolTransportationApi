using DTO.Request.Students;
using DTO.Response;
using DTO.Response.Students;
using MediatR;

namespace Application.Handler.Students.Command.AssignChangeAddressStudent
{
    public class AssignChangeAddressStudentCommand : IRequest<CommonResultResponseDto<IList<AssignChangeAddressStudentResponseDto>>>
    {
        public List<int> RouteId { get; set; }
        public List<AssignRouteChangeAddressStudentDto> Students { get; set; }
        public bool IsAssignStudent { get; set; }

    }
}

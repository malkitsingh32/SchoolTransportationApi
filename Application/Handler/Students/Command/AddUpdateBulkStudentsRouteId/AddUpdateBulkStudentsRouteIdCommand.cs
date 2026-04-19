using DTO.Request.Routes;
using DTO.Request.Students;
using DTO.Response;
using MediatR;

namespace Application.Handler.Students.Command.AddUpdateBulkStudentsRouteId
{
    public class AddUpdateBulkStudentsRouteIdCommand : IRequest<CommonResultResponseDto<string>>
    {
        public List<StudentsDetailListDto> studentsDetailLists { get; set; }
        public bool AddStudent { get; set; }
        public List<OverrideStudentDto> OverrideStudentList { get; set; }
    }
}

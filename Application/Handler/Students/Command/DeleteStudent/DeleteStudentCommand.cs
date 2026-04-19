using DTO.Response;
using MediatR;

namespace Application.Handler.Students.Command.DeleteStudent
{
    public class DeleteStudentCommand : IRequest<CommonResultResponseDto<string>>
    {
        public int Id { get; set; }
        public bool IsFromRoute { get; set; }
        public int Type { get; set; }
    }
}

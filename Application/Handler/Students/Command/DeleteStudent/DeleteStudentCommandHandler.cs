using Application.Abstraction.Services;
using DTO.Response;
using MediatR;

namespace Application.Handler.Students.Command.DeleteStudent
{
    public class DeleteStudentCommandHandler : IRequestHandler<DeleteStudentCommand, CommonResultResponseDto<string>>
    {
        private readonly IStudentsService _studentsService;
        public DeleteStudentCommandHandler(IStudentsService studentsService)
        {
            _studentsService = studentsService;
        }

        public async Task<CommonResultResponseDto<string>> Handle(DeleteStudentCommand deleteStudentCommand, CancellationToken cancellationToken)
        {
            return await _studentsService.DeleteStudent(deleteStudentCommand.Id, deleteStudentCommand.IsFromRoute, deleteStudentCommand.Type);
        }
    }
}

using Application.Abstraction.Services;
using DTO.Request.Students;
using DTO.Response;
using Mapster;
using MediatR;

namespace Application.Handler.Students.Command.AssignRouteToStudent
{
    public class AssignRouteToStudentCommandHandler : IRequestHandler<AssignRouteToStudentCommand, CommonResultResponseDto<string>>
    {
        private readonly IStudentsService _studentsService;
        public AssignRouteToStudentCommandHandler(IStudentsService studentsService)
        {
            _studentsService = studentsService;
        }
        public async Task<CommonResultResponseDto<string>> Handle(AssignRouteToStudentCommand assignRouteToStudentCommand, CancellationToken cancellationToken)
        {
            var user = await _studentsService.AssignRouteToStudent(assignRouteToStudentCommand.Adapt<AssignRouteToStudentRequestDto>());
            return await Task.FromResult(user);
        }
    }
}

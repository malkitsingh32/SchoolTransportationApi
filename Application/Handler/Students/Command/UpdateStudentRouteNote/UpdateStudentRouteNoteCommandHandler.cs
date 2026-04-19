using Application.Abstraction.Services;
using DTO.Request.Students;
using DTO.Response;
using Mapster;
using MediatR;

namespace Application.Handler.Students.Command.UpdateStudentRouteNote
{
    public class UpdateStudentRouteNoteCommandHandler : IRequestHandler<UpdateStudentRouteNoteCommand, CommonResultResponseDto<string>>
    {
        private readonly IStudentsService _studentsService;
        public UpdateStudentRouteNoteCommandHandler(IStudentsService studentsService)
        {
            _studentsService = studentsService;
        }

        public async Task<CommonResultResponseDto<string>> Handle(UpdateStudentRouteNoteCommand updateStudentRouteNoteCommand, CancellationToken cancellationToken)
        {
            return await _studentsService.UpdateStudentRouteNote(updateStudentRouteNoteCommand.Adapt<UpdateStudentRouteNoteDto>());
        }
    }
}

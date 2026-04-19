using Application.Abstraction.Services;
using MediatR;

namespace Application.Handler.Students.Command.DownloadStudentApplicationForm
{
    public class DownloadStudentApplicationFormCommandHandler : IRequestHandler<DownloadStudentApplicationFormCommand, Byte[]>
    {
        private readonly IStudentsService _studentsService;
        public DownloadStudentApplicationFormCommandHandler(IStudentsService studentsService)
        {
            _studentsService = studentsService;
        }

        public async Task<byte[]> Handle(DownloadStudentApplicationFormCommand downloadStudentApplicationForm, CancellationToken cancellationToken)
        {
            return await _studentsService.DownloadStudentApplicationForm(downloadStudentApplicationForm.StudentId);
        
    }
    }
}

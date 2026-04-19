using MediatR;

namespace Application.Handler.Students.Command.DownloadStudentApplicationForm
{
    public class DownloadStudentApplicationFormCommand : IRequest<Byte[]>
    {
        public int StudentId { get; set; }
    }
}

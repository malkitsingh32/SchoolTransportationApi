using DTO.Response.Students;

namespace Application.Abstraction.Services
{
    public interface ISmsService
    {
        Task SendBulkSmsAsync(IList<StudentSmsDto> message);
    }
}

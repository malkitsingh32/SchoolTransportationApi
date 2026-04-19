using DTO.Request.Notification;
using DTO.Response;
using DTO.Response.Notification;

namespace Application.Abstraction.Services
{
    public interface INotificationService
    {
        Task<CommonResultResponseDto<string>> SaveBulkMessage(SaveBulkMessageRequestDto saveBulkMessageRequestDto);
        Task<CommonResultResponseDto<IList<GetBulkMessagesListResponseDto>>> GetBulkMessages(SaveBulkMessageRequestDto saveBulkMessageRequestDto);

    }
}

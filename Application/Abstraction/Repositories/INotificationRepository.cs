using DTO.Request.Notification;
using DTO.Response.Notification;

namespace Application.Abstraction.Repositories
{
    public interface INotificationRepository
    {
        Task<int> SaveBulkMessage(SaveBulkMessageRequestDto saveBulkMessageRequestDto);
        Task<IList<GetBulkMessagesListResponseDto>> GetBulkMessages(SaveBulkMessageRequestDto saveBulkMessageRequestDto, int messageId);

    }
}

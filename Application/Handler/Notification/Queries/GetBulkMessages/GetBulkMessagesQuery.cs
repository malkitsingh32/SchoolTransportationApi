using DTO.Response;
using DTO.Response.Notification;
using MediatR;

namespace Application.Handler.Notification.Queries.GetBulkMessages
{
    public class GetBulkMessagesQuery : IRequest<CommonResultResponseDto<IList<GetBulkMessagesListResponseDto>>>
    {
        public string RecipientType { get; set; }
        public string MessageType { get; set; }
    }
}

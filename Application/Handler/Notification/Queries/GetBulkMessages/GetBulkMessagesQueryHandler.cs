using Application.Abstraction.Services;
using DTO.Request.Notification;
using DTO.Response;
using DTO.Response.Notification;
using Mapster;
using MediatR;

namespace Application.Handler.Notification.Queries.GetBulkMessages
{
    public class GetBulkMessagesQueryHandler : IRequestHandler<GetBulkMessagesQuery, CommonResultResponseDto<IList<GetBulkMessagesListResponseDto>>>
    {
        private readonly INotificationService _notificationService;
        public GetBulkMessagesQueryHandler(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task<CommonResultResponseDto<IList<GetBulkMessagesListResponseDto>>> Handle(GetBulkMessagesQuery request, CancellationToken cancellationToken)
        {
            return await _notificationService.GetBulkMessages(request.Adapt<SaveBulkMessageRequestDto>());
        }
    }
}

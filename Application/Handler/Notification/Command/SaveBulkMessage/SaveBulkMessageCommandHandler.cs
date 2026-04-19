using Application.Abstraction.Services;
using DTO.Request.Notification;
using DTO.Response;
using Mapster;
using MediatR;

namespace Application.Handler.Notification.Command.SaveBulkMessage
{
    public class SaveBulkMessageCommandHandler : IRequestHandler<SaveBulkMessageCommand, CommonResultResponseDto<string>>
    {
        private readonly INotificationService  _notificationService;
        public SaveBulkMessageCommandHandler(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }
    
        public async Task<CommonResultResponseDto<string>> Handle(SaveBulkMessageCommand saveBulkMessageCommand, CancellationToken cancellationToken)
        {
            return await _notificationService.SaveBulkMessage(saveBulkMessageCommand.Adapt<SaveBulkMessageRequestDto>());
        }
    }
}

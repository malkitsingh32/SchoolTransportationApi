using Application.Abstraction.Repositories;
using Application.Abstraction.Services;
using DTO.Request.Notification;
using DTO.Response;
using DTO.Response.Notification;
using Helper;
using Helper.Constant;

namespace Infrastructure.Implementation.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly TwilioService  _twilioService;
        public NotificationService(INotificationRepository notificationRepository, TwilioService twilioService)
        {
            _notificationRepository = notificationRepository;
            _twilioService = twilioService;
        }

        public async Task<CommonResultResponseDto<IList<GetBulkMessagesListResponseDto>>> GetBulkMessages(SaveBulkMessageRequestDto saveBulkMessageRequestDto)
        {
            //var bulkMessage = await _notificationRepository.GetBulkMessages(saveBulkMessageRequestDto);
            return CommonResultResponseDto<IList<GetBulkMessagesListResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, null);
        }

        public async Task<CommonResultResponseDto<string>> SaveBulkMessage(SaveBulkMessageRequestDto saveBulkMessageRequestDto)
        {
            var messageId = await _notificationRepository.SaveBulkMessage(saveBulkMessageRequestDto);

            if (messageId <= 0)
            {
                return CommonResultResponseDto<string>.Failure(new[] { "Something went wrong" }, null); 
            }

            var messages = await _notificationRepository.GetBulkMessages(saveBulkMessageRequestDto, messageId);

            if (messages == null || !messages.Any())
            {
                return CommonResultResponseDto<string>.Failure(new[] { "No recipients found" }, null); 
            }

            DateTime? scheduledUtc = null;

            if (saveBulkMessageRequestDto.ScheduledDateTime.HasValue)
            {
                scheduledUtc = DateTime.SpecifyKind(
                    saveBulkMessageRequestDto.ScheduledDateTime.Value,
                    DateTimeKind.Local
                ).ToUniversalTime();

                // Twilio limit: within 7 days
                if (scheduledUtc.Value > DateTime.UtcNow.AddDays(7))
                {
                    return CommonResultResponseDto<string>.Failure(
                        new[] { "Scheduled time must be within 7 days" }, null);
                }
            }


            const int batchSize = 50; // safe batch size for Twilio

            var validMessages = messages
                .Where(x => !string.IsNullOrWhiteSpace(x.PhoneNumber))
                .ToList();

            for (int i = 0; i < validMessages.Count; i += batchSize)
            {
                var batch = validMessages.Skip(i).Take(batchSize);

                var tasks = batch.Select(async item =>
                   await _twilioService.SendSmsAsync(item.PhoneNumber, item.MessageBody, scheduledUtc)
                );

                await Task.WhenAll(tasks);

                // Throttle between batches (important)
                await Task.Delay(1000); // 1 second
            }

            return CommonResultResponseDto<string>.Success(new[] { ActionStatusConstant.Created }, null, 0);
        }



    }
}

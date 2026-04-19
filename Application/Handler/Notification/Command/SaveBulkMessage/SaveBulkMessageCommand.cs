using DTO.Response;
using MediatR;

namespace Application.Handler.Notification.Command.SaveBulkMessage
{
    public class SaveBulkMessageCommand : IRequest<CommonResultResponseDto<string>>
    {
        public string RecipientType { get; set; }   
        public string MessageBody { get; set; }
        public string MessageType { get; set; }       
        public DateTime? ScheduledDateTime { get; set; }
        public int CreatedBy { get; set; }
    }
}

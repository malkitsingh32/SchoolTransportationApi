using DTO.Response;
using MediatR;

namespace Application.Handler.Drivers.Command.SendLinkToResetDriverPassword
{
    public class SendLinkToResetDriverPasswordCommand : IRequest<CommonResultResponseDto<string>>
    {
        public int DriverId { get; set; }
        public string? Email { get; set; }
    }
}

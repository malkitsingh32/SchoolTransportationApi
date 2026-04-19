using DTO.Response;
using MediatR;

namespace Application.Handler.Drivers.Command.SendOtpOnEmail
{
    public class SendOtpOnEmailQuery : IRequest<CommonResultResponseDto<string>>
    {
        public string Email { get; set; }
    }
}

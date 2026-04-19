using DTO.Response;
using MediatR;
namespace Application.Handler.Drivers.Command.CheckOTP
{
    public class CheckOTPQuery : IRequest<CommonResultResponseDto<Domain.Entities.Drivers>>
    {
        public string Email { get; set; }
        public int Otp { get; set; }
    }
}

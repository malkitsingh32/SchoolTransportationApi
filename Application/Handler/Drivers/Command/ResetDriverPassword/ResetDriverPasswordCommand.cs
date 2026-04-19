using DTO.Response;
using MediatR;

namespace Application.Handler.Drivers.Command.ResetDriverPassword
{
    public class ResetDriverPasswordCommand : IRequest<CommonResultResponseDto<string>>
    {
        public int DriverId { get; set; }
        public string? Email { get; set; }
        public string Password { get; set; }
    }
}
    
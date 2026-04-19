using Application.Abstraction.Services;
using DTO.Response;
using DTO.Response.Driver;
using Mapster;
using MediatR;

namespace Application.Handler.Drivers.Command.SendOtpOnEmail
{
    public class SendOtpOnEmailQueryHandler : IRequestHandler<SendOtpOnEmailQuery, CommonResultResponseDto<string>>
    {
        private readonly IDriversService _driversService;
        public SendOtpOnEmailQueryHandler(IDriversService driversService)
        {
            _driversService = driversService;
        }

        public async Task<CommonResultResponseDto<string>> Handle(SendOtpOnEmailQuery sendOtpOnEmailQuery, CancellationToken cancellationToken)
        {
            return await _driversService.SendOtpOnEmail(sendOtpOnEmailQuery.Adapt<SendOtpOnEmailRequestDto>());
        }
    }
}

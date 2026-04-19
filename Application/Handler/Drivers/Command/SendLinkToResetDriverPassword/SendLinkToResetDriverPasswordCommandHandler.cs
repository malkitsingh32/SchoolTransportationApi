using Application.Abstraction.Services;
using DTO.Response;
using DTO.Response.Driver;
using Mapster;
using MediatR;

namespace Application.Handler.Drivers.Command.SendLinkToResetDriverPassword
{
    public class SendLinkToResetDriverPasswordCommandHandler : IRequestHandler<SendLinkToResetDriverPasswordCommand, CommonResultResponseDto<string>>
    {
        private readonly IDriversService _driversService;
        public SendLinkToResetDriverPasswordCommandHandler(IDriversService driversService)
        {
            _driversService = driversService;
        }

        public async Task<CommonResultResponseDto<string>> Handle(SendLinkToResetDriverPasswordCommand sendLinkToResetDriverPasswordCommand, CancellationToken cancellationToken)
        {
            return await _driversService.SendLinkToResetDriverPassword(sendLinkToResetDriverPasswordCommand.Adapt<SendLinkToResetDriverPasswordDto>());
        }
    }
}

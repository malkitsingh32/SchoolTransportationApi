using Application.Abstraction.Services;
using DTO.Response;
using DTO.Response.Driver;
using Mapster;
using MediatR;

namespace Application.Handler.Drivers.Command.ResetDriverPassword
{
    public class ResetDriverPasswordCommandHandler : IRequestHandler<ResetDriverPasswordCommand, CommonResultResponseDto<string>>
    {
        private readonly IDriversService _driversService;
        public ResetDriverPasswordCommandHandler(IDriversService driversService)
        {
            _driversService = driversService;
        }

        public async Task<CommonResultResponseDto<string>> Handle(ResetDriverPasswordCommand  resetDriverPasswordCommand, CancellationToken cancellationToken)
        {
            return await _driversService.ResetDriverPassword(resetDriverPasswordCommand.Adapt<ResetDriverPasswordDto>());
            
        }
    }
}

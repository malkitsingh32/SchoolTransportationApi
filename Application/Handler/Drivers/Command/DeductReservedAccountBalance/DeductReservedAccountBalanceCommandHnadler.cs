using Application.Abstraction.Services;
using DTO.Request.Drivers;
using DTO.Response;
using Mapster;
using MediatR;

namespace Application.Handler.Drivers.Command.DeductReservedAccountBalance
{
    public class DeductReservedAccountBalanceCommandHnadler : IRequestHandler<DeductReservedAccountBalanceCommand, CommonResultResponseDto<string>>
    {
        private readonly IDriversService _driversService;
        public DeductReservedAccountBalanceCommandHnadler(IDriversService driversService)
        {
            _driversService = driversService;
        }

        public async Task<CommonResultResponseDto<string>> Handle(DeductReservedAccountBalanceCommand deductReservedAccountBalanceCommand, CancellationToken cancellationToken)
        {
            var user = await _driversService.DeductReservedAccountBalance(deductReservedAccountBalanceCommand.Adapt<DeductReservedAccountBalanceRequestDto>());
            return await Task.FromResult(user);
        }
    }
}

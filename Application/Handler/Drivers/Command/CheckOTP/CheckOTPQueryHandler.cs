
using Application.Abstraction.Services;
using DTO.Response;
using DTO.Response.Driver;
using Mapster;
using MediatR;

namespace Application.Handler.Drivers.Command.CheckOTP
{
    public class CheckOTPQueryHandler : IRequestHandler<CheckOTPQuery, CommonResultResponseDto<Domain.Entities.Drivers>>
    {
        private readonly IDriversService _driversService;
        public CheckOTPQueryHandler(IDriversService driversService)
        {
            _driversService = driversService;
        }

        public async Task<CommonResultResponseDto<Domain.Entities.Drivers>> Handle(CheckOTPQuery checkOTPQuery, CancellationToken cancellationToken)
        {
            return await _driversService.CheckOTP(checkOTPQuery.Adapt<CheckOTPRequestDto>());
        }
    }
}

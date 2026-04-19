using Application.Abstraction.Services;
using DTO.Request.Drivers;
using DTO.Response;
using DTO.Response.Bus;
using Mapster;
using MediatR;

namespace Application.Handler.Drivers.Command.AddUpdateDriversDetails
{
    public class AddUpdateDriversDetailsCommandHandler : IRequestHandler<AddUpdateDriversDetailsCommand, CommonResultResponseDto<CheckDriverHasBusResponseDto>>
    {
        private readonly IDriversService _driversService;
        public AddUpdateDriversDetailsCommandHandler(IDriversService driversService)
        {
            _driversService = driversService;
        }

        public async Task<CommonResultResponseDto<CheckDriverHasBusResponseDto>> Handle(AddUpdateDriversDetailsCommand addUpdateDriversDetailsCommand, CancellationToken cancellationToken)
        {
            var user = await _driversService.AddUpdateDriversDetails(addUpdateDriversDetailsCommand.Adapt<AddUpdateDriversDetailsRequestDto>());
            return await Task.FromResult(user);
        }
    }
}

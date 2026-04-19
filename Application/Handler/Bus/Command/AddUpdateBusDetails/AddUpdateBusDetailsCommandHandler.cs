using Application.Abstraction.Services;
using DTO.Request.BusDetails;
using DTO.Response;
using DTO.Response.Bus;
using Mapster;
using MediatR;

namespace Application.Handler.Bus.Command.AddUpdateBusDetails
{
    public class AddUpdateBusDetailsCommandHandler : IRequestHandler<AddUpdateBusDetailsCommand, CommonResultResponseDto<IList<CheckDriverHasBusResponseDto>>>
    {
        private readonly IBusDetailsService _busDetailsService;
        public AddUpdateBusDetailsCommandHandler(IBusDetailsService busDetailsService)
        {
            _busDetailsService = busDetailsService;
        }

        public async Task<CommonResultResponseDto<IList<CheckDriverHasBusResponseDto>>> Handle(AddUpdateBusDetailsCommand addUpdateBusDetailsCommand, CancellationToken cancellationToken)
        {
            var user = await _busDetailsService.AddUpdateBusDetails(addUpdateBusDetailsCommand.Adapt<AddUpdateBusDetailsRequestDto>());
            return await Task.FromResult(user);
        }
    }
}

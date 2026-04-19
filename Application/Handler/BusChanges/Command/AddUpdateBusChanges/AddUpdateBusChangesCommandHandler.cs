using Application.Abstraction.Services;
using DTO.Request.BusChanges;
using DTO.Response;
using Mapster;
using MediatR;

namespace Application.Handler.BusChanges.Command.AddUpdateBusChanges
{
    public class AddUpdateBusChangesCommandHandler : IRequestHandler<AddUpdateBusChangesCommand, CommonResultResponseDto<string>>
    {
        private readonly IBusChangesService _busChangesService;
        public AddUpdateBusChangesCommandHandler(IBusChangesService busChangesService)
        {
            _busChangesService = busChangesService;
        }

        public async Task<CommonResultResponseDto<string>> Handle(AddUpdateBusChangesCommand addUpdateBusChangesCommand, CancellationToken cancellationToken)
        {
            return  await _busChangesService.AddUpdateBusChanges(addUpdateBusChangesCommand.Adapt<AddUpdateBusChangesRequestDto>());
        }
    }
}

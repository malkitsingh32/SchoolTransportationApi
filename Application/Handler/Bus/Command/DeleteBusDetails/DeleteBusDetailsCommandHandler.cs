using Application.Abstraction.Services;
using DTO.Response;
using MediatR;

namespace Application.Handler.Bus.Command.DeleteBusDetails
{
    public class DeleteBusDetailsCommandHandler : IRequestHandler<DeleteBusDetailsCommand, CommonResultResponseDto<string>>
    {
        private readonly IBusDetailsService _busService;
        public DeleteBusDetailsCommandHandler(IBusDetailsService busService)
        {
            _busService = busService;
        }
        public async Task<CommonResultResponseDto<string>> Handle(DeleteBusDetailsCommand deleteBusDetailsCommand, CancellationToken cancellationToken)
        {
            return await _busService.DeleteBusDetails(deleteBusDetailsCommand.Id);
        }
    }
}

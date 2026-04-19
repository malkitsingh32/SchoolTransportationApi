using Application.Abstraction.Services;
using DTO.Response;
using MediatR;

namespace Application.Handler.Streets.Command.DeleteStreet
{
    public class DeleteStreetsCommandHandler : IRequestHandler<DeleteStreetsCommand, CommonResultResponseDto<string>>
    {
        private readonly IStreetsService _streetsService;
        public DeleteStreetsCommandHandler(IStreetsService streetsService)
        {
            _streetsService = streetsService;
        }
        public async Task<CommonResultResponseDto<string>> Handle(DeleteStreetsCommand deleteStreetsCommand, CancellationToken cancellationToken)
        {
            return await _streetsService.DeleteStreets(deleteStreetsCommand.Id);
        }
    }
}

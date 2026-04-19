using Application.Abstraction.Services;
using DTO.Request.Street;
using DTO.Response;
using Mapster;
using MediatR;

namespace Application.Handler.Streets.Command.AddUpdateStreets
{
    public class AddUpdateStreetsCommandHandler : IRequestHandler<AddUpdateStreetsCommand, CommonResultResponseDto<string>>
    {
        private readonly IStreetsService _streetsService;

        public AddUpdateStreetsCommandHandler(IStreetsService streetsService)
        {
            _streetsService = streetsService;
        }
        public async Task<CommonResultResponseDto<string>> Handle(AddUpdateStreetsCommand addUpdateStreetsCommand, CancellationToken cancellationToken)
        {
            var user = await _streetsService.AddUpdateStreets(addUpdateStreetsCommand.Adapt<AddUpdateStreetsDto>());
            return await Task.FromResult(user);
        }
    }
}

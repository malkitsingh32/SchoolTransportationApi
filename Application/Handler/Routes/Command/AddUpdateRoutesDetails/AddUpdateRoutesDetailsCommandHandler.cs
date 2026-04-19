using Application.Abstraction.Services;
using DTO.Request.Routes;
using DTO.Response;
using Mapster;
using MediatR;

namespace Application.Handler.Routes.Command.AddUpdateRoutesDetails
{
    public class AddUpdateRoutesDetailsCommandHandler : IRequestHandler<AddUpdateRoutesDetailsCommand, CommonResultResponseDto<string>>
    {
        private readonly IRoutesService _routesService;
        public AddUpdateRoutesDetailsCommandHandler(IRoutesService routesService)
        {
            _routesService = routesService;
        }

        public async Task<CommonResultResponseDto<string>> Handle(AddUpdateRoutesDetailsCommand addUpdateRoutesDetailsCommand, CancellationToken cancellationToken)
        {
            var user = await _routesService.AddUpdateRoutesDetails(addUpdateRoutesDetailsCommand.Adapt<AddUpdateRoutesDetailsRequestDto>());
            return await Task.FromResult(user);
        }
    }
}

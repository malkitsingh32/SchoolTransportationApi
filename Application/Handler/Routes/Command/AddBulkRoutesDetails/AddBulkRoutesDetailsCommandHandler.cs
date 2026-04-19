using Application.Abstraction.Services;
using DTO.Request.Routes;
using DTO.Response;
using Mapster;
using MediatR;

namespace Application.Handler.Routes.Command.AddBulkRoutesDetails
{
    public class AddBulkRoutesDetailsCommandHandler : IRequestHandler<AddBulkRoutesDetailsCommand, CommonResultResponseDto<IList<string>>>
    {
        private readonly IRoutesService _routesService;
        public AddBulkRoutesDetailsCommandHandler(IRoutesService routesService)
        {
            _routesService = routesService;
        }

        public async Task<CommonResultResponseDto<IList<string>>> Handle(AddBulkRoutesDetailsCommand addBulkRoutesDetailsCommand, CancellationToken cancellationToken)
        {
            var user = await _routesService.AddBulkRoutesDetails(addBulkRoutesDetailsCommand.Adapt<AddBulkRoutesDetailsRequestDto>());
            return await Task.FromResult(user);
        }
    }
}

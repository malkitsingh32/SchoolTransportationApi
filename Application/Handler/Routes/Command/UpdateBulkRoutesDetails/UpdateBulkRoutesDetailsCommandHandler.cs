using Application.Abstraction.Services;
using DTO.Request.Routes;
using DTO.Response;
using Mapster;
using MediatR;

namespace Application.Handler.Routes.Command.UpdateBulkRoutesDetails
{
    public class UpdateBulkRoutesDetailsCommandHandler : IRequestHandler<UpdateBulkRoutesDetailsCommand, CommonResultResponseDto<string>>
    {
        private readonly IRoutesService _routesService;
        public UpdateBulkRoutesDetailsCommandHandler(IRoutesService routesService)
        {
            _routesService = routesService;
        }

        public async Task<CommonResultResponseDto<string>> Handle(UpdateBulkRoutesDetailsCommand updateBulkRoutesDetailsCommand, CancellationToken cancellationToken)
        {
            var user = await _routesService.UpdateBulkRoutesDetails(updateBulkRoutesDetailsCommand.Adapt<UpdateBulkRoutesDetailsRequestDto>());
            return await Task.FromResult(user);
        }
    }
}

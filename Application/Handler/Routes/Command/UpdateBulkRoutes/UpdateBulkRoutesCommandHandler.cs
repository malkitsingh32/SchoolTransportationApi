using Application.Abstraction.Services;
using DTO.Request.Routes;
using DTO.Response;
using Mapster;
using MediatR;

namespace Application.Handler.Routes.Command.UpdateBulkRoutes
{
    public class UpdateBulkRoutesCommandHandler : IRequestHandler<UpdateBulkRoutesCommand, CommonResultResponseDto<string>>
    {
        private readonly IRoutesService _routesService;
        public UpdateBulkRoutesCommandHandler(IRoutesService routesService)
        {
            _routesService = routesService;
        }

        public async Task<CommonResultResponseDto<string>> Handle(UpdateBulkRoutesCommand updateBulkRoutesCommand, CancellationToken cancellationToken)
        {
            var user = await _routesService.AddUpdateBulkRoutes(updateBulkRoutesCommand.Adapt<UpdateBulkRoutesRequestDto>());
            return await Task.FromResult(user);
        }
    }
}

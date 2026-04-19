using Application.Abstraction.Services;
using DTO.Request.Routes;
using DTO.Response;
using Mapster;
using MediatR;

namespace Application.Handler.Routes.Command.UpdateTodayDriver
{
    public class UpdateTodayDriverCommandHandler : IRequestHandler<UpdateTodayDriverCommand, CommonResultResponseDto<string>>
    {
        private readonly IRoutesService _routesService;
        public UpdateTodayDriverCommandHandler(IRoutesService routesService)
        {
            _routesService = routesService;
        }

        public async Task<CommonResultResponseDto<string>> Handle(UpdateTodayDriverCommand updateTodayDriverCommand, CancellationToken cancellationToken)
        {
            return await _routesService.UpdateTodayDriver(updateTodayDriverCommand.Adapt<UpdateTodayDriverDto>());

        }
    }
}

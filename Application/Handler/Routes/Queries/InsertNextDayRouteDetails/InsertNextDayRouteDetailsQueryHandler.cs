using Application.Abstraction.Services;
using DTO.Response;
using MediatR;

namespace Application.Handler.Routes.Queries.InsertNextDayRouteDetails
{
    public class InsertNextDayRouteDetailsQueryHandler : IRequestHandler<InsertNextDayRouteDetailsQuery, CommonResultResponseDto<string>>
    {
        private readonly IBackgroundServices  _backgroundServices;
        public InsertNextDayRouteDetailsQueryHandler(IBackgroundServices backgroundServices)
        {
            _backgroundServices = backgroundServices;
        }

        public async Task<CommonResultResponseDto<string>> Handle(InsertNextDayRouteDetailsQuery request, CancellationToken cancellationToken)
        {
         return await _backgroundServices.InsertNextDayRouteDetails();
        }
    }
}

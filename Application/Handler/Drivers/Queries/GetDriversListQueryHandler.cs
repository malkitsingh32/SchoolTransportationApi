using Application.Abstraction.Services;
using DTO.Response;
using DTO.Response.Driver;
using MediatR;

namespace Application.Handler.Drivers.Queries
{
    public class GetDriversListQueryHandler : IRequestHandler<GetDriversListQuery, CommonResultResponseDto<IList<GetDriversListResponseDto>>>
    {
        private readonly IDriversService _driversService;

        public GetDriversListQueryHandler(IDriversService driversService)
        {
            _driversService = driversService;
        }
        public async Task<CommonResultResponseDto<IList<GetDriversListResponseDto>>> Handle(GetDriversListQuery request, CancellationToken cancellationToken)
        {
            return await _driversService.GetDriversList();
        }
    }
}

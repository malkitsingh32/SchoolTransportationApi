using Application.Abstraction.Services;
using DTO.Response;
using DTO.Response.SystemValues;
using MediatR;

namespace Application.Handler.Drivers.Queries.GetDriverTypeList
{
    public class GetDriverTypeListQueryHandler : IRequestHandler<GetDriverTypeListQuery, CommonResultResponseDto<IList<GetDriverTypeResponseDto>>>
    {
        private readonly IDriversService _driversService;

        public GetDriverTypeListQueryHandler(IDriversService driversService)
        {
            _driversService = driversService;
        }

        public async Task<CommonResultResponseDto<IList<GetDriverTypeResponseDto>>> Handle(GetDriverTypeListQuery request, CancellationToken cancellationToken)
        {
            return await _driversService.GetDriverTypeList();
        }
    }
}

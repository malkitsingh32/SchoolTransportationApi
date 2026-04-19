using Application.Abstraction.Services;
using DTO.Response;
using DTO.Response.SystemValues;
using MediatR;

namespace Application.Handler.SystemValues.Queries.GetCities
{
    public class GetCitiesQueryHandler : IRequestHandler<GetCitiesQuery, CommonResultResponseDto<IList<GetCitiesResponseDto>>>
    {

        private readonly IAdminService _adminService;

        public GetCitiesQueryHandler(IAdminService adminService)
        {
            _adminService = adminService;
        }
        public async Task<CommonResultResponseDto<IList<GetCitiesResponseDto>>> Handle(GetCitiesQuery getCitiesQuery, CancellationToken cancellationToken)
        {
            return await _adminService.GetCities(getCitiesQuery.Id);
        }
    }
}

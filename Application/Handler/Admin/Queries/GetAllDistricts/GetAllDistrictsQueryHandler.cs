using Application.Abstraction.Services;
using DTO.Response;
using DTO.Response.Admin;
using MediatR;

namespace Application.Handler.Admin.Queries.GetAllDistricts
{
    public class GetAllDistrictsQueryHandler : IRequestHandler<GetAllDistrictsQuery, CommonResultResponseDto<IList<GetAllDistrictsResponseDto>>>
    {
        private readonly IAdminService _adminService;

        public GetAllDistrictsQueryHandler(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<CommonResultResponseDto<IList<GetAllDistrictsResponseDto>>> Handle(GetAllDistrictsQuery request, CancellationToken cancellationToken)
        {
            return await _adminService.GetAllDistricts();
        }
    }
}

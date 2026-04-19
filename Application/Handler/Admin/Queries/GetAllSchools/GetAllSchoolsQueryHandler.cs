using Application.Abstraction.Services;
using DTO.Response;
using DTO.Response.Admin;
using MediatR;

namespace Application.Handler.Admin.Queries.GetAllSchools
{
    public class GetAllSchoolsQueryHandler : IRequestHandler<GetAllSchoolsQuery, CommonResultResponseDto<IList<GetAllSchoolsResponseDto>>>
    {
        private readonly IAdminService _adminService;

        public GetAllSchoolsQueryHandler(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<CommonResultResponseDto<IList<GetAllSchoolsResponseDto>>> Handle(GetAllSchoolsQuery request, CancellationToken cancellationToken)
        {
            return await _adminService.GetAllSchools();
        }
    }
}

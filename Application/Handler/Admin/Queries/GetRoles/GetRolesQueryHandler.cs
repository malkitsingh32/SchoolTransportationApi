using Application.Abstraction.Services;
using DTO.Response;
using DTO.Response.SystemValues;
using MediatR;

namespace Application.Handler.Admin.Queries.GetRoles
{
    public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, CommonResultResponseDto<IList<GetAllRolesResponseDto>>>
    {
        private readonly IAdminService _adminService;

        public GetRolesQueryHandler(IAdminService adminService)
        {
            _adminService = adminService;
        }
        public async Task<CommonResultResponseDto<IList<GetAllRolesResponseDto>>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            return await _adminService.GetRoles();
        }
    }
}

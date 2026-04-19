using Application.Abstraction.Services;
using DTO.Response;
using DTO.Response.User;
using MediatR;

namespace Application.Handler.Admin.Queries.GetPermissions
{
    public class GetPermissionsByRoleIdQueryHandler : IRequestHandler<GetPermissionsByRoleIdQuery, CommonResultResponseDto<List<Permissions>>>
    {
        private readonly IAdminService _adminService;
        public GetPermissionsByRoleIdQueryHandler(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<CommonResultResponseDto<List<Permissions>>> Handle(GetPermissionsByRoleIdQuery getPermissionsByRoleIdQuery, CancellationToken cancellationToken)
        {
            return await _adminService.GetPermissionsByRoleId(getPermissionsByRoleIdQuery.RoleId);
        }
    }
}

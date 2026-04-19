using Application.Abstraction.Services;
using DTO.Response;
using MediatR;

namespace Application.Handler.Admin.Command.UpdatePermissions
{
    public class UpdatePermissionByRoleIdQueryHnadler : IRequestHandler<UpdatePermissionByRoleIdCommand, CommonResultResponseDto<string>>
    {
        private readonly IAdminService _adminService;


        public UpdatePermissionByRoleIdQueryHnadler(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<CommonResultResponseDto<string>> Handle(UpdatePermissionByRoleIdCommand updatePermissionByRoleIdCommand, CancellationToken cancellationToken)
        {
            var user = await _adminService.UpdatePermissionByRoleId(updatePermissionByRoleIdCommand.PermissionId, updatePermissionByRoleIdCommand.RoleId, updatePermissionByRoleIdCommand.PermissionType, updatePermissionByRoleIdCommand.CanView, updatePermissionByRoleIdCommand.CanEdit);
            return await Task.FromResult(user);
        }
    }
}

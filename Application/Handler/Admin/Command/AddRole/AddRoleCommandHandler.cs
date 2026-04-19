using Application.Abstraction.Services;
using DTO.Response;
using MediatR;

namespace Application.Handler.Admin.Command.AddRole
{
    public class AddRoleCommandHandler : IRequestHandler<AddRoleCommand, CommonResultResponseDto<string>>
    {
        private readonly IAdminService _adminService;


        public AddRoleCommandHandler(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<CommonResultResponseDto<string>> Handle(AddRoleCommand addRoleCommand, CancellationToken cancellationToken)
        {
            var user = await _adminService.AddRole(addRoleCommand.RoleName);
            return await Task.FromResult(user);
        }
    }
}

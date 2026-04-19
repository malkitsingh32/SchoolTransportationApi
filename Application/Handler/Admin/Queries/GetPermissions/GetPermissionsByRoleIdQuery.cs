using DTO.Response;
using DTO.Response.User;
using MediatR;

namespace Application.Handler.Admin.Queries.GetPermissions
{
    public class GetPermissionsByRoleIdQuery : IRequest<CommonResultResponseDto<List<Permissions>>>
    {
        public int RoleId { get; set; }
    }
}

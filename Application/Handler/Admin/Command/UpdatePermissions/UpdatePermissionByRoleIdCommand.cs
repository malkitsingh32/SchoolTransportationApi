using DTO.Response;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Handler.Admin.Command.UpdatePermissions
{
    public class UpdatePermissionByRoleIdCommand : IRequest<CommonResultResponseDto<string>>
    {
        [Required]
        public int PermissionId { get; set; }
        [Required]
        public int RoleId { get; set; }
        [Required]
        public string PermissionType { get; set; }
        [Required]
        public bool CanView { get; set; }
        [Required]
        public bool CanEdit { get; set; }
    }
}

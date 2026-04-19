using DTO.Response;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Handler.Admin.Command.AddRole
{
    public class AddRoleCommand : IRequest<CommonResultResponseDto<string>>
    {
        [Required]
        public string RoleName { get; set; }
    }
}

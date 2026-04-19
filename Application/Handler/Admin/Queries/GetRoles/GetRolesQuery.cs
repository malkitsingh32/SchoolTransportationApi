using DTO.Response;
using DTO.Response.SystemValues;
using MediatR;

namespace Application.Handler.Admin.Queries.GetRoles
{
    public class GetRolesQuery : IRequest<CommonResultResponseDto<IList<GetAllRolesResponseDto>>>
    {
    }
}

using DTO.Response;
using DTO.Response.Admin;
using MediatR;

namespace Application.Handler.Admin.Queries.GetAllGenders
{
    public class GetAllGendersQuery : IRequest<CommonResultResponseDto<IList<GetAllGendersResponseDto>>>
    {
    }
}

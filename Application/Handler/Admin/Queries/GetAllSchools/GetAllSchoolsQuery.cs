using DTO.Response;
using DTO.Response.Admin;
using MediatR;

namespace Application.Handler.Admin.Queries.GetAllSchools
{
    public class GetAllSchoolsQuery : IRequest<CommonResultResponseDto<IList<GetAllSchoolsResponseDto>>>
    {
    }
}

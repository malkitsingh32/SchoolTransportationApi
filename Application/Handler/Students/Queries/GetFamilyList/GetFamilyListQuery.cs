using DTO.Response;
using DTO.Response.Students;
using MediatR;

namespace Application.Handler.Students.Queries.GetFamilyList
{
    public class GetFamilyListQuery : IRequest<CommonResultResponseDto<IList<GetFamilyListResponseDto>>>
    {
    }
}

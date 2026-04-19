using DTO.Response;
using DTO.Response.Students;
using MediatR;

namespace Application.Handler.Students.Queries.GetGradeList
{
    public class GetGradeListQuery : IRequest<CommonResultResponseDto<IList<GetGradeListResponseDto>>>
    {
    }
}

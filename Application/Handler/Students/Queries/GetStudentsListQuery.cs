using DTO.Response;
using DTO.Response.Students;
using MediatR;

namespace Application.Handler.Students.Queries
{
    public class GetStudentsListQuery : IRequest<CommonResultResponseDto<IList<GetStudentsResponseDto>>>
    {
    }
}

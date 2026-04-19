using DTO.Response;
using DTO.Response.Students;
using MediatR;

namespace Application.Handler.Students.Queries.GetGradeBranchList
{
    public class GetGradeBranchListQuery : IRequest<CommonResultResponseDto<IList<GetGradeBranchListResponseDto>>>
    {
    }
}

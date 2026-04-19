

using DTO.Response;
using DTO.Response.Students;
using MediatR;

namespace Application.Handler.Students.Queries.GetAllBranch
{
    public class GetAllBranchQuery : IRequest<CommonResultResponseDto<IList<GetAllBranchResponseDto>>>
    {
    }
}

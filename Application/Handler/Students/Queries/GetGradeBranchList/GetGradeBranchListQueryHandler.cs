using Application.Abstraction.Services;
using DTO.Response;
using DTO.Response.Students;
using MediatR;

namespace Application.Handler.Students.Queries.GetGradeBranchList
{
    public class GetGradeBranchListQueryHandler : IRequestHandler<GetGradeBranchListQuery, CommonResultResponseDto<IList<GetGradeBranchListResponseDto>>>
    {
        private readonly IStudentsService _studentsService;
        public GetGradeBranchListQueryHandler(IStudentsService studentsService)
        {
            _studentsService = studentsService;
        }

        public async Task<CommonResultResponseDto<IList<GetGradeBranchListResponseDto>>> Handle(GetGradeBranchListQuery request, CancellationToken cancellationToken)
        {
            return await _studentsService.GetGradeBranchList();
        }
    }
}

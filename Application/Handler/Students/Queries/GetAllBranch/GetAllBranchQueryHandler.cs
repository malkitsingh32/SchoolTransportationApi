

using Application.Abstraction.Services;
using DTO.Response;
using DTO.Response.Students;
using MediatR;

namespace Application.Handler.Students.Queries.GetAllBranch
{
    public class GetAllBranchQueryHandler : IRequestHandler<GetAllBranchQuery, CommonResultResponseDto<IList<GetAllBranchResponseDto>>>
    {
        private readonly IStudentsService _studentsService;
        public GetAllBranchQueryHandler(IStudentsService studentsService)
        {
            _studentsService = studentsService;
        }

        public async Task<CommonResultResponseDto<IList<GetAllBranchResponseDto>>> Handle(GetAllBranchQuery request, CancellationToken cancellationToken)
        {
            return await _studentsService.GetAllBranch();
        }
    }
}

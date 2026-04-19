using Application.Abstraction.Services;
using DTO.Response;
using DTO.Response.Students;
using MediatR;

namespace Application.Handler.Students.Queries.GetGradeList
{
    public class GetGradeListQueryHandler : IRequestHandler<GetGradeListQuery, CommonResultResponseDto<IList<GetGradeListResponseDto>>>
    {
        private readonly IStudentsService _studentsService;
        public GetGradeListQueryHandler(IStudentsService studentsService)
        {
            _studentsService = studentsService;
        }

        public async Task<CommonResultResponseDto<IList<GetGradeListResponseDto>>> Handle(GetGradeListQuery request, CancellationToken cancellationToken)
        {
            return await _studentsService.GetGradeList();

        }
    }
}

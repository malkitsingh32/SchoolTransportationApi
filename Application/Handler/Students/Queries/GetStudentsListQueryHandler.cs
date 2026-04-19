using Application.Abstraction.Services;
using DTO.Response;
using DTO.Response.Students;
using MediatR;

namespace Application.Handler.Students.Queries
{
    public class GetStudentsListQueryHandler : IRequestHandler<GetStudentsListQuery, CommonResultResponseDto<IList<GetStudentsResponseDto>>>
    {
        private readonly IStudentsService _studentsService;

        public GetStudentsListQueryHandler(IStudentsService studentsService)
        {
            _studentsService = studentsService;
        }
        public async Task<CommonResultResponseDto<IList<GetStudentsResponseDto>>> Handle(GetStudentsListQuery request, CancellationToken cancellationToken)
        {
            return await _studentsService.GetStudentsList();
        }
    }
}

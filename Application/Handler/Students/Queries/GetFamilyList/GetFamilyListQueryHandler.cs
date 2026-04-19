using Application.Abstraction.Services;
using DTO.Response;
using DTO.Response.Students;
using MediatR;

namespace Application.Handler.Students.Queries.GetFamilyList
{
    public class GetFamilyListQueryHandler : IRequestHandler<GetFamilyListQuery, CommonResultResponseDto<IList<GetFamilyListResponseDto>>>
    {
        private readonly IStudentsService _studentsService;

        public GetFamilyListQueryHandler(IStudentsService studentsService)
        {
            _studentsService = studentsService;
        }
        public async Task<CommonResultResponseDto<IList<GetFamilyListResponseDto>>> Handle(GetFamilyListQuery request, CancellationToken cancellationToken)
        {
            return await _studentsService.GetFamilyList();
        }
    }
}

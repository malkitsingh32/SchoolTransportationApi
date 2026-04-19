using Application.Abstraction.Services;
using Application.Common.Dtos;
using Application.Common.Interfaces.Common;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.Students;
using MediatR;

namespace Application.Handler.Students.Queries.GetStudentsByFamilyId
{
    public class GetStudentsByFamilyIdQueryHandler : IRequestHandler<GetStudentsByFamilyIdQuery, CommonResultResponseDto<PaginatedList<GetStudentsByFamilyIdResponseDto>>>
    {
        private readonly IStudentsService _studentsService;
        private readonly IRequestBuilder _requestBuilder;
        public GetStudentsByFamilyIdQueryHandler(IStudentsService studentsService, IRequestBuilder requestBuilder)
        {
            _studentsService = studentsService;
            _requestBuilder = requestBuilder;
        }

        public async Task<CommonResultResponseDto<PaginatedList<GetStudentsByFamilyIdResponseDto>>> Handle(GetStudentsByFamilyIdQuery request, CancellationToken cancellationToken)
        {
            var filterModel = _requestBuilder.GetRequestBuilder(request.CommonRequest);
            return await _studentsService.GetStudentsByFamilyId(filterModel.GetFilters(), request.CommonRequest, filterModel.GetSorts(), request.FamilyId);
        }
    }
    public class GetStudentsByFamilyIdRequestDto
    {
        public ServerRowsRequest CommonRequest { get; set; }
        public string FamilyId { get; set; }
    }
}

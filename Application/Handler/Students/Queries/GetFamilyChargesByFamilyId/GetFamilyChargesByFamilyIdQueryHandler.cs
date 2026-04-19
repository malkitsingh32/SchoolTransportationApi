using Application.Abstraction.Services;
using Application.Common.Dtos;
using Application.Common.Interfaces.Common;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.Students;
using MediatR;

namespace Application.Handler.Students.Queries.GetFamilyChargesByFamilyId
{
    public class GetFamilyChargesByFamilyIdQueryHandler : IRequestHandler<GetFamilyChargesByFamilyIdQuery, CommonResultResponseDto<PaginatedList<GetFamilyChargesByFamilyIdResponseDto>>>
    {
        private readonly IStudentsService _studentsService;
        private readonly IRequestBuilder _requestBuilder;
        public GetFamilyChargesByFamilyIdQueryHandler(IStudentsService studentsService, IRequestBuilder requestBuilder)
        {
            _studentsService = studentsService;
            _requestBuilder = requestBuilder;
        }

        public async Task<CommonResultResponseDto<PaginatedList<GetFamilyChargesByFamilyIdResponseDto>>> Handle(GetFamilyChargesByFamilyIdQuery request, CancellationToken cancellationToken)
        {
            var filterModel = _requestBuilder.GetRequestBuilder(request.CommonRequest);
            return await _studentsService.GetFamilyChargesByFamilyId(filterModel.GetFilters(), request.CommonRequest, filterModel.GetSorts(), request.FamilyId);
        }
    }
    public class GetFamilyChargesByFamilyIdRequestDto
    {
        public ServerRowsRequest CommonRequest { get; set; }
        public string FamilyId { get; set; }
    }
}

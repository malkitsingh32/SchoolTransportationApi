using Application.Abstraction.Services;
using Application.Common.Interfaces.Common;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.SystemValues;
using MediatR;

namespace Application.Handler.Admin.Queries.GetSchool
{
    public class GetSchoolQueryHandler : IRequestHandler<GetSchoolQuery, CommonResultResponseDto<PaginatedList<GetSchoolsResponseDto>>>
    {

        private readonly IAdminService _adminService;
        private readonly IRequestBuilder _requestBuilder;
        public GetSchoolQueryHandler(IAdminService adminService, IRequestBuilder requestBuilder)
        {
            _adminService = adminService;
            _requestBuilder = requestBuilder;
        }
        public async Task<CommonResultResponseDto<PaginatedList<GetSchoolsResponseDto>>> Handle(GetSchoolQuery request, CancellationToken cancellationToken)
        {
            var filterModel = _requestBuilder.GetRequestBuilder(request.CommonRequest);
            return await _adminService.GetSchools(filterModel.GetFilters(), request.CommonRequest, filterModel.GetSorts());
        }
    }
}

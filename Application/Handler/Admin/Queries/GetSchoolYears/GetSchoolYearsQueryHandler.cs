using Application.Abstraction.Services;
using Application.Common.Interfaces.Common;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.User;
using MediatR;

namespace Application.Handler.Admin.Queries.GetSchoolYears
{
    public class GetSchoolYearsQueryHandler : IRequestHandler<GetSchoolYearsQuery, CommonResultResponseDto<PaginatedList<GetSchoolYearsResponseDto>>>
    {

        private readonly IAdminService _adminService;
        private readonly IRequestBuilder _requestBuilder;

        public GetSchoolYearsQueryHandler(IAdminService adminService, IRequestBuilder requestBuilder)
        {
            _adminService = adminService;
            _requestBuilder = requestBuilder;
        }

        public async Task<CommonResultResponseDto<PaginatedList<GetSchoolYearsResponseDto>>> Handle(GetSchoolYearsQuery request, CancellationToken cancellationToken)
        {
            var filterModel = _requestBuilder.GetRequestBuilder(request.CommonRequest);
            return await _adminService.GetSchoolYears(filterModel.GetFilters(), request.CommonRequest, filterModel.GetSorts());

        }
    }
}

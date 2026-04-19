using Application.Abstraction.Services;
using Application.Common.Interfaces.Common;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.SystemValues;
using MediatR;

namespace Application.Handler.Admin.Queries.GetDistrict
{
    public class GetDistrictQueryHandler : IRequestHandler<GetDistrictQuery, CommonResultResponseDto<PaginatedList<GetDistrictResponseDto>>>
    {
        private readonly IAdminService _adminService;
        private readonly IRequestBuilder _requestBuilder;
        public GetDistrictQueryHandler(IAdminService adminService, IRequestBuilder requestBuilder)
        {
            _adminService = adminService;
            _requestBuilder = requestBuilder;
        }

        public async Task<CommonResultResponseDto<PaginatedList<GetDistrictResponseDto>>> Handle(GetDistrictQuery request, CancellationToken cancellationToken)
        {
            var filterModel = _requestBuilder.GetRequestBuilder(request.CommonRequest);
            return await _adminService.GetDistrict(filterModel.GetFilters(), request.CommonRequest, filterModel.GetSorts());
        }
    }
}

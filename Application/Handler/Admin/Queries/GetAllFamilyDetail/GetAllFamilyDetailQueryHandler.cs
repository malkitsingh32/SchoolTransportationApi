

using Application.Abstraction.Services;
using Application.Common.Interfaces.Common;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.SystemValues;
using MediatR;

namespace Application.Handler.Admin.Queries.GetAllFamilyDetail
{
    public class GetAllFamilyDetailQueryHandler : IRequestHandler<GetAllFamilyDetailQuery, CommonResultResponseDto<PaginatedList<GetAllFamilyDetailResponseDto>>>
    {
        private readonly IAdminService _adminService;
        private readonly IRequestBuilder _requestBuilder;
        public GetAllFamilyDetailQueryHandler(IAdminService adminService, IRequestBuilder requestBuilder)
        {
            _adminService = adminService;
            _requestBuilder = requestBuilder;
        }

        public async Task<CommonResultResponseDto<PaginatedList<GetAllFamilyDetailResponseDto>>> Handle(GetAllFamilyDetailQuery request, CancellationToken cancellationToken)
        {
            var filterModel = _requestBuilder.GetRequestBuilder(request.CommonRequest);
            return await _adminService.GetAllFamilyDetail(filterModel.GetFilters(), request.CommonRequest, filterModel.GetSorts());
        }
    }
}

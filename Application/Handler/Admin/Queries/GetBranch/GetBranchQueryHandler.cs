

using Application.Abstraction.Services;
using Application.Common.Interfaces.Common;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.SystemValues;
using MediatR;

namespace Application.Handler.Admin.Queries.GetBranch
{
    public class GetBranchQueryHandler : IRequestHandler<GetBranchQuery, CommonResultResponseDto<PaginatedList<GetBranchResponseDto>>>
    {
        private readonly IAdminService _adminService;
        private readonly IRequestBuilder _requestBuilder;
        public GetBranchQueryHandler(IAdminService adminService, IRequestBuilder requestBuilder)
        {
            _adminService = adminService;
            _requestBuilder = requestBuilder;
        }

        public async Task<CommonResultResponseDto<PaginatedList<GetBranchResponseDto>>> Handle(GetBranchQuery request, CancellationToken cancellationToken)
        {
            var filterModel = _requestBuilder.GetRequestBuilder(request.CommonRequest);
            return await _adminService.GetBranch(filterModel.GetFilters(), request.CommonRequest, filterModel.GetSorts());
        }
    }
}

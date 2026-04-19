using Application.Abstraction.Services;
using Application.Common.Interfaces.Common;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.SystemValues;
using MediatR;

namespace Application.Handler.Admin.Queries.GetCc
{
    public class GetCcQueryHandler : IRequestHandler<GetCcQuery, CommonResultResponseDto<PaginatedList<GetCcResponseDto>>>
    {
        private readonly IAdminService _adminService;
        private readonly IRequestBuilder _requestBuilder;
        public GetCcQueryHandler(IAdminService adminService, IRequestBuilder requestBuilder)
        {
            _adminService = adminService;
            _requestBuilder = requestBuilder;
        }
        public async Task<CommonResultResponseDto<PaginatedList<GetCcResponseDto>>> Handle(GetCcQuery request, CancellationToken cancellationToken)
        {
            var filterModel = _requestBuilder.GetRequestBuilder(request.CommonRequest);
            return await _adminService.GetCc(filterModel.GetFilters(), request.CommonRequest, filterModel.GetSorts());
        }
    }
}

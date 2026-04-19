using Application.Abstraction.Services;
using Application.Common.Interfaces.Common;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.SystemValues;
using MediatR;

namespace Application.Handler.Admin.Queries.GetRunType
{
    public class GetRunTypeQueryHandler : IRequestHandler<GetRunTypeQuery, CommonResultResponseDto<PaginatedList<GetRunTypeResponseDto>>>
    {

        private readonly IAdminService _adminService;
        private readonly IRequestBuilder _requestBuilder;
        public GetRunTypeQueryHandler(IAdminService adminService, IRequestBuilder requestBuilder)
        {
            _adminService = adminService;
            _requestBuilder = requestBuilder;
        }

        public async Task<CommonResultResponseDto<PaginatedList<GetRunTypeResponseDto>>> Handle(GetRunTypeQuery request, CancellationToken cancellationToken)
        {
            var filterModel = _requestBuilder.GetRequestBuilder(request.CommonRequest);
            return await _adminService.GetRunType(filterModel.GetFilters(), request.CommonRequest, filterModel.GetSorts());
        }
    }
}

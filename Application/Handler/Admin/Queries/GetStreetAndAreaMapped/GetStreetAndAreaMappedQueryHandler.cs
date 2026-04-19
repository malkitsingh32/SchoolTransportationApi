using Application.Abstraction.Services;
using Application.Common.Interfaces.Common;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.SystemValues;
using MediatR;

namespace Application.Handler.Admin.Queries.GetStreetAndAreaMapped
{
    public class GetStreetAndAreaMappedQueryHandler : IRequestHandler<GetStreetAndAreaMappedQuery, CommonResultResponseDto<PaginatedList<GetStreetAndAreaMappedResponseDto>>>
    {
        private readonly IAdminService _adminService;
        private readonly IRequestBuilder _requestBuilder;
        public GetStreetAndAreaMappedQueryHandler(IAdminService adminService, IRequestBuilder requestBuilder)
        {
            _adminService = adminService;
            _requestBuilder = requestBuilder;
        }

        public async Task<CommonResultResponseDto<PaginatedList<GetStreetAndAreaMappedResponseDto>>> Handle(GetStreetAndAreaMappedQuery request, CancellationToken cancellationToken)
        {
            var filterModel = _requestBuilder.GetRequestBuilder(request.CommonRequest);
            return await _adminService.GetStreetAndAreaMapped(filterModel.GetFilters(), request.CommonRequest, filterModel.GetSorts());
        }
    }
}

using Application.Abstraction.Services;
using Application.Common.Interfaces.Common;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.SystemValues;
using MediatR;

namespace Application.Handler.Admin.Queries.GetSearchLocation
{
    public class GetSearchLocationQueryHandler : IRequestHandler<GetSearchLocationQuery, CommonResultResponseDto<PaginatedList<GetSearchLocationResponseDto>>>
    {

        private readonly IAdminService _adminService;
        private readonly IRequestBuilder _requestBuilder;
        public GetSearchLocationQueryHandler(IAdminService adminService, IRequestBuilder requestBuilder)
        {
            _adminService = adminService;
            _requestBuilder = requestBuilder;
        }

        public async Task<CommonResultResponseDto<PaginatedList<GetSearchLocationResponseDto>>> Handle(GetSearchLocationQuery request, CancellationToken cancellationToken)
        {
            var filterModel = _requestBuilder.GetRequestBuilder(request.CommonRequest);
            return await _adminService.GetSearchLocation(filterModel.GetFilters(), request.CommonRequest, filterModel.GetSorts());
        }
    }
}

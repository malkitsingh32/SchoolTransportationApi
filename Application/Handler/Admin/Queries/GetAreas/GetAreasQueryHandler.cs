using Application.Abstraction.Services;
using Application.Common.Interfaces.Common;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.SystemValues;
using MediatR;

namespace Application.Handler.Admin.Queries.GetAreas
{
    public class GetAreasQueryHandler : IRequestHandler<GetAreasQuery, CommonResultResponseDto<PaginatedList<GetAreasResponseDto>>>
    {
        private readonly IAdminService _adminService;
        private readonly IRequestBuilder _requestBuilder;

        public GetAreasQueryHandler(IAdminService adminService, IRequestBuilder requestBuilder)
        {
            _adminService = adminService;
            _requestBuilder = requestBuilder;

        }

        public async Task<CommonResultResponseDto<PaginatedList<GetAreasResponseDto>>> Handle(GetAreasQuery request, CancellationToken cancellationToken)
        {
            var filterModel = _requestBuilder.GetRequestBuilder(request.CommonRequest);
            return await _adminService.GetAreas(filterModel.GetFilters(), request.CommonRequest, filterModel.GetSorts());
        }
    }
}

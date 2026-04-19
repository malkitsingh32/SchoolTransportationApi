using Application.Abstraction.Services;
using Application.Common.Interfaces.Common;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.SystemValues;
using MediatR;

namespace Application.Handler.Admin.Queries.GetBusType
{
    public class GetBusTypeQueryHandler : IRequestHandler<GetBusTypeQuery, CommonResultResponseDto<PaginatedList<GetBusTypeResponseDto>>>
    {

        private readonly IAdminService _adminService;
        private readonly IRequestBuilder _requestBuilder;
        public GetBusTypeQueryHandler(IAdminService adminService, IRequestBuilder requestBuilder)
        {
            _adminService = adminService;
            _requestBuilder = requestBuilder;
        }

        public async Task<CommonResultResponseDto<PaginatedList<GetBusTypeResponseDto>>> Handle(GetBusTypeQuery request, CancellationToken cancellationToken)
        {
            var filterModel = _requestBuilder.GetRequestBuilder(request.CommonRequest);
            return await _adminService.GetBusType(filterModel.GetFilters(), request.CommonRequest, filterModel.GetSorts());
        }
    }
}

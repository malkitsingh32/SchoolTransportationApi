using Application.Abstraction.Services;
using Application.Common.Interfaces.Common;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.SystemValues;
using MediatR;

namespace Application.Handler.Admin.Queries.GetDriverType
{
    public class GetDriverTypeQueryHandler : IRequestHandler<GetDriverTypeQuery, CommonResultResponseDto<PaginatedList<GetDriverTypeResponseDto>>>
    {

        private readonly IAdminService _adminService;
        private readonly IRequestBuilder _requestBuilder;
        public GetDriverTypeQueryHandler(IAdminService adminService, IRequestBuilder requestBuilder)
        {
            _adminService = adminService;
            _requestBuilder = requestBuilder;
        }

        public async Task<CommonResultResponseDto<PaginatedList<GetDriverTypeResponseDto>>> Handle(GetDriverTypeQuery request, CancellationToken cancellationToken)
        {
            var filterModel = _requestBuilder.GetRequestBuilder(request.CommonRequest);
            return await _adminService.GetDriverType(filterModel.GetFilters(), request.CommonRequest, filterModel.GetSorts());
        }
    }
}

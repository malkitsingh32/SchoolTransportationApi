using Application.Abstraction.Services;
using Application.Common.Interfaces.Common;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.SystemValues;
using MediatR;

namespace Application.Handler.Admin.Queries.GetChargeStructure
{
    public class GetChargeStructureQueryHandler : IRequestHandler<GetChargeStructureQuery, CommonResultResponseDto<PaginatedList<GetChargeStructureResponseDto>>>
    {

        private readonly IAdminService _adminService;
        private readonly IRequestBuilder _requestBuilder;
        public GetChargeStructureQueryHandler(IAdminService adminService, IRequestBuilder requestBuilder)
        {
            _adminService = adminService;
            _requestBuilder = requestBuilder;
        }
        public async Task<CommonResultResponseDto<PaginatedList<GetChargeStructureResponseDto>>> Handle(GetChargeStructureQuery request, CancellationToken cancellationToken)
        {
            var filterModel = _requestBuilder.GetRequestBuilder(request.CommonRequest);
            return await _adminService.GetChargeStructure(filterModel.GetFilters(), request.CommonRequest, filterModel.GetSorts());
        }
    }
}

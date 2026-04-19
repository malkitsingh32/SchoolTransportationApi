using Application.Abstraction.Services;
using Application.Common.Interfaces.Common;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.SystemValues;
using MediatR;

namespace Application.Handler.Admin.Queries.GetDecductionAmount
{
    public class GetDecductionAmountQueryHandler : IRequestHandler<GetDecductionAmountQuery, CommonResultResponseDto<PaginatedList<GetDecductionAmountResponseDto>>>
    {

        private readonly IAdminService _adminService;
        private readonly IRequestBuilder _requestBuilder;
        public GetDecductionAmountQueryHandler(IAdminService adminService, IRequestBuilder requestBuilder)
        {
            _adminService = adminService;
            _requestBuilder = requestBuilder;
        }

        public async Task<CommonResultResponseDto<PaginatedList<GetDecductionAmountResponseDto>>> Handle(GetDecductionAmountQuery request, CancellationToken cancellationToken)
        {
            var filterModel = _requestBuilder.GetRequestBuilder(request.CommonRequest);
            return await _adminService.GetDecductionAmount(filterModel.GetFilters(), request.CommonRequest, filterModel.GetSorts());
        }
    }
}

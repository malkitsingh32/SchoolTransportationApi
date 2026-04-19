using Application.Abstraction.Services;
using Application.Common.Interfaces.Common;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.SystemValues;
using MediatR;

namespace Application.Handler.Admin.Queries.GetGender
{
    public class GetGenderQueryHandler : IRequestHandler<GetGenderQuery, CommonResultResponseDto<PaginatedList<GetGenderResponseDto>>>
    {

        private readonly IAdminService _adminService;
        private readonly IRequestBuilder _requestBuilder;
        public GetGenderQueryHandler(IAdminService adminService, IRequestBuilder requestBuilder)
        {
            _adminService = adminService;
            _requestBuilder = requestBuilder;
        }

        public async Task<CommonResultResponseDto<PaginatedList<GetGenderResponseDto>>> Handle(GetGenderQuery request, CancellationToken cancellationToken)
        {
            var filterModel = _requestBuilder.GetRequestBuilder(request.CommonRequest);
            return await _adminService.GetGender(filterModel.GetFilters(), request.CommonRequest, filterModel.GetSorts());
        }
    }
}

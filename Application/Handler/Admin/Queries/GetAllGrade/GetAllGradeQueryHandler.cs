using Application.Abstraction.Services;
using Application.Common.Interfaces.Common;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.SystemValues;
using MediatR;

namespace Application.Handler.Admin.Queries.GetAllGrade
{
    public class GetAllGradeQueryHandler : IRequestHandler<GetAllGradeQuery, CommonResultResponseDto<PaginatedList<GetAllGradeResponseDto>>>
    {
        private readonly IAdminService _adminService;
        private readonly IRequestBuilder _requestBuilder;
        public GetAllGradeQueryHandler(IAdminService adminService, IRequestBuilder requestBuilder)
        {
            _adminService = adminService;
            _requestBuilder = requestBuilder;
        }

        public async Task<CommonResultResponseDto<PaginatedList<GetAllGradeResponseDto>>> Handle(GetAllGradeQuery request, CancellationToken cancellationToken)
        {
            var filterModel = _requestBuilder.GetRequestBuilder(request.CommonRequest);
            return await _adminService.GetAllGrade(filterModel.GetFilters(), request.CommonRequest, filterModel.GetSorts());
        }
    }
}

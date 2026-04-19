using Application.Abstraction.Services;
using Application.Common.Interfaces.Common;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.SystemValues;
using MediatR;

namespace Application.Handler.Admin.Queries.GetSeasonFolder
{
    public class GetSeasonFolderQueryHandler : IRequestHandler<GetSeasonFolderQuery, CommonResultResponseDto<PaginatedList<GetSeasonFolderResponseDto>>>
    {

        private readonly IAdminService _adminService;
        private readonly IRequestBuilder _requestBuilder;
        public GetSeasonFolderQueryHandler(IAdminService adminService, IRequestBuilder requestBuilder)
        {
            _adminService = adminService;
            _requestBuilder = requestBuilder;
        }

        public async Task<CommonResultResponseDto<PaginatedList<GetSeasonFolderResponseDto>>> Handle(GetSeasonFolderQuery request, CancellationToken cancellationToken)
        {
            var filterModel = _requestBuilder.GetRequestBuilder(request.CommonRequest);
            return await _adminService.GetSeasonFolder(filterModel.GetFilters(), request.CommonRequest, filterModel.GetSorts());
        }
    }
}

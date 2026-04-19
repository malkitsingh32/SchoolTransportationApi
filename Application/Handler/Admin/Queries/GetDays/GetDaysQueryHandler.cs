using Application.Abstraction.Services;
using Application.Common.Interfaces.Common;
using Application.Common.Response;
using DTO.Response.Admin;
using DTO.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handler.Admin.Queries.GetDays
{
    public class GetDaysQueryHandler : IRequestHandler<GetDaysQuery, CommonResultResponseDto<PaginatedList<GetDaysResponseDto>>>
    {

        private readonly IAdminService _adminService;
        private readonly IRequestBuilder _requestBuilder;

        public GetDaysQueryHandler(
            IAdminService adminService,
            IRequestBuilder requestBuilder)
        {
            _adminService = adminService;
            _requestBuilder = requestBuilder;
        }

        public async Task<CommonResultResponseDto<PaginatedList<GetDaysResponseDto>>> Handle(GetDaysQuery request, CancellationToken cancellationToken)
        {

            var filterModel = _requestBuilder.GetRequestBuilder(request.CommonRequest);

            return await _adminService.GetDays(filterModel.GetFilters(), request.CommonRequest, filterModel.GetSorts());

        }

    }
}

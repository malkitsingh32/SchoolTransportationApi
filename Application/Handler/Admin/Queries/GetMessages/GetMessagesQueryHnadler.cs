using Application.Abstraction.Services;
using Application.Common.Interfaces.Common;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.Admin;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handler.Admin.Queries.GetMessages
{
    public class GetMessagesQueryHnadler : IRequestHandler<GetMessagesQuery, CommonResultResponseDto<PaginatedList<GetMessageResponseDto>>>
    {

        private readonly IAdminService _adminService;
        private readonly IRequestBuilder _requestBuilder;

        public GetMessagesQueryHnadler(
            IAdminService adminService,
            IRequestBuilder requestBuilder)
        {
            _adminService = adminService;
            _requestBuilder = requestBuilder;
        }

        public async Task<CommonResultResponseDto<PaginatedList<GetMessageResponseDto>>> Handle(
            GetMessagesQuery request,
            CancellationToken cancellationToken)
        {
            var filterModel = _requestBuilder.GetRequestBuilder(request.CommonRequest);

            return await _adminService.GetMessages(
                filterModel.GetFilters(),
                request.CommonRequest,
                filterModel.GetSorts()
            );
        }
    }
}

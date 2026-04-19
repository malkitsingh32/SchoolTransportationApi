using Application.Abstraction.Services;
using Application.Handler.Bus.Queries.GetAllBusDetails;
using DTO.Response.Bus;
using DTO.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.Response.Admin;

namespace Application.Handler.Admin.Queries.GetPredefinedSMSMessages
{
    public class GetPredefinedSMSMessagesQueryHandler : IRequestHandler<GetPredefinedSMSMessagesQuery, CommonResultResponseDto<IList<GetPredefinedSMSMessagesResponseDto>>>
    {
        private readonly IAdminService _adminService;

        public GetPredefinedSMSMessagesQueryHandler(IAdminService adminService)
        {
            _adminService = adminService;
        }
        public async Task<CommonResultResponseDto<IList<GetPredefinedSMSMessagesResponseDto>>> Handle(GetPredefinedSMSMessagesQuery request, CancellationToken cancellationToken)
        {
            return await _adminService.GetPredefinedSMSMessages();
        }
    }
}

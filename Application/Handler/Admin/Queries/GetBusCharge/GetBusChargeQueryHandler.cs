using Application.Abstraction.Services;
using DTO.Response;
using DTO.Response.Admin;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handler.Admin.Queries.GetBusCharge
{
    public class GetBusChargeQueryHandler : IRequestHandler<GetBusChargeQuery, CommonResultResponseDto<GetBusChargeQueryResponseDto>>
    {
        private readonly IAdminService _adminService;

        public GetBusChargeQueryHandler(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<CommonResultResponseDto<GetBusChargeQueryResponseDto>> Handle(GetBusChargeQuery request, CancellationToken cancellationToken)
        {
            return await _adminService.GetBusCharge();
        }
    }
}

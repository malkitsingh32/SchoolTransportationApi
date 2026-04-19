using Application.Abstraction.Services;
using DTO.Response.Admin;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handler.Admin.Queries.GetRouteTypeDays
{
    public class GetRouteTypeDaysQueryHandler : IRequestHandler<GetRouteTypeDaysQuery, IList<RouteTypeDayDto>>
    {
        private readonly IAdminService _adminService;

        public GetRouteTypeDaysQueryHandler(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<IList<RouteTypeDayDto>> Handle(GetRouteTypeDaysQuery request, CancellationToken cancellationToken)
        {
            return await _adminService.GetRouteTypeDays(request.RouteTypeId);
        }
    }
}

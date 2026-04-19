using Application.Abstraction.Services;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.Admin;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handler.Admin.Queries.GetPredefinedColors
{
    public class GetPredefinedColorsQueryHandler: IRequestHandler<GetPredefinedColorsQuery, CommonResultResponseDto<PaginatedList<PredefinedColorDto>>>
    {
        private readonly IAdminService _adminService;

        public GetPredefinedColorsQueryHandler(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<CommonResultResponseDto<PaginatedList<PredefinedColorDto>>> Handle(
            GetPredefinedColorsQuery request,
            CancellationToken cancellationToken)
        {
            return await _adminService.GetPredefinedColors(
                request.FilterModel,
                request.CommonRequest,
                request.OrderBy
            );
        }
    }
}

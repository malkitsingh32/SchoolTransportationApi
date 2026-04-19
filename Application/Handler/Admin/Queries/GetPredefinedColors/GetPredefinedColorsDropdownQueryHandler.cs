using Application.Abstraction.Services;
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
    public class GetPredefinedColorsDropdownQueryHandler: IRequestHandler<GetPredefinedColorsDropdownQuery, CommonResultResponseDto<IList<PredefinedColorDto>>>
    {
        private readonly IAdminService _adminService;

        public GetPredefinedColorsDropdownQueryHandler(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<CommonResultResponseDto<IList<PredefinedColorDto>>> Handle(
            GetPredefinedColorsDropdownQuery request,
            CancellationToken cancellationToken)
        {
            return await _adminService.GetPredefinedColorsDropdown();
        }
    }
}

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
    public class GetPredefinedColorsDropdownQuery: IRequest<CommonResultResponseDto<IList<PredefinedColorDto>>>
    {
    }
}

using Application.Common.Dtos;
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
    public class GetPredefinedColorsQuery : IRequest<CommonResultResponseDto<PaginatedList<PredefinedColorDto>>>
    {
        public string FilterModel { get; set; }
        public ServerRowsRequest CommonRequest { get; set; }
        public string OrderBy { get; set; }
    }
}

using Application.Common.Dtos;
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
    public class GetDaysQuery : ServerRowsRequest, IRequest<CommonResultResponseDto<PaginatedList<GetDaysResponseDto>>>
    {
        public ServerRowsRequest CommonRequest { get; set; }

    }
}

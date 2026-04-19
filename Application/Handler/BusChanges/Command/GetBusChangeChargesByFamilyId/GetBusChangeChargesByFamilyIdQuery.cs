using Application.Common.Dtos;
using Application.Common.Response;
using DTO.Response.Students;
using DTO.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.Response.BusChanges;

namespace Application.Handler.BusChanges.Command.GetBusChangeChargesByFamilyId
{
    public class GetBusChangeChargesByFamilyIdQuery : ServerRowsRequest, IRequest<CommonResultResponseDto<PaginatedList<GetBusChargesByFamilyIdResponseDto>>>
    {
        public string FamilyId { get; set; }
        public ServerRowsRequest CommonRequest { get; set; }

    }
}

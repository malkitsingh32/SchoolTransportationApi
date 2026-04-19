using Application.Common.Dtos;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.SystemValues;
using MediatR;

namespace Application.Handler.Admin.Queries.GetChargeStructure
{
    public class GetChargeStructureQuery : ServerRowsRequest, IRequest<CommonResultResponseDto<PaginatedList<GetChargeStructureResponseDto>>>
    {
        public ServerRowsRequest CommonRequest { get; set; }
    }
}

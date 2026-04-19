using Application.Common.Dtos;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.SystemValues;
using MediatR;

namespace Application.Handler.Admin.Queries.GetDecductionAmount
{
    public class GetDecductionAmountQuery : ServerRowsRequest, IRequest<CommonResultResponseDto<PaginatedList<GetDecductionAmountResponseDto>>>
    {
        public ServerRowsRequest CommonRequest { get; set; }
    }
}

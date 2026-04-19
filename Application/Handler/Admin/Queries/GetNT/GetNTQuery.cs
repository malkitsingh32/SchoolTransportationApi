using Application.Common.Dtos;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.SystemValues;
using MediatR;

namespace Application.Handler.Admin.Queries.GetNT
{
    public class GetNTQuery : ServerRowsRequest, IRequest<CommonResultResponseDto<PaginatedList<GetNTResponseDto>>>
    {
        public ServerRowsRequest CommonRequest { get; set; }
    }
}

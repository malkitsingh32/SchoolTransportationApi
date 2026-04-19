using Application.Common.Dtos;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.SystemValues;
using MediatR;

namespace Application.Handler.Admin.Queries.GetCc
{
    public class GetCcQuery : ServerRowsRequest, IRequest<CommonResultResponseDto<PaginatedList<GetCcResponseDto>>>
    {
        public ServerRowsRequest CommonRequest { get; set; }
    }
}

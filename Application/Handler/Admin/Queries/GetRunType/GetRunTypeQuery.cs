using Application.Common.Dtos;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.SystemValues;
using MediatR;

namespace Application.Handler.Admin.Queries.GetRunType
{
    public class GetRunTypeQuery : ServerRowsRequest, IRequest<CommonResultResponseDto<PaginatedList<GetRunTypeResponseDto>>>
    {
        public ServerRowsRequest CommonRequest { get; set; }
    }
}

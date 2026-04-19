using Application.Common.Dtos;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.SystemValues;
using MediatR;

namespace Application.Handler.Admin.Queries.GetStreetAndAreaMapped
{
    public class GetStreetAndAreaMappedQuery : ServerRowsRequest, IRequest<CommonResultResponseDto<PaginatedList<GetStreetAndAreaMappedResponseDto>>>
    {
        public ServerRowsRequest CommonRequest { get; set; }
    }
}

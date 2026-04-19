using Application.Common.Dtos;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.Streets;
using MediatR;

namespace Application.Handler.Streets.Queries.GetStreets
{
    public class GetStreetsQuery : ServerRowsRequest, IRequest<CommonResultResponseDto<PaginatedList<GetStreetsResponseDto>>>
    {
        public ServerRowsRequest CommonRequest { get; set; }
        public int RouteId { get; set; }
    }
}

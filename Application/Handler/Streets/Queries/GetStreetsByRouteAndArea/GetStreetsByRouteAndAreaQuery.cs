using Application.Common.Dtos;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.Streets;
using MediatR;

namespace Application.Handler.Streets.Queries.GetStreetsByRouteAndArea
{
    public class GetStreetsByRouteAndAreaQuery : ServerRowsRequest, IRequest<CommonResultResponseDto<PaginatedList<GetStreetsResponseDto>>>
    {
        public ServerRowsRequest CommonRequest { get; set; }
        public int RouteId { get; set; }
        //public string? AreaId { get; set; }
    }
}

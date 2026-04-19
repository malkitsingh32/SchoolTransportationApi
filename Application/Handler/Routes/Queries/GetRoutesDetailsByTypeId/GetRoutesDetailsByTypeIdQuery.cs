using Application.Common.Dtos;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.Routes;
using MediatR;

namespace Application.Handler.Routes.Queries.GetRoutesDetailsByTypeId
{
    public class GetRoutesDetailsByTypeIdQuery : ServerRowsRequest, IRequest<CommonResultResponseDto<PaginatedList<GetRoutesResponseDto>>>
    {
        public ServerRowsRequest CommonRequest { get; set; }
        public int RouteTypeId { get; set; }
    }
}

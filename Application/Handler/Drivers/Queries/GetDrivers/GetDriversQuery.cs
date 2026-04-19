using Application.Common.Dtos;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.Driver;
using MediatR;

namespace Application.Handler.Drivers.Queries.GetDrivers
{
    public class GetDriversQuery : ServerRowsRequest, IRequest<CommonResultResponseDto<PaginatedList<GetDriversResponseDto>>>
    {
        public ServerRowsRequest CommonRequest { get; set; }
        public int RouteId { get; set; }
    }
}

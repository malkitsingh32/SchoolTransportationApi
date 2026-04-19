using Application.Common.Dtos;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.Routes;
using MediatR;

namespace Application.Handler.Routes.Queries.GetRoutesByTabs
{
    public class GetRoutesByTabsQuery : ServerRowsRequest, IRequest<CommonResultResponseDto<PaginatedList<GetRoutesResponseDto>>>
    {
        public ServerRowsRequest CommonRequest { get; set; }
        public int BusDetailId { get; set; }
        public int StreetId { get; set; }
        public int DriverId { get; set; }
        public int StudentId { get; set; }
        public int SeasonFolderId { get; set; }
        public string? Grade { get; set; }
        public string? Role { get; set; }
    }
}

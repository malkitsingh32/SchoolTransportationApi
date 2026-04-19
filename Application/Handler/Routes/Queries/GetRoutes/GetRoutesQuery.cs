using Application.Common.Dtos;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.Routes;
using MediatR;

namespace Application.Handler.Routes.Queries.GetRoutes
{
    public class GetRoutesQuery : ServerRowsRequest, IRequest<CommonResultResponseDto<PaginatedList<GetRoutesResponseDto>>>
    {
        public ServerRowsRequest CommonRequest { get; set; }
        public int BusDetailId { get; set; }
        public int StreetId { get; set; }
        public int DriverId { get; set; }
        public int StudentId { get; set; }
        public int SeasonFolderId { get; set; }
        public string? Grade { get; set; }
        public string? Role { get; set; }
        public int? IsActiveRoutes { get; set; }
        public int? Gender { get; set; }
        public string? School { get; set; }
        public string? RouteId { get; set; }
    }
}

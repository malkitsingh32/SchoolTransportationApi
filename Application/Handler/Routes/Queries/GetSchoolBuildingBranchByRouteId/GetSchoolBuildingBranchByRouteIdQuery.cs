using Application.Common.Dtos;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.Routes;
using MediatR;

namespace Application.Handler.Routes.Queries.GetSchoolBuildingBranchByRouteId
{
    public class GetSchoolBuildingBranchByRouteIdQuery : ServerRowsRequest, IRequest<CommonResultResponseDto<PaginatedList<GetSchoolBuildingBranchResponseDto>>>
    {
        public ServerRowsRequest CommonRequest { get; set; }
        public int RouteId { get; set; }
    }

}

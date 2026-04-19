using Application.Common.Dtos;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.SystemValues;
using MediatR;

namespace Application.Handler.Admin.Queries.GetBuilding
{
    public class GetBuildingQuery : ServerRowsRequest, IRequest<CommonResultResponseDto<PaginatedList<GetBuildingResponseDto>>>
    {
        public ServerRowsRequest CommonRequest { get; set; }
    }
}

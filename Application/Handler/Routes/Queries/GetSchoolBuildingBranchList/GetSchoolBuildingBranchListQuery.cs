using Application.Common.Dtos;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.Streets;
using MediatR;

namespace Application.Handler.Routes.Queries.GetSchoolBuildingBranchList
{
    public class GetSchoolBuildingBranchListQuery : ServerRowsRequest, IRequest<CommonResultResponseDto<PaginatedList<GetSchoolBuildingBranchListResponseDto>>>
    {
        public ServerRowsRequest CommonRequest { get; set; }
    }
}

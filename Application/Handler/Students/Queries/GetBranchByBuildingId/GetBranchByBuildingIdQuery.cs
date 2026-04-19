using DTO.Response;
using DTO.Response.Students;
using MediatR;

namespace Application.Handler.Students.Queries.GetBranchByBuildingId
{
    public class GetBranchByBuildingIdQuery : IRequest<CommonResultResponseDto<IList<GetBranchByBuildingIdResponseDto>>>
    {
        public int BuildingId { get; set; }
    }
}

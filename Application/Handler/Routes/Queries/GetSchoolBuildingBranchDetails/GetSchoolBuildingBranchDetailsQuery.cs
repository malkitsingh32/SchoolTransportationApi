using DTO.Response;
using DTO.Response.Routes;
using MediatR;

namespace Application.Handler.Routes.Queries.GetSchoolBuildingBranchDetails
{
    public class GetSchoolBuildingBranchDetailsQuery : IRequest<CommonResultResponseDto<IList<GetSchoolBuildingBranchDetailsResponseDto>>>
    {
    }
}

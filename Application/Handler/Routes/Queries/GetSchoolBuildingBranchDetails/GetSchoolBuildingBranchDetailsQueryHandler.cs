using Application.Abstraction.Services;
using DTO.Response;
using DTO.Response.Routes;
using MediatR;

namespace Application.Handler.Routes.Queries.GetSchoolBuildingBranchDetails
{
    public class GetSchoolBuildingBranchDetailsQueryHandler : IRequestHandler<GetSchoolBuildingBranchDetailsQuery, CommonResultResponseDto<IList<GetSchoolBuildingBranchDetailsResponseDto>>>
    {
        private readonly IRoutesService _routesService;

        public GetSchoolBuildingBranchDetailsQueryHandler(IRoutesService routesService)
        {
            _routesService = routesService;
        }

        public async Task<CommonResultResponseDto<IList<GetSchoolBuildingBranchDetailsResponseDto>>> Handle(GetSchoolBuildingBranchDetailsQuery request, CancellationToken cancellationToken)
        {
            return await _routesService.GetSchoolBuildingBranchDetails();
        }
    }
}

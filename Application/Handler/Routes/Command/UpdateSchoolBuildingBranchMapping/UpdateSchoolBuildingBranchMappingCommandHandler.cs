using Application.Abstraction.Services;
using DTO.Request.Routes;
using DTO.Response;
using Mapster;
using MediatR;

namespace Application.Handler.Routes.Command.UpdateSchoolBuildingBranchMapping
{
    public class UpdateSchoolBuildingBranchMappingCommandHandler : IRequestHandler<UpdateSchoolBuildingBranchMappingCommand, CommonResultResponseDto<string>>
    {
        private readonly IRoutesService _routesService;
        public UpdateSchoolBuildingBranchMappingCommandHandler(IRoutesService routesService)
        {
            _routesService = routesService;
        }

        public async Task<CommonResultResponseDto<string>> Handle(UpdateSchoolBuildingBranchMappingCommand updateSchoolBuildingBranchMappingCommand, CancellationToken cancellationToken)
        {
            return await _routesService.UpdateSchoolBuildingBranchMapping(updateSchoolBuildingBranchMappingCommand.Adapt<UpdateSchoolBuildingBranchMappingDto>());
        }
    }
}

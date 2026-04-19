using Application.Abstraction.Services;
using DTO.Request.Street;
using DTO.Response;
using Mapster;
using MediatR;

namespace Application.Handler.Streets.Command.AddSchoolBuildingBranchMapping
{
    public class AddSchoolBuildingBranchMappingCommandHandler : IRequestHandler<AddSchoolBuildingBranchMappingCommand, CommonResultResponseDto<string>>
    {
        private readonly IStreetsService _streetsService;

        public AddSchoolBuildingBranchMappingCommandHandler(IStreetsService streetsService)
        {
            _streetsService = streetsService;
        }

        public async Task<CommonResultResponseDto<string>> Handle(AddSchoolBuildingBranchMappingCommand addSchoolBuildingBranchMappingCommand, CancellationToken cancellationToken)
        {
            return await _streetsService.AddSchoolBuildingBranchMapping(addSchoolBuildingBranchMappingCommand.Adapt<AddSchoolBuildingBranchMappingDto>());
        }
    }
}

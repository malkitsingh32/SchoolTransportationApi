using Application.Abstraction.Services;
using DTO.Request.Admin;
using DTO.Response;
using Mapster;
using MediatR;

namespace Application.Handler.Admin.Command.UpdateGradeMapping
{
    public class UpdateGradeMappingCommandHandler : IRequestHandler<UpdateGradeMappingCommand, CommonResultResponseDto<string>>
    {
        private readonly IAdminService _adminService;
        public UpdateGradeMappingCommandHandler(IAdminService adminService)
        {
            _adminService = adminService;
        }
        public async Task<CommonResultResponseDto<string>> Handle(UpdateGradeMappingCommand updateGradeMappingCommand, CancellationToken cancellationToken)
        {
            return await _adminService.UpdateGradeMapping(updateGradeMappingCommand.Adapt<UpdateGradeMappingDto>());
        }
    }
}

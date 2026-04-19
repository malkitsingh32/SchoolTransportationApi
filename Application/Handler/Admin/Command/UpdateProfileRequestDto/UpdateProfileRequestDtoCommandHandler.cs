using Application.Abstraction.Services;
using DTO.Response;
using Mapster;
using MediatR;

namespace Application.Handler.Admin.Command.UpdateProfileRequestDto
{
    public class UpdateProfileRequestDtoCommandHandler : IRequestHandler<UpdateProfileRequestDtoCommand, CommonResultResponseDto<string>>
    {
        private readonly IAdminService _adminService;
        public UpdateProfileRequestDtoCommandHandler(IAdminService adminService)
        {
            _adminService = adminService;
        }
        public async Task<CommonResultResponseDto<string>> Handle(UpdateProfileRequestDtoCommand updateProfileRequestDtoCommand, CancellationToken cancellationToken)
        {
            var user = await _adminService.UpdateProfile(updateProfileRequestDtoCommand.Adapt<DTO.Request.Admin.UpdateProfileRequestDto>());
            return await Task.FromResult(user);
        }
    }
}

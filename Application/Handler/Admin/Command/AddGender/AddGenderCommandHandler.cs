using Application.Abstraction.Services;
using DTO.Response;
using MediatR;

namespace Application.Handler.Admin.Command.AddGender
{
    public class AddGenderCommandHandler : IRequestHandler<AddGenderCommand, CommonResultResponseDto<string>>
    {
        private readonly IAdminService _adminService;
        public AddGenderCommandHandler(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<CommonResultResponseDto<string>> Handle(AddGenderCommand addGenderCommand, CancellationToken cancellationToken)
        {
            var user = await _adminService.AddGender(addGenderCommand.Id,addGenderCommand.Gender , addGenderCommand.UserId);
            return await Task.FromResult(user);
        }
    }
}

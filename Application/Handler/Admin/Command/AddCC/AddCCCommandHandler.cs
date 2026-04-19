using Application.Abstraction.Services;
using DTO.Response;
using MediatR;

namespace Application.Handler.Admin.Command.AddCC
{
    public class AddCCCommandHandler : IRequestHandler<AddCCCommand, CommonResultResponseDto<string>>
    {
        private readonly IAdminService _adminService;
        public AddCCCommandHandler(IAdminService adminService)
        {
            _adminService = adminService;
        }
        public async Task<CommonResultResponseDto<string>> Handle(AddCCCommand addCCCommand, CancellationToken cancellationToken)
        {
            var user = await _adminService.AddCC(addCCCommand.Id,addCCCommand.CardnoxId, addCCCommand.Family, addCCCommand.UserId);
            return await Task.FromResult(user);
        }
    }
}

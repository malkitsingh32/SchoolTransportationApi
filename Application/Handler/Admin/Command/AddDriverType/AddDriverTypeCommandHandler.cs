using Application.Abstraction.Services;
using DTO.Response;
using MediatR;

namespace Application.Handler.Admin.Command.AddDriverType
{
    public class AddDriverTypeCommandHandler : IRequestHandler<AddDriverTypeCommand, CommonResultResponseDto<string>>
    {
        private readonly IAdminService _adminService;
        public AddDriverTypeCommandHandler(IAdminService adminService)
        {
            _adminService = adminService;
        }
        public async Task<CommonResultResponseDto<string>> Handle(AddDriverTypeCommand addDriverTypeCommand, CancellationToken cancellationToken)
        {
            var user = await _adminService.AddDriverType(addDriverTypeCommand.Id, addDriverTypeCommand.DriverType, addDriverTypeCommand.PayRate, addDriverTypeCommand.UserId);
            return await Task.FromResult(user);
        }
    }
}

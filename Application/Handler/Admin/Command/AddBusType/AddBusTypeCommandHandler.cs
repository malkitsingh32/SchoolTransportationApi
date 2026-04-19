using Application.Abstraction.Services;
using DTO.Request.Admin;
using DTO.Response;
using Mapster;
using MediatR;

namespace Application.Handler.Admin.Command.AddBusType
{
    public class AddBusTypeCommandHandler : IRequestHandler<AddBusTypeCommand, CommonResultResponseDto<string>>
    {
        private readonly IAdminService _adminService;
        public AddBusTypeCommandHandler(IAdminService adminService)
        {
            _adminService = adminService;
        }
        public async Task<CommonResultResponseDto<string>> Handle(AddBusTypeCommand addBusTypeCommand, CancellationToken cancellationToken)
        {
            var user = await _adminService.AddBusType(addBusTypeCommand.Adapt<AddBusTypeRequestDto>());
            return await Task.FromResult(user);
        }
    }
}

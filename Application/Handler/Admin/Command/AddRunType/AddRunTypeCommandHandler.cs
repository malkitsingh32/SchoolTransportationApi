using Application.Abstraction.Services;
using DTO.Request.Admin;
using DTO.Response;
using Mapster;
using MediatR;

namespace Application.Handler.Admin.Command.AddRunType
{
    public class AddRunTypeCommandHandler : IRequestHandler<AddRunTypeCommand, CommonResultResponseDto<string>>
    {
        private readonly IAdminService _adminService;
        public AddRunTypeCommandHandler(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<CommonResultResponseDto<string>> Handle(AddRunTypeCommand addRunTypeCommand, CancellationToken cancellationToken)
        {
            return await _adminService.AddRunType(addRunTypeCommand.Adapt<AddRunTypeRequestDto>());
        }
    }
}

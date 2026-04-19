using Application.Abstraction.Services;
using DTO.Request.Admin;
using DTO.Response;
using Mapster;
using MediatR;

namespace Application.Handler.Admin.Command.AddMessages
{
    public class AddMessagesCommandHandler : IRequestHandler<AddMessagesCommand, CommonResultResponseDto<string>>
    {

        private readonly IAdminService _adminService;

        public AddMessagesCommandHandler(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<CommonResultResponseDto<string>> Handle(
            AddMessagesCommand request,
            CancellationToken cancellationToken)
        {
            return await _adminService.AddMessage(request.Adapt<AddMessageRequestDto>());
        }
    }
}

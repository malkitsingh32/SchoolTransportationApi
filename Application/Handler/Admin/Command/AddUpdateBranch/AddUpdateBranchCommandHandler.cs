

using Application.Abstraction.Services;
using DTO.Request.Admin;
using DTO.Response;
using Mapster;
using MediatR;

namespace Application.Handler.Admin.Command.AddUpdateBranch
{
    public class AddUpdateBranchCommandHandler : IRequestHandler<AddUpdateBranchCommand, CommonResultResponseDto<string>>
    {
        private readonly IAdminService _adminService;
        public AddUpdateBranchCommandHandler(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<CommonResultResponseDto<string>> Handle(AddUpdateBranchCommand addUpdateBranchCommand, CancellationToken cancellationToken)
        {
            var user = await _adminService.AddUpdateBranch(addUpdateBranchCommand.Adapt<AddUpdateBranchRequestDto>());
            return await Task.FromResult(user);
        }
    }
}

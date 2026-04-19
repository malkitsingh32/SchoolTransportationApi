using Application.Abstraction.Services;
using DTO.Response;
using MediatR;

namespace Application.Handler.Admin.Command.AddNT
{
    public class AddNTCommandHandler : IRequestHandler<AddNTCommand, CommonResultResponseDto<string>>
    {
        private readonly IAdminService _adminService;
        public AddNTCommandHandler(IAdminService adminService)
        {
            _adminService = adminService;
        }
        public async Task<CommonResultResponseDto<string>> Handle(AddNTCommand addNTCommand, CancellationToken cancellationToken)
        {
            var user = await _adminService.AddNT(addNTCommand.Id,addNTCommand.NTName, addNTCommand.UserId);
            return await Task.FromResult(user);
        }
    }
}

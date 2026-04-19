using Application.Abstraction.Services;
using DTO.Response;
using MediatR;

namespace Application.Handler.Admin.Command.AddDeductionAmount
{
    public class AddDeductionAmountCommandHandler : IRequestHandler<AddDeductionAmountCommand, CommonResultResponseDto<string>>
    {
        private readonly IAdminService _adminService;
        public AddDeductionAmountCommandHandler(IAdminService adminService)
        {
            _adminService = adminService;
        }
        public async Task<CommonResultResponseDto<string>> Handle(AddDeductionAmountCommand addDeductionAmountCommand, CancellationToken cancellationToken)
        {
            var user = await _adminService.AddDeductionAmount(addDeductionAmountCommand.Id, addDeductionAmountCommand.Amount, addDeductionAmountCommand.UserId);
            return await Task.FromResult(user);
        }
    }
}

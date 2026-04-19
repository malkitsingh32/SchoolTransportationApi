using Application.Abstraction.Services;
using DTO.Request.Payroll;
using DTO.Response;
using Mapster;
using MediatR;

namespace Application.Handler.Payroll.Command.addUpdatePayrollDetails
{
    public class AddUpdatePayrollDetailsCommandHandler : IRequestHandler<AddUpdatePayrollDetailsCommand, CommonResultResponseDto<string>>
    {
        private readonly IPayrollService _payrollService;

        public AddUpdatePayrollDetailsCommandHandler(IPayrollService payrollService)
        {
            _payrollService = payrollService;
        }

        public async Task<CommonResultResponseDto<string>> Handle(AddUpdatePayrollDetailsCommand addUpdatePayrollDetailsCommand, CancellationToken cancellationToken)
        {
            var user = await _payrollService.AddUpdatePayrollDetails(addUpdatePayrollDetailsCommand.Adapt<AddUpdatePayrollDetailsRequestDto>());
            return await Task.FromResult(user);
        }
    }
}

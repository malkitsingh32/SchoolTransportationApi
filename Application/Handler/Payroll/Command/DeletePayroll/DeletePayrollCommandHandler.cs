using Application.Abstraction.Services;
using DTO.Response;
using MediatR;

namespace Application.Handler.Payroll.Command.DeletePayroll
{
    public class DeletePayrollCommandHandler : IRequestHandler<DeletePayrollCommand, CommonResultResponseDto<string>>
    {
        private readonly IPayrollService _payrollService;

        public DeletePayrollCommandHandler(IPayrollService payrollService)
        {
            _payrollService = payrollService;
        }

        public async Task<CommonResultResponseDto<string>> Handle(DeletePayrollCommand deletePayrollCommand, CancellationToken cancellationToken)
        {
            return await _payrollService.DeletePayroll(deletePayrollCommand.DriverId, deletePayrollCommand.StartDate, deletePayrollCommand.EndDate);
        }
    }
}
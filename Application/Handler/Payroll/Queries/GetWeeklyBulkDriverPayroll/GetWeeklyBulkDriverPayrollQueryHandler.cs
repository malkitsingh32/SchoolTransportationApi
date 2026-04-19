using Application.Abstraction.Services;
using DTO.Request.Payroll;
using DTO.Response;
using DTO.Response.Payroll;
using Mapster;
using MediatR;

namespace Application.Handler.Payroll.Queries.GetWeeklyBulkDriverPayroll
{
    public class GetWeeklyBulkDriverPayrollQueryHandler : IRequestHandler<GetWeeklyBulkDriverPayrollQuery, CommonResultResponseDto<IList<GetWeeklyBulkDriverPayrollResponseDto>>>
    {
        private readonly IPayrollService _payrollService;

        public GetWeeklyBulkDriverPayrollQueryHandler(IPayrollService payrollService)
        {
            _payrollService = payrollService;
        }

        public async Task<CommonResultResponseDto<IList<GetWeeklyBulkDriverPayrollResponseDto>>> Handle(GetWeeklyBulkDriverPayrollQuery getWeeklyBulkDriverPayrollQuery, CancellationToken cancellationToken)
        {
            return await _payrollService.GetWeeklyBulkDriverPayroll(getWeeklyBulkDriverPayrollQuery.Adapt<GetWeeklyBulkDriverPayrollRequestDto>());
        }
    }
}

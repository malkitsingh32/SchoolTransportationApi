using Application.Abstraction.Services;
using DTO.Request.Payroll;
using DTO.Response;
using DTO.Response.Payroll;
using Mapster;
using MediatR;

namespace Application.Handler.Payroll.Queries.GetPayrollForAllDriversByDate
{
    public class GetPayrollForAllDriversByDateQueryHandler : IRequestHandler<GetPayrollForAllDriversByDateQuery, CommonResultResponseDto<IList<GetPayrollForAllDriversByDateResponseDto>>>
    {
        private readonly IPayrollService _payrollService;

        public GetPayrollForAllDriversByDateQueryHandler(IPayrollService payrollService)
        {
            _payrollService = payrollService;
        }

        public async Task<CommonResultResponseDto<IList<GetPayrollForAllDriversByDateResponseDto>>> Handle(GetPayrollForAllDriversByDateQuery getPayrollForAllDriversByDateQuery, CancellationToken cancellationToken)
        {
            return await _payrollService.GetPayrollForAllDriversByDate(getPayrollForAllDriversByDateQuery.Adapt<GetPayrollForAllDriversByDateRequestDto>());
        }
    }
}

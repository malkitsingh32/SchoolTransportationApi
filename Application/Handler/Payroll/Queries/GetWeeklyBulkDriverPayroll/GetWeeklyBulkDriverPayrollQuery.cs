using DTO.Response;
using DTO.Response.Payroll;
using MediatR;

namespace Application.Handler.Payroll.Queries.GetWeeklyBulkDriverPayroll
{
    public class GetWeeklyBulkDriverPayrollQuery : IRequest<CommonResultResponseDto<IList<GetWeeklyBulkDriverPayrollResponseDto>>>
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}

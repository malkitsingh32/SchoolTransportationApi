using DTO.Response;
using DTO.Response.Payroll;
using MediatR;

namespace Application.Handler.Payroll.Queries.GetPayrollForAllDriversByDate
{
    public class GetPayrollForAllDriversByDateQuery : IRequest<CommonResultResponseDto<IList<GetPayrollForAllDriversByDateResponseDto>>>
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}

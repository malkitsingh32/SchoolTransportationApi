using DTO.Response;
using MediatR;

namespace Application.Handler.Payroll.Command.addUpdatePayrollDetails
{
    public class AddUpdatePayrollDetailsCommand : IRequest<CommonResultResponseDto<string>>
    {
        public int PayrollId { get; set; }
        public DateTime Date { get; set; }
        public int Driver { get; set; }
        public decimal Amount { get; set; }
        public int[] TotalRoutes { get; set; }
        public int[] RegularRoutes { get; set; }
        public int[] PickedUpRoutes { get; set; }
        public int[] AbsentRoutes { get; set; }
        public int UserId { get; set; }
    }
}

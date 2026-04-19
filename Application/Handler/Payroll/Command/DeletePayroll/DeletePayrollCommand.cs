using DTO.Response;
using MediatR;

namespace Application.Handler.Payroll.Command.DeletePayroll
{
    public class DeletePayrollCommand : IRequest<CommonResultResponseDto<string>>
    {
       public int DriverId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

    }
}

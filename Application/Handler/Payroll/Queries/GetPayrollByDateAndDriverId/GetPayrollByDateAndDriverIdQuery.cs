using Application.Common.Dtos;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.Payroll;
using MediatR;

namespace Application.Handler.Payroll.Queries.GetPayrollByDateAndDriverId
{
    public class GetPayrollByDateAndDriverIdQuery : ServerRowsRequest, IRequest<CommonResultResponseDto<PaginatedList<GetPayrollByDateAndDriverIdResponseDto>>>
    {
        public ServerRowsRequest CommonRequest { get; set; }
        public int DriverId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}

using Application.Common.Dtos;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.Payroll;
using MediatR;

namespace Application.Handler.Payroll.Queries
{
    public class GetPayrollQuery : ServerRowsRequest, IRequest<CommonResultResponseDto<PaginatedList<GetPayrollResponseDto>>>
    {
        public ServerRowsRequest CommonRequest { get; set; }
    }
}

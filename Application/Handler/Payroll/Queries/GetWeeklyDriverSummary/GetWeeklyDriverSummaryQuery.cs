using Application.Common.Dtos;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.Payroll;
using MediatR;

namespace Application.Handler.Payroll.Queries.GetWeeklyDriverSummary
{
    public class GetWeeklyDriverSummaryQuery : ServerRowsRequest, IRequest<CommonResultResponseDto<PaginatedList<GetWeeklyDriverSummaryResponseDto>>>
    {
        public ServerRowsRequest CommonRequest { get; set; }
    }
}

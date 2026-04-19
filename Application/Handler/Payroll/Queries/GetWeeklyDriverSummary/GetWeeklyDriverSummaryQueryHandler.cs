using Application.Abstraction.Services;
using Application.Common.Interfaces.Common;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.Payroll;
using MediatR;

namespace Application.Handler.Payroll.Queries.GetWeeklyDriverSummary
{
    public class GetWeeklyDriverSummaryQueryHandler : IRequestHandler<GetWeeklyDriverSummaryQuery, CommonResultResponseDto<PaginatedList<GetWeeklyDriverSummaryResponseDto>>>
    {
        private readonly IPayrollService _payrollService;
        private readonly IRequestBuilder _requestBuilder;
        public GetWeeklyDriverSummaryQueryHandler(IPayrollService payrollService, IRequestBuilder requestBuilder)
        {
            _payrollService = payrollService;
            _requestBuilder = requestBuilder;
        }

        public async Task<CommonResultResponseDto<PaginatedList<GetWeeklyDriverSummaryResponseDto>>> Handle(GetWeeklyDriverSummaryQuery request, CancellationToken cancellationToken)
        {
            var filterModel = _requestBuilder.GetRequestBuilder(request.CommonRequest);
            return await _payrollService.GetWeeklyDriverSummary(filterModel.GetFilters(), request.CommonRequest, filterModel.GetSorts());
        }
    }
}

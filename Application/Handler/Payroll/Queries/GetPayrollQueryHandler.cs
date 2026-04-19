using Application.Abstraction.Services;
using Application.Common.Interfaces.Common;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.Payroll;
using MediatR;

namespace Application.Handler.Payroll.Queries
{
    public class GetPayrollQueryHandler : IRequestHandler<GetPayrollQuery, CommonResultResponseDto<PaginatedList<GetPayrollResponseDto>>>
    {
        private readonly IPayrollService _payrollService;
        private readonly IRequestBuilder _requestBuilder;
        public GetPayrollQueryHandler(IPayrollService payrollService, IRequestBuilder requestBuilder)
        {
            _payrollService = payrollService;
            _requestBuilder = requestBuilder;
        }

        public async Task<CommonResultResponseDto<PaginatedList<GetPayrollResponseDto>>> Handle(GetPayrollQuery request, CancellationToken cancellationToken)
        {
            var filterModel = _requestBuilder.GetRequestBuilder(request.CommonRequest);
            return await _payrollService.GetPayroll(filterModel.GetFilters(), request.CommonRequest, filterModel.GetSorts());
        }
    }
}

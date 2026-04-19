using Application.Abstraction.Services;
using Application.Common.Dtos;
using Application.Common.Interfaces.Common;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.Payroll;
using MediatR;

namespace Application.Handler.Payroll.Queries.GetRoutesHistoryByDriver
{
    public class GetRoutesHistoryByDriverIdQueryHandler : IRequestHandler<GetRoutesHistoryByDriverIdQuery, CommonResultResponseDto<PaginatedList<GetRoutesHistoryByDriverResponseDto>>>
    {
        private readonly IPayrollService _payrollService;
        private readonly IRequestBuilder _requestBuilder;
        public GetRoutesHistoryByDriverIdQueryHandler(IPayrollService payrollService, IRequestBuilder requestBuilder)
        {
            _payrollService = payrollService;
            _requestBuilder = requestBuilder;
        }

        public async Task<CommonResultResponseDto<PaginatedList<GetRoutesHistoryByDriverResponseDto>>> Handle(GetRoutesHistoryByDriverIdQuery request, CancellationToken cancellationToken)
        {
            var filterModel = _requestBuilder.GetRequestBuilder(request.CommonRequest);
            return await _payrollService.GetRoutesHistoryByDriver(filterModel.GetFilters(), request.CommonRequest, filterModel.GetSorts(), request.DriverId, request.Date);
        }
    }
}

public class GetRoutesHistoryByDriverRequestDto
{
    public ServerRowsRequest CommonRequest { get; set; }
    public int DriverId { get; set; }
    public DateTime Date { get; set; }

}
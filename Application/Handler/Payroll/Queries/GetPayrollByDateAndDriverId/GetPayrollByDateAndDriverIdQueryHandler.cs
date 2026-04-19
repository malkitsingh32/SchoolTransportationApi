using Application.Abstraction.Services;
using Application.Common.Dtos;
using Application.Common.Interfaces.Common;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.Payroll;
using MediatR;

namespace Application.Handler.Payroll.Queries.GetPayrollByDateAndDriverId
{
    public class GetPayrollByDateAndDriverIdQueryHandler : IRequestHandler<GetPayrollByDateAndDriverIdQuery, CommonResultResponseDto<PaginatedList<GetPayrollByDateAndDriverIdResponseDto>>>
    {
        private readonly IPayrollService _payrollService;
        private readonly IRequestBuilder _requestBuilder;
        public GetPayrollByDateAndDriverIdQueryHandler(IPayrollService payrollService, IRequestBuilder requestBuilder)
        {
            _payrollService = payrollService;
            _requestBuilder = requestBuilder;
        }

        public async Task<CommonResultResponseDto<PaginatedList<GetPayrollByDateAndDriverIdResponseDto>>> Handle(GetPayrollByDateAndDriverIdQuery request, CancellationToken cancellationToken)
        {
            var filterModel = _requestBuilder.GetRequestBuilder(request.CommonRequest);
            return await _payrollService.GetPayrollByDateAndDriverId(filterModel.GetFilters(), request.CommonRequest, filterModel.GetSorts(), request.DriverId, request.StartDate, request.EndDate);
        }
    }
}

public class GetPayrollByDateAndDriverIdRequestDto
{
    public ServerRowsRequest CommonRequest { get; set; }
    public int DriverId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}

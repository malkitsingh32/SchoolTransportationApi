using Application.Abstraction.Services;
using Application.Common.Dtos;
using Application.Common.Interfaces.Common;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.Payments;
using MediatR;

namespace Application.Handler.Payments.Queries
{
    internal class GetPaymentsQueryHandler : IRequestHandler<GetPaymentsQuery, CommonResultResponseDto<PaginatedList<GetPaymentsResponseDto>>>
    {
        private readonly IPaymentsService _paymentsService;
        private readonly IRequestBuilder _requestBuilder;

        public GetPaymentsQueryHandler(IPaymentsService paymentsService, IRequestBuilder requestBuilder)
        {
            _paymentsService = paymentsService;
            _requestBuilder = requestBuilder;

        }
        public async Task<CommonResultResponseDto<PaginatedList<GetPaymentsResponseDto>>> Handle(GetPaymentsQuery request, CancellationToken cancellationToken)
        {
            var filterModel = _requestBuilder.GetRequestBuilder(request.CommonRequest);
            return await _paymentsService.GetPayments(filterModel.GetFilters(), request.CommonRequest, filterModel.GetSorts(), request.StudentId);
        }
    }

    public class GetPaymentsRequestDto
    {
        public ServerRowsRequest CommonRequest { get; set; }
        public int StudentId { get; set; }

    }
}

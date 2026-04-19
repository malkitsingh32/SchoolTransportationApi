using Application.Abstraction.Services;
using Application.Common.Interfaces.Common;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.Payments;
using MediatR;

namespace Application.Handler.Payments.Queries.GetAllTransactions
{
    public class GetAllTransactionsQueryHandler : IRequestHandler<GetAllTransactionsQuery, CommonResultResponseDto<PaginatedList<GetAllTransactionsResponseDto>>>
    {
        private readonly IPaymentsService _paymentsService;
        private readonly IRequestBuilder _requestBuilder;

        public GetAllTransactionsQueryHandler(IPaymentsService paymentsService, IRequestBuilder requestBuilder)
        {
            _paymentsService = paymentsService;
            _requestBuilder = requestBuilder;

        }

        public async Task<CommonResultResponseDto<PaginatedList<GetAllTransactionsResponseDto>>> Handle(GetAllTransactionsQuery request, CancellationToken cancellationToken)
        {
            var filterModel = _requestBuilder.GetRequestBuilder(request.CommonRequest);
            return await _paymentsService.GetAllTransactions(filterModel.GetFilters(), request.CommonRequest, filterModel.GetSorts());
        }
    }
}

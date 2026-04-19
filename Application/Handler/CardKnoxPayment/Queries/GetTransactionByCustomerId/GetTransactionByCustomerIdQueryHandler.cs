using Application.Abstraction.Services;
using Application.Common.Dtos;
using Application.Common.Interfaces.Common;
using Application.Common.Response;
using Application.Handler.CardKnoxPayment.Queries.GetCardknoxPaymentMethodByFamilyId;
using DTO.Response.CardknoxPaymentMethod;
using DTO.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handler.CardKnoxPayment.Queries.GetTransactionByCustomerId
{
    public class GetTransactionByCustomerIdQueryHandler : IRequestHandler<GetTransactionByCustomerIdQuery, CommonResultResponseDto<PaginatedList<GetTransactionByCustomerIdResponseDto>>>
    {
        private readonly ICardknoxService _cardknoxService;
        private readonly IRequestBuilder _requestBuilder;
        public GetTransactionByCustomerIdQueryHandler(ICardknoxService cardknoxService, IRequestBuilder requestBuilder)
        {
            _cardknoxService = cardknoxService;
            _requestBuilder = requestBuilder;
        }

        public async Task<CommonResultResponseDto<PaginatedList<GetTransactionByCustomerIdResponseDto>>> Handle(GetTransactionByCustomerIdQuery request, CancellationToken cancellationToken)
        {
            var filterModel = _requestBuilder.GetRequestBuilder(request.CommonRequest);
            return await _cardknoxService.GetTransactionByCustomerId(filterModel.GetFilters(), request.CommonRequest, filterModel.GetSorts(), request.CustomerId);
        }
    }
    public class GetTransactionByCustomerIdRequestDto
    {
        public ServerRowsRequest CommonRequest { get; set; }
        public string CustomerId { get; set; }
    }
}

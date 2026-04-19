using Application.Common.Dtos;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.CardknoxPaymentMethod;
using MediatR;

namespace Application.Handler.CardKnoxPayment.Queries.GetCardknoxPaymentMethodByFamilyId
{
    public class GetTransactionByCustomerIdQuery : ServerRowsRequest, IRequest<CommonResultResponseDto<PaginatedList<GetTransactionByCustomerIdResponseDto>>>
    {
        public string CustomerId { get; set; }
        public ServerRowsRequest CommonRequest { get; set; }

    }
}

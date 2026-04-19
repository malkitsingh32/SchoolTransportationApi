using Application.Common.Dtos;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.CardknoxPaymentMethod;
using MediatR;

namespace Application.Handler.CardKnoxPayment.Queries.GetCardknoxPaymentMethodByFamilyId
{
    public class GetCardknoxPaymentMethodByFamilyIdQuery : ServerRowsRequest, IRequest<CommonResultResponseDto<PaginatedList<GetCardknoxPaymentMethodByFamilyIdResponseDto>>>
    {
        public string FamilyId { get; set; }
        public ServerRowsRequest CommonRequest { get; set; }

    }
}

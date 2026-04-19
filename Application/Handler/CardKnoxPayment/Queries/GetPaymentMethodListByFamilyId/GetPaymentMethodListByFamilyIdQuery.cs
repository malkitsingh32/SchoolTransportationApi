using DTO.Response;
using DTO.Response.CardknoxPaymentMethod;
using MediatR;

namespace Application.Handler.CardKnoxPayment.Queries.GetPaymentMethodListByFamilyId
{
    public class GetPaymentMethodListByFamilyIdQuery : IRequest<CommonResultResponseDto<IList<GetPaymentMethodListByFamilyIdResponseDto>>>
    {
        public int FamilyId { get; set; }
    }
}

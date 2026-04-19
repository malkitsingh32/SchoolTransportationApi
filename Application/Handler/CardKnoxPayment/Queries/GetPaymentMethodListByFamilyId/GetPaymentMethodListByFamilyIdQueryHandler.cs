using Application.Abstraction.Services;
using DTO.Response;
using DTO.Response.CardknoxPaymentMethod;
using MediatR;

namespace Application.Handler.CardKnoxPayment.Queries.GetPaymentMethodListByFamilyId
{
    public class GetPaymentMethodListByFamilyIdQueryHandler : IRequestHandler<GetPaymentMethodListByFamilyIdQuery, CommonResultResponseDto<IList<GetPaymentMethodListByFamilyIdResponseDto>>>
    {
        private readonly ICardknoxService _cardknoxService;

        public GetPaymentMethodListByFamilyIdQueryHandler(ICardknoxService cardknoxService)
        {
            _cardknoxService = cardknoxService;
        }

        public async Task<CommonResultResponseDto<IList<GetPaymentMethodListByFamilyIdResponseDto>>> Handle(GetPaymentMethodListByFamilyIdQuery request, CancellationToken cancellationToken)
        {
            return await _cardknoxService.GetPaymentMethodListByFamilyId(request.FamilyId);
        }
    }
}

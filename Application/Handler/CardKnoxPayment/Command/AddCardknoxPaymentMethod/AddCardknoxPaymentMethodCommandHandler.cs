using Application.Abstraction.Services;
using DTO.Request.CardknoxPaymentMethod;
using DTO.Response;
using Mapster;
using MediatR;

namespace Application.Handler.CardKnoxPayment.Command.AddCardknoxPaymentMethod
{
    public class AddCardknoxPaymentMethodCommandHandler : IRequestHandler<AddCardknoxPaymentMethodCommand, CommonResultResponseDto<string>>
    {
        private readonly ICardknoxService _cardknoxService;
        public AddCardknoxPaymentMethodCommandHandler(ICardknoxService cardknoxService)
        {
            _cardknoxService = cardknoxService;
        }

        public async Task<CommonResultResponseDto<string>> Handle(AddCardknoxPaymentMethodCommand request, CancellationToken cancellationToken)
        {
            return await _cardknoxService.AddCardknoxPaymentMethod(request.Adapt<AddCardknoxPaymentMethodDto>());
            
        }
    }
}

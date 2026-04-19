using Application.Abstraction.Services;
using DTO.Request.CardknoxPaymentMethod;
using DTO.Response;
using Mapster;
using MediatR;

namespace Application.Handler.CardKnoxPayment.Command.DeleteCardknoxPayment
{
    public class DeleteCardknoxPaymentCommandHAndler : IRequestHandler<DeleteCardknoxPaymentCommand, CommonResultResponseDto<string>>
    {
        private readonly ICardknoxService _cardknoxService;
        public DeleteCardknoxPaymentCommandHAndler(ICardknoxService cardknoxService)
        {
            _cardknoxService = cardknoxService;
        }

        public async Task<CommonResultResponseDto<string>> Handle(DeleteCardknoxPaymentCommand request, CancellationToken cancellationToken)
        {
            return await _cardknoxService.DeleteCardknoxPayment(request.Adapt<DeleteCardknoxPaymentRequestDto>());
        }
    }
}

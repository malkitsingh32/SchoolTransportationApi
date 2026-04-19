using Application.Abstraction.Services;
using DTO.Request.CardknoxPaymentMethod;
using DTO.Response;
using Mapster;
using MediatR;

namespace Application.Handler.CardKnoxPayment.Command.AddTransactionByCustomerId
{
    public class AddTransactionByCustomerIdQueryHandler : IRequestHandler<AddTransactionByCustomerIdQuery, CommonResultResponseDto<string>>
    {
        private readonly ICardknoxService _cardknoxService;
        public AddTransactionByCustomerIdQueryHandler(ICardknoxService cardknoxService)
        {
            _cardknoxService = cardknoxService;
        }

        public async Task<CommonResultResponseDto<string>> Handle(AddTransactionByCustomerIdQuery request, CancellationToken cancellationToken)
        {
            return await _cardknoxService.AddTransactionByCustomerId(request.Adapt<AddTransactionByCustomerIdRequestDto>());

        }
    }
}

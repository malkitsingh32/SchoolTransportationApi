using DTO.Response;
using MediatR;

namespace Application.Handler.CardKnoxPayment.Command.AddCardknoxPaymentMethod
{
    public class AddCardknoxPaymentMethodCommand : IRequest<CommonResultResponseDto<string>>
    {
        public int? FamilyId { get; set; }
        public string? CustomerId { get; set; }
        public string CardHolderName { get; set; }
        public string Last4 { get; set; }
        public string CardNumber { get; set; }
        public string SecurityCode { get; set; }
        public string ExpDate { get; set; }
        public string BillingAddress { get; set; }
        public bool IsDefault { get; set; }
        public string Zipcode { get; set; }
        public string? Token { get; set; }           // for iFields path (optional now)
        public string? TokenType { get; set; }       // "cc" or "ach" — used in validation + payload
        public string? TokenAlias { get; set; }
        public string? CardType { get; set; }
        public string? PaymentMethodId { get; set; }
    }
}

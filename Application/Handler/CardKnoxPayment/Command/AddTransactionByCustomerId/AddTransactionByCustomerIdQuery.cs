using DTO.Response;
using MediatR;

namespace Application.Handler.CardKnoxPayment.Command.AddTransactionByCustomerId
{
    public class AddTransactionByCustomerIdQuery : IRequest<CommonResultResponseDto<string>>
    {
        public string CustomerId { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public bool IsManual { get; set; }
        public string? PaymentMethodId { get; set; }
        public string? CheckNumber { get; set; }
        public DateTime? CheckDate { get; set; }
        public int ChargeId { get; set; }

    }
}

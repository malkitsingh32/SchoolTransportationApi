using DTO.Response;
using MediatR;

namespace Application.Handler.Payments.Command.RecordPayment
{
    public class RecodePaymentCommand : IRequest<CommonResultResponseDto<string>>
    {
        public int ChargeId { get; set; }
        public decimal Amount { get; set; }
        public int UserId { get; set; }
        public string PaymentType { get; set; }
    }
}

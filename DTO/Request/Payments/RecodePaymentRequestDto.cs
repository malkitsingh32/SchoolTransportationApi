namespace DTO.Request.Payments
{
    public class RecodePaymentRequestDto
    {
        public int ChargeId { get; set; }
        public decimal Amount { get; set; }
        public int UserId { get; set; }
        public string PaymentType { get; set; }
    }
}

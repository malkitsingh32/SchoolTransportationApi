namespace DTO.Request.CardknoxPaymentMethod
{
    public class AddTransactionByCustomerIdRequestDto
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

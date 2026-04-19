namespace DTO.Response.CardknoxPaymentMethod
{
    public class GetTransactionByCustomerIdResponseDto
    {
        public int Id { get; set; }
        public string CustomerId { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public DateTime? LastAttemptDate { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string PaymentMethod { get; set; }
        public string Last4 { get; set; }
        public string CardType { get; set; }
        public string Description { get; set; }

    }
}

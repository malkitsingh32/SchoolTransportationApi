namespace DTO.Response.CardknoxCustomers
{
    public class TransactionsResponseDto
    {
        public string RefNum { get; set; }
        public string Result { get; set; }
        public string Error { get; set; }
        public List<TransactionDto> Transactions { get; set; }
        public string NextToken { get; set; }
    }

    public class TransactionDto
    {
        public string TransactionId { get; set; }
        public string ScheduleId { get; set; }
        public string CustomerId { get; set; }
        public string PaymentMethodId { get; set; }
        public DateTime TransactionDate { get; set; }
        public string GatewayRefNum { get; set; }
        public string GatewayStatus { get; set; }
    }
}

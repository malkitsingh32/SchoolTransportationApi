namespace DTO.Response.Payments
{
    public class GetAllTransactionsResponseDto
    {
        public DateTime TransactionDate { get; set; }
        public string GatewayStatus { get; set; }
        public string FatherFirstName { get; set; }
        public string MotherFirstName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CardType { get; set; }
        public string CardNumber { get; set; }
        public string PaymentStatus { get; set; }
        public string PaymentType { get; set; }
    }
}

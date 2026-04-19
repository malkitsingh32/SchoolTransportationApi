namespace DTO.Response.CardknoxPaymentMethod
{
    public class GetCustomerResponse
    {
        public string Result { get; set; }
        public string RefNum { get; set; }
        public string Error { get; set; }
        public string CustomerId { get; set; }
        public List<PaymentMethodDto> PaymentMethods { get; set; }
    }
    public class PaymentMethodDto
    {
        public string PaymentMethodId { get; set; }
        public string TokenType { get; set; }
        public string TokenAlias { get; set; }
        public string MaskedNumber { get; set; }
        public string CardType { get; set; }
        public string Exp { get; set; }
        public bool IsDefault { get; set; }
    }
}

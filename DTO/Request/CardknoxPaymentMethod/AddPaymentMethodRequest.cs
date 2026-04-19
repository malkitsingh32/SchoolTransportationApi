namespace DTO.Request.CardknoxPaymentMethod
{
    public class AddPaymentMethodRequest
    {
        public string CustomerId { get; set; }
        public string Token { get; set; }
        public string TokenType { get; set; } = "cc";
        public string TokenAlias { get; set; }
        public string Exp { get; set; }
        public string Routing { get; set; }
        public string AccountType { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public string Zip { get; set; }
        public bool SetAsDefault { get; set; } = false;
        public string Last4 { get; set; }
    }
}

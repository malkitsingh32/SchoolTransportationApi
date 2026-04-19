namespace DTO.Request.CardknoxPaymentMethod
{
    public class AddCardknoxPaymentMethodDto
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
        public string? TokenAlias { get; set; }      // optional display name, e.g. "CC ending in 4242"
        public string? CardType { get; set; }      
        public string? PaymentMethodId { get; set; }      

    }

    public class GetPaymentMethodResponse
    {
        public string Result { get; set; }
        public string Error { get; set; }
        public string PaymentMethodId { get; set; }
        public int Revision { get; set; }        // ← needed for update
        public string Token { get; set; }
        public string TokenType { get; set; }
        public string TokenAlias { get; set; }
        public bool IsDefault { get; set; }
    }

    public class UpdatePaymentMethodResponse
    {
        public string Result { get; set; }
        public string Error { get; set; }
        public string PaymentMethodId { get; set; }
    }
}

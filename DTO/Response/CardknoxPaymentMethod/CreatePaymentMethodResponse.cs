namespace DTO.Response.CardknoxPaymentMethod
{
    public class CreatePaymentMethodResponse
    {
        public string Result { get; set; } // "S" for success, "E" for error
        public string RefNum { get; set; }
        public string Error { get; set; }
        public string PaymentMethodId { get; set; }
    }
}

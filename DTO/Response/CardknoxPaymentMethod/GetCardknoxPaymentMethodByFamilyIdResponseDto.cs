namespace DTO.Response.CardknoxPaymentMethod
{
    public class GetCardknoxPaymentMethodByFamilyIdResponseDto
    {
        public int Id { get; set; }
        public int? FamilyId { get; set; }
        public string CustomerId { get; set; }
        public string Token { get; set; }
        public string SecurityCode { get; set; }
        public string CardHolderName { get; set; }
        public string Last4 { get; set; }
        public string ExpDate { get; set; }
        public string BillingAddress { get; set; }
        public bool IsDefault { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CardType { get; set; }
        public string PaymentMethodId { get; set; }
    }
}

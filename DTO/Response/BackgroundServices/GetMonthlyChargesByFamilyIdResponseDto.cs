namespace DTO.Response.BackgroundServices
{
    public class GetMonthlyChargesByFamilyIdResponseDto
    {
        public int FamilyId { get; set; }
        public string FatherFirstName { get; set; }
        public string MotherFirstName { get; set; }
        public string LastName { get; set; }
        public decimal TotalCharge { get; set; }
        public string CustomerId { get; set; }
        public string MaskedCardNumber { get; set; }
        public string CardType { get; set; }
        public string ExpiryMonth { get; set; }
        public string ExpiryYear { get; set; }
        public int? previousAttemptCount { get; set; }
        public int? ChargeId { get; set; }
    }
}

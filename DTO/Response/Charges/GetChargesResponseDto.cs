namespace DTO.Response.Charges
{
    public class GetChargesResponseDto
    {
        public int ChargeId { get; set; }
        public int StudentId { get; set; }
        public decimal ChargeAmount { get; set; }
        public string FormattedChargeAmount => ChargeAmount.ToString("F2");
        public DateTime ChargeDate { get; set; }
        public string ChargeStatus { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherFirstName { get; set; }
        public string MotherFirstName { get; set; }
        public int familyId { get; set; }
    }
}

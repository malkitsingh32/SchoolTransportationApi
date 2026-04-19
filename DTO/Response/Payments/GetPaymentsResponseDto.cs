namespace DTO.Response.Payments
{
    public class GetPaymentsResponseDto
    {
        public int PaymentId { get; set; }
        public int StudentId { get; set; }
        public decimal ChargeAmount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string ChargeStatus { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherFirstName { get; set; }
        public string MotherFirstName { get; set; }
        public string CardType { get; set; }
        public string CardNumber { get; set; }
        public string PaymentStatus { get; set; }
        public string PaymentType { get; set; }
    }
}

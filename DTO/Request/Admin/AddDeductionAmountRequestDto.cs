namespace DTO.Request.Admin
{
    public class AddDeductionAmountRequestDto
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public int UserId { get; set; }
    }
}

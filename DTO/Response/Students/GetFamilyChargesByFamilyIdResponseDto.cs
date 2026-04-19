namespace DTO.Response.Students
{
    public class GetFamilyChargesByFamilyIdResponseDto
    {
        public int ChargeId { get; set; }
        public DateTime ChargeDate { get; set; }
        public decimal Amount { get; set; }
        public DateTime ProcessDate { get; set; }
        public decimal Balance { get; set; }
        public string Description { get; set; }
        public List<GetFamilyChargesDetailsDto> Details { get; set; } = new();
    }

    public class GetFamilyChargesDetailsDto
    {
        public string StudentName { get; set; }
        public DateTime? DOB { get; set; }
        public int? Funding { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public DateTime? ProcessDateTime { get; set; }
        public string Status { get; set; }
        public int ChargeId { get; set; }
    }
    public class ChargeMasterDto
    {
        public int Id { get; set; }
        public int FamilyId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        //public DateTime ProcessDateTime { get; set; }
        public decimal Balance { get; set; }
        public string Description { get; set; }
    }

    public class ChargeDetailDto
    {
        public string Student { get; set; }
        public DateTime? DOB { get; set; }
        public int? Funding { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public DateTime? ProcessDateTime { get; set; }
        public string Status { get; set; }
        public int ChargeId { get; set; }
    }
}

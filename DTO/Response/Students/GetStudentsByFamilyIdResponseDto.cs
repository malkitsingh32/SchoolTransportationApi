namespace DTO.Response.Students
{
    public class GetStudentsByFamilyIdResponseDto
    {
        public int StudentID { get; set; }
        public int FamilyId { get; set; }
        public string StudentName { get; set; }
        public string? HomeNumber { get; set; }
        public DateTime? DOB { get; set; }
        public bool? IsFunded { get; set; }
        public int? Mosdos { get; set; }
        public string? SchoolName { get; set; }
        public int? Grade { get; set; }
        public string? GradeName { get; set; }
        public decimal MonthlyFee { get; set; }
    }
}

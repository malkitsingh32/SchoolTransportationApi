namespace DTO.Response.Students
{
    public class GetFatherCellsListResponseDto
    {
        public int FamilyId { get; set; }
        public string FatherFirstName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string FatherCell { get; set; }
    }

    public class StudentSmsDto
    {
        public string PhoneNumber { get; set; }
        public string Message { get; set; }
    }
}

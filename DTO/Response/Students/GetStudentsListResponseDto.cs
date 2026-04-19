namespace DTO.Response.Students
{
    public class GetStudentsListResponseDto
    {
        public int StudentID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FathersName { get; set; }
        public string Address { get; set; }
        public string Grade { get; set; }
        public string District { get; set; }
        public string MWFamilyID { get; set; }
        public string MWStudentID { get; set; }
        public string SchoolFamilyID { get; set; }
        public string SchoolStudentID { get; set; }
        public string StudentLegalFirstName { get; set; }
        public string LegalLastName { get; set; }
        public string FatherFirstName { get; set; }
        public string MotherFirstName { get; set; }
        public DateTime DOB { get; set; }
        public string Gender { get; set; }
        public string Area { get; set; }
        public string HomeNumber { get; set; }
        public string FatherCell { get; set; }
        public string MotherCell { get; set; }
        public string FamilyThirdCell { get; set; }
        public string Email { get; set; }
        public string BuildingSys { get; set; }
        public string NT { get; set; }
        public string AM_BusName { get; set; }
        public string PM_BusName { get; set; }
        public string AM_Station { get; set; }
        public string PM_Station { get; set; }
        public string Friday_BusName { get; set; }
        public string Friday_Station { get; set; }
        public int UserId { get; set; }
        public int AM_BusDetaiilId { get; set; }
        public int PM_BusDetaiilId { get; set; }
        public int Friday_BusDetaiilId { get; set; }
        public int RouteId { get; set; }
    }
}

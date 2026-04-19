namespace DTO.Request.Students
{
    public class ImportBulkStudentsRequestDto
    {
        public List<ImportBulkStudentsListDto> ImportBulkStudentsLists { get; set; }
    }
    public class ImportBulkStudentsListDto
    {
        public string Address { get; set; }
        public string Area { get; set; } 
        public string Branch { get; set; } 
        public string Building { get; set; }
        public string City { get; set; }
        public string District { get; set; } 
        public string Dob { get; set; } 
        public string Email { get; set; } 
        public string FamilyThirdCell { get; set; } 
        public string FatherCell { get; set; } 
        public string FatherFirstName { get; set; } 
        public string FirstName { get; set; } 
        public string Gender { get; set; } 
        public string Grade { get; set; } 
        public string HomeNumber { get; set; } 
        public string LastName { get; set; } 
        public string Mosdos { get; set; } 
        public string MotherCell { get; set; } 
        public string MotherFirstName { get; set; } 
        public string MwFamilyId { get; set; } 
        public string MwStudentId { get; set; } 
        public string Nt { get; set; } 
        public string SchoolFamilyId { get; set; } 
        public string SchoolStudentId { get; set; } 
        public string State { get; set; } 
        public string StreetNumber { get; set; } 
        public string StudentLegalName { get; set; } 
        public string Unit { get; set; } 
        public string Zipcode { get; set; } 
    }

}

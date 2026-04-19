using System.ComponentModel.DataAnnotations.Schema;

namespace DTO.Response.Students
{
    public class ImportStudentsResult
    {
        public ImportStudentsResult()
        {
            Inserted = new List<InsertedStudentResult>();
            Failed = new List<FailedStudentResult>();
        }
        public List<InsertedStudentResult> Inserted { get; set; } 
        public List<FailedStudentResult> Failed { get; set; } 
    }


    public class InsertedStudentResult
    {
        public int TempRowId { get; set; }
        public int StudentID { get; set; }
    }

    public class FailedStudentResult
    {
        public int TempRowId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StudentLegalName { get; set; }
        public string GenderName { get; set; }
        public string FatherFirstName { get; set; }
        public string MotherFirstName { get; set; }
        public string Dob { get; set; }
        //public string Mosdos { get; set; }
        [Column("Mosdos")]
        public string SchoolName { get; set; }

        public string GradeName { get; set; }
        public string StreetNumber { get; set; }
        public string Address { get; set; }
        public string Unit { get; set; }
        public string StateName { get; set; }
        public string CityName { get; set; }
        public string Zipcode { get; set; }
        public string FatherCell { get; set; }
        public string MotherCell { get; set; }
        public string FamilyThirdCell { get; set; }
        public string HomeNumber { get; set; }
        public string Email { get; set; }
        public string MwFamilyId { get; set; }
        public string MwStudentId { get; set; }
        public string SchoolFamilyId { get; set; }
        public string SchoolStudentId { get; set; }
        public string BuildingName { get; set; }
        public string AreaName { get; set; }
        public string DistrictName { get; set; }
        public string NtName { get; set; }
        public string BranchName { get; set; }
        public string Reason { get; set; }
    }


}

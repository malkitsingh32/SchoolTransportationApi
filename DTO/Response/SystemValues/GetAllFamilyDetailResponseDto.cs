

namespace DTO.Response.SystemValues
{
    public class GetAllFamilyDetailResponseDto
    {
        // Family Details
        public string FamilyId { get; set; }
        public string LastName { get; set; }
        public string FatherFirstName { get; set; }
        public string MotherFirstName { get; set; }
        public string Address { get; set; }
        public string HomeNumber { get; set; }
        public string FatherCell { get; set; }
        public string MotherCell { get; set; }
        public string FamilyThirdCell { get; set; }
        public string Email { get; set; }
        public int State { get; set; }
        public string StateName { get; set; }
        public int City { get; set; }
        public string CityName { get; set; }
        public int Area { get; set; }
        public string AreaName { get; set; }
        public int District { get; set; }
        public string DistrictName { get; set; }
        public int Zipcode { get; set; }
        public bool IsTracking { get; set; }
        public int StreetNumber { get; set; }
        public decimal Balance { get; set; }
        public decimal LastPayment { get; set; }
        // Student Details
        public int? StudentID { get; set; }
        public string FirstName { get; set; }
        public string StudentLastName { get; set; }
        public string StudentLegalName { get; set; }
        public int? GenderId { get; set; }
        public string GenderName { get; set; }
        public DateTime? DOB { get; set; }
        public int? GradeId { get; set; }
        public string GradeName { get; set; }
        public string CustomerId { get; set; }
    }
}

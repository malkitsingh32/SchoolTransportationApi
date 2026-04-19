using DTO.Response;
using MediatR;

namespace Application.Handler.Students.Command.AddUpdateStudents
{
    public class AddUpdateStudentsCommand  : IRequest<CommonResultResponseDto<string>>
    {
        public int? StudentID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? FatherFirstName { get; set; }
        public string? MotherFirstName { get; set; }
        public string? StudentLegalName { get; set; }
        public DateTime? DOB { get; set; }
        public string? Address { get; set; }
        public int? Grade { get; set; }
        public int? Area { get; set; }
        public int? District { get; set; }
        public int? BuildingSys { get; set; }
        public string? NT { get; set; }
        public string? MWFamilyID { get; set; }
        public string? MWStudentID { get; set; }
        public string? SchoolFamilyID { get; set; }
        public string? SchoolStudentID { get; set; }
        public string? HomeNumber { get; set; }
        public string? FatherCell { get; set; }
        public string? MotherCell { get; set; }
        public string? FamilyThirdCell { get; set; }
        public string? Email { get; set; }
        public int? Gender { get; set; }
        //public int AMBusType { get; set; }
        //public int PMBusType { get; set; }
        //public int FridayBusType { get; set; }
        public int? UserId { get; set; }
        public int? State { get; set; }
        public int? City { get; set; }
        public int? Zipcode { get; set; }
        public int? RouteID { get; set; }
        public int? StreetId { get; set; }
        public int? Branch { get; set; }
        public int? StreetNumber { get; set; }
        public string? Unit { get; set; }
        public int? Mosdos { get; set; }
        public string? LatLong { get; set; }
        public int? AutoFillFamilyID { get; set; }
        public int? FamilyID { get; set; }
        public bool isUpdate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? FundStartDate { get; set; }
        public bool? Isfunded { get; set; }
        public bool IsUnknown { get; set; }
        public bool IsFromBusChange { get; set; }
    }
}

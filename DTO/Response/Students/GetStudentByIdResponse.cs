using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Response.Students
{
    public class GetStudentByIdResponse
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
        public string StudentLegalName { get; set; }
        public string FatherFirstName { get; set; }
        public string MotherFirstName { get; set; }
        public DateTime? DOB { get; set; }
        public string Gender { get; set; }
        public string GenderName { get; set; }
        public int Area { get; set; }
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

        public string Unit { get; set; }
        public int UserId { get; set; }
        //public int AM_BusDetailId { get; set; }
        //public int PM_BusDetailId { get; set; }
        //public int Friday_BusDetailId { get; set; }
        public string BuildingName { get; set; }
        public string DistrictName { get; set; }
        public string AreaName { get; set; }
        public string NtName { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public int Zipcode { get; set; }
        public int RouteId { get; set; }
        public int StreetId { get; set; }
        public int FamilyId { get; set; }
        //public string? Time { get; set; }
        public string? RouteName { get; set; }
        public string? RouteNumber { get; set; }
        public string? BusName { get; set; }
        public string? FormattedLocation { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public TimeSpan? Time { get; set; }
        public string? Days { get; set; }
        public string StreetNumber { get; set; }
        public int RowNumber { get; set; }
        public string SchoolName { get; set; }
        public string SchoolLegalName { get; set; }
        public int SchoolId { get; set; }
        public string BranchName { get; set; }
        public int BranchId { get; set; }
        public string GradeName { get; set; }
        public string? BusNumber { get; set; }
        public string? BusStopLatLong { get; set; }
        public double BusLongitude { get; set; }
        public double BusLatitude { get; set; }
        public string? StudentLatLng { get; set; }
        public int? FamilyStudentCount { get; set; }
        public string AssignedRouteTypeNames { get; set; }
        public string AssignedRouteTypeIDs { get; set; }
        public string RequiredRouteTypeIds { get; set; }
        public string AddressMask { get; set; }
        public int StreetNumberMask { get; set; }
        public bool IsFromFamilyTable { get; set; }
        public bool IsTracking { get; set; }
        public DateTime? StudentStartDate { get; set; }
        public DateTime? FundStartDate { get; set; }
        public bool? Isfunded { get; set; }
        public bool IsUnknown { get; set; }

    }
}

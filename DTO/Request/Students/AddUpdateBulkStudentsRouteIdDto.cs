
using DTO.Request.Routes;

namespace DTO.Request.Students
{
    public class AddUpdateBulkStudentsRouteIdDto
    {      
        public List<StudentsDetailListDto> studentsDetailLists { get; set; }
        public bool AddStudent { get; set; }
        public List<OverrideStudentDto> OverrideStudentList { get; set; }


    }

    public class StudentsDetailListDto
    {
        public int? StudentId { get; set; }
        public int? RouteId { get; set; }
        public int? StreetNumber { get; set; }
        public string? Address { get; set; }

    }
}

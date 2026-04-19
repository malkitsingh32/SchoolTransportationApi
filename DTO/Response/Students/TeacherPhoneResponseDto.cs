using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Response.Students
{
    public class TeacherPhoneResponseDto
    {
        public int RouteId { get; set; }
        public string TeacherName { get; set; }
        public string TeacherPhone { get; set; }
        public string FormattedLocation { get; set; }
    }
}

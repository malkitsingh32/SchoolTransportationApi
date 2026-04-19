using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Request.Routes
{
    public class GetFilteredRoutesForBusChangeRequestDto
    {
        public int? Area { get; set; }
        public int? SchoolId { get; set; }
        public int? Gender { get; set; }
        public int? Grade { get; set; }
        public int? Branch { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Include { get; set; }
        public string? StartFrom { get; set; }
    }
}

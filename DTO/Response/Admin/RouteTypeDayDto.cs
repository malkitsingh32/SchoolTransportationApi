using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Response.Admin
{
    public class RouteTypeDayDto
    {
        public int Id { get; set; }
        public string DayName { get; set; }
        public bool ExclusivelyPay { get; set; }
    }
}

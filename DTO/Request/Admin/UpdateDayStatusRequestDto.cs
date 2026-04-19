using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Request.Admin
{
    public class UpdateDayStatusRequestDto
    {
        public int Id { get; set; }

        public bool IsActive { get; set; }
    }
}

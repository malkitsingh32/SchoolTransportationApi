using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Response.Admin
{
    public class PredefinedColorDto
    {
        public int Id { get; set; }

        public string ColorName { get; set; }

        public string ColorCode { get; set; }

        public bool IsActive { get; set; }
    }
}

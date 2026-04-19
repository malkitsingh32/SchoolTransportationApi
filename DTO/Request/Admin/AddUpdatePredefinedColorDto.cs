using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Request.Admin
{
    public class AddUpdatePredefinedColorDto
    {
        public int Id { get; set; }         

        public string ColorName { get; set; }

        public string ColorCode { get; set; }
    }
}

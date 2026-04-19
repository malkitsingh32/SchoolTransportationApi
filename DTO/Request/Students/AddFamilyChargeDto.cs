using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Request.Students
{
    public class AddFamilyChargeDto
    {
        public int FamilyId { get; set; }
        public decimal Amount { get; set; }
        public string? Description { get; set; }
    }
}

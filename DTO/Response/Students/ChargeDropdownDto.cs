using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Response.Students
{
    public class ChargeDropdownDto
    {
        public int ChargeId { get; set; }
        public decimal Amount { get; set; }
        public DateTime ChargeDate { get; set; }
        public decimal Balance { get; set; }
        public string? Description { get; set; }
    }
}

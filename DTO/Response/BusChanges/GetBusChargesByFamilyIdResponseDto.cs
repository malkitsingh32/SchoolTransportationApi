using DTO.Response.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Response.BusChanges
{
    public class GetBusChargesByFamilyIdResponseDto
    {
        public int ChargeId { get; set; }
        public DateTime ChargeDate { get; set; }
        public decimal Amount { get; set; }
        public DateTime ProcessDate { get; set; }
        public decimal Balance { get; set; }
    }
}

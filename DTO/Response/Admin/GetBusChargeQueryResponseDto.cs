using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Response.Admin
{
    public class GetBusChargeQueryResponseDto
    {
        public int BusChargeId { get; set; }
        public decimal Amount { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Response.CardknoxPaymentMethod
{
    public class PaymentResultDto
    {
        public bool IsSuccess { get; set; }
        public string GatewayRefNum { get; set; }
        public string Status { get; set; }
        public string Error { get; set; }
        public string FullResponse { get; set; }
    }
}

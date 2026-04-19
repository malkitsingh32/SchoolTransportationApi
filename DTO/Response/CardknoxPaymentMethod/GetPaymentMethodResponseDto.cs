using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Response.CardknoxPaymentMethod
{
    public class GetPaymentMethodResponseDto
    {
        public string PaymentMethodId { get; set; }
        public string CardNumber { get; set; }
        public string CardType { get; set; }
        public string ID { get; set; }
        public bool IsDefault { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Request.CardknoxPaymentMethod
{
    public class PaymentTransactions
    {
        public int Id { get; set; }
        public string CustomerId { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; } // Success / Failed
        public string GatewayRefNum { get; set; }
        public string ErrorMessage { get; set; }
        public string FullResponse { get; set; }
        public int? AttemptCount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastAttemptDate { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public DateTime? TransactionDate { get; set; }
        public string GatewayResponse { get; set; }
        public string? Description { get; set; }
        public bool? IsManual { get; set; }
        public bool? IsBusChange { get; set; }
        public string? CheckNumber { get; set; }
        public DateTime? CheckDate { get; set; }
        public string? PaymentMethodId { get; set; }
        public string? CardNumber { get; set; }
        public int? ChargeId { get; set; }
    }
}

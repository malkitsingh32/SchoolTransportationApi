using Application.Abstraction.Services;
using DTO.Response.BackgroundServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implementation.Services
{
    public class BillingService : IBillingService
    {
        private readonly IBackgroundServices _backgroundServices;
        private readonly Serilog.ILogger _logger;

        public BillingService(
            IBackgroundServices backgroundServices,
            Serilog.ILogger logger)
        {
            _backgroundServices = backgroundServices;
            _logger = logger;
        }

        public async Task<IEnumerable<GetMonthlyChargesByFamilyIdResponseDto>> GetCustomersToCharge()
        {
            var allCharges = await _backgroundServices.getMonthlyChargesByFamilyId();

            // 👉 For now simple logic (we improve later with DB)
            var today = DateTime.Now.Date;

            var customersToCharge = allCharges
                .Where(x => x.TotalCharge > 0)
                .ToList();

            _logger.Information("Found {count} customers to charge", customersToCharge.Count);

            return customersToCharge;
        }

        public async Task MarkSuccess(string customerId)
        {
            _logger.Information("Payment success for customer {customerId}", customerId);

            // 👉 Later: update DB (ChargedThisMonth = true)
        }

        public async Task MarkFailed(string customerId)
        {
            _logger.Warning("Payment failed for customer {customerId}", customerId);

            // 👉 Later: set retry for next day
        }
    }
}

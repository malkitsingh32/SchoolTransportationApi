using DTO.Response.BackgroundServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstraction.Services
{
    public interface IBillingService
    {
        Task<IEnumerable<GetMonthlyChargesByFamilyIdResponseDto>> GetCustomersToCharge();
        Task MarkSuccess(string customerId);
        Task MarkFailed(string customerId);
    }
}

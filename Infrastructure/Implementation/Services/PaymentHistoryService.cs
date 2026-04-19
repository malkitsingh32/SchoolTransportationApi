using Application.Abstraction.Repositories;
using Application.Abstraction.Services;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Spreadsheet;
using DTO.Request.CardknoxPaymentMethod;
using DTO.Response;
using Helper.Constant;
using Infrastructure.Implementation.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implementation.Services
{
    public class PaymentHistoryService : IPaymentHistoryService
    {

        private readonly IPaymentHistoryRepository _paymentHistoryRepository;
        public PaymentHistoryService(IPaymentHistoryRepository paymentHistoryRepository)
        {
            _paymentHistoryRepository = paymentHistoryRepository;
        }

        public async Task<bool> AlreadyChargedThisMonth(string customerId)
        {
            var now = DateTime.Now;
            return await _paymentHistoryRepository.AlreadyChargedThisMonth(customerId, now.Month, now.Year);
        }

        public async Task SaveSuccess(string customerId, decimal amount, string refNum, string response, string? description, bool? isManual,  string? checkNumber, DateTime? checkDate, string? paymentMethodId, string? cardNumber, bool isBusChange, int? chargeId, string? Status)
        {
            var now = DateTime.Now;

            var entity = new PaymentTransactions
            {
                CustomerId = customerId,
                Amount = amount,
                Status = Status,
                GatewayRefNum = refNum,
                FullResponse = response,
                AttemptCount = 1,
                CreatedAt = now,
                LastAttemptDate = now,
                Month = now.Month,
                Year = now.Year,
                Description = description,
                IsManual = isManual,
                CheckNumber = checkNumber,
                CheckDate = checkDate,
                PaymentMethodId = paymentMethodId,
                CardNumber = cardNumber,
                IsBusChange = isBusChange,
                ChargeId = chargeId
            };

            await _paymentHistoryRepository.SaveSuccess(entity);
        }

        public async Task SaveFailure(string customerId, decimal amount, string error, string response, string? description, bool? isManual, int? attemptCount, string? paymentMethodId, string? cardNumber, bool isBusChange, int? chargeId, string? Status)
        {
            var now = DateTime.Now;

            var entity = new PaymentTransactions
            {
                CustomerId = customerId,
                Amount = amount,
                Status = Status,
                ErrorMessage = error,
                FullResponse = response,
                AttemptCount = attemptCount,
                CreatedAt = now,
                LastAttemptDate = now,
                Month = now.Month,
                Year = now.Year,
                Description = description,
                IsManual = isManual,
                PaymentMethodId = paymentMethodId,
                CardNumber = cardNumber,
                IsBusChange = isBusChange,
                ChargeId = chargeId
            };

            await _paymentHistoryRepository.SaveFailure(entity);
        }

        public async Task<IList<PaymentTransactions>> GetFailedPaymentsToRetry()
        {
            return await _paymentHistoryRepository.GetFailedPayments();
        }

        public async Task IncrementRetry(int id)
        {
            await _paymentHistoryRepository.IncrementRetry(id);
        }

        public async Task<bool> HasPendingFailure(string customerId)
        {
            var now = DateTime.Now;
            return await _paymentHistoryRepository.HasPendingFailure(customerId, now.Month, now.Year);
        }
    }
}

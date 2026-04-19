using Domain.Entities;
using DTO.Request.BusChanges;
using DTO.Request.CardknoxPaymentMethod;

namespace Application.Abstraction.Services
{
    public interface IPaymentHistoryService
    {
        Task<bool> AlreadyChargedThisMonth(string customerId);
        Task SaveSuccess(string customerId, decimal amount, string refNum, string response, string? description, bool? isManual, string? checkNumber, DateTime? checkDate, string? paymentMethodId, string? cardNumber, bool isBusChange, int? chargeId, string? Status);
        Task SaveFailure(string customerId, decimal amount, string error, string response, string? description, bool? isManual, int? attemptCount, string? paymentMethodId, string? cardNumber, bool isBusChange, int? chargeId, string? Status);
        Task<IList<PaymentTransactions>> GetFailedPaymentsToRetry();
        Task IncrementRetry(int id);
        Task<bool> HasPendingFailure(string customerId);
    }
}

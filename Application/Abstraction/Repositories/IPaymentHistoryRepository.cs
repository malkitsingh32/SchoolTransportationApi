using DTO.Request.CardknoxPaymentMethod;

namespace Application.Abstraction.Repositories
{
    public interface IPaymentHistoryRepository
    {
        Task<bool> AlreadyChargedThisMonth(string customerId, int month, int year);
        Task SaveSuccess(PaymentTransactions entity);
        Task SaveFailure(PaymentTransactions entity);
        Task<IList<PaymentTransactions>> GetFailedPayments();
        Task IncrementRetry(int Id);
        Task<bool> HasPendingFailure(string customerId, int month, int year);
    }
}

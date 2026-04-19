using DTO.Response.CardknoxPaymentMethod;

namespace Application.Abstraction.Services
{
    public interface IPaymentTransactionService
    {
        Task<PaymentResultDto> ProcessPaymentAsync(string customerId, decimal amount, string? description, string paymentMethodId);
        Task<PaymentResultDto> ProcessPaymentWithCardAsync(string customerId, decimal amount, string? description, string CardNumber, string ExpiryDate, string CVV, string CardHolderName);
    }
}

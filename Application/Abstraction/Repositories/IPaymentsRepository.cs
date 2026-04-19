using Application.Common.Dtos;
using DTO.Request.Payments;
using DTO.Response.Payments;

namespace Application.Abstraction.Repositories
{
    public interface IPaymentsRepository
    {
        Task<(List<GetPaymentsResponseDto>, int)> GetPayments(string filterModel, ServerRowsRequest commonRequest, string getSort, int studentId);
        Task<int> RecodePayment(RecodePaymentRequestDto recodePaymentRequestDto);
        Task<(List<GetAllTransactionsResponseDto>, int)> GetAllTransactions(string filterModel, ServerRowsRequest commonRequest, string getSort);
    }
}

using Application.Common.Dtos;
using Application.Common.Response;
using DTO.Request.Payments;
using DTO.Response;
using DTO.Response.Payments;

namespace Application.Abstraction.Services
{
    public interface IPaymentsService
    {
        Task<CommonResultResponseDto<PaginatedList<GetPaymentsResponseDto>>> GetPayments(string filterModel, ServerRowsRequest commonRequest, string getSort, int studentId);
        Task<CommonResultResponseDto<string>> RecodePayment(RecodePaymentRequestDto recodePaymentRequestDto);
        Task<CommonResultResponseDto<PaginatedList<GetAllTransactionsResponseDto>>> GetAllTransactions(string filterModel, ServerRowsRequest commonRequest, string getSort);
    }
}

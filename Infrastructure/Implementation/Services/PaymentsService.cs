using Application.Abstraction.Repositories;
using Application.Abstraction.Services;
using Application.Common.Dtos;
using Application.Common.Response;
using DTO.Request.Payments;
using DTO.Response;
using DTO.Response.Payments;
using Helper;
using Helper.Constant;

namespace Infrastructure.Implementation.Services
{
    class PaymentsService : IPaymentsService
    {
        private readonly IPaymentsRepository _paymentsRepository;
        public PaymentsService(IPaymentsRepository paymentsRepository)
        {
            _paymentsRepository = paymentsRepository;
        }


        public async Task<CommonResultResponseDto<PaginatedList<GetPaymentsResponseDto>>> GetPayments(string filterModel, ServerRowsRequest commonRequest, string getSort, int studentId)
        {
            var (payments, total) = await _paymentsRepository.GetPayments(filterModel, commonRequest, getSort, studentId);
            return CommonResultResponseDto<PaginatedList<GetPaymentsResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, new PaginatedList<GetPaymentsResponseDto>(payments, total), 0);
        }

        public async Task<CommonResultResponseDto<string>> RecodePayment(RecodePaymentRequestDto recodePaymentRequestDto)
        {
            var paymentId = await _paymentsRepository.RecodePayment(recodePaymentRequestDto);
            if (paymentId > 0)
            {
                return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Created }, null, paymentId);
            }
            else if (paymentId < 0)
            {
                return CommonResultResponseDto<string>.Failure(new string[] { "Something went wrong" }, null);
            }
            else
            {
                return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Updated }, null, paymentId);
            }
        }

        public async Task<CommonResultResponseDto<PaginatedList<GetAllTransactionsResponseDto>>> GetAllTransactions(string filterModel, ServerRowsRequest commonRequest, string getSort)
        {
            var (transactions, total) = await _paymentsRepository.GetAllTransactions(filterModel, commonRequest, getSort);
            return CommonResultResponseDto<PaginatedList<GetAllTransactionsResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, new PaginatedList<GetAllTransactionsResponseDto>(transactions, total), 0);
        }
    }
}

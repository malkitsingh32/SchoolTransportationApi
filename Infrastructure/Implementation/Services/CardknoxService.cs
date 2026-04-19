using Application.Abstraction.Repositories;
using Application.Abstraction.Services;
using Application.Common.Dtos;
using Application.Common.Helpers;
using Application.Common.Response;
using DTO.Request.CardknoxPaymentMethod;
using DTO.Response;
using DTO.Response.CardknoxPaymentMethod;
using Helper;
using Helper.Constant;

namespace Infrastructure.Implementation.Services
{
    public class CardknoxService : ICardknoxService
    {
        private readonly ICardknoxRepository  _cardknoxRepository;
        private readonly Utility _utility;
        private readonly IPaymentTransactionService _paymentService;
        private readonly IPaymentHistoryService _paymentHistoryService;
        public CardknoxService(ICardknoxRepository cardknoxRepository, Utility utility, IPaymentTransactionService paymentService,IPaymentHistoryService paymentHistoryService)
        {
            _cardknoxRepository = cardknoxRepository;
            _utility = utility;
            _paymentService = paymentService;
            _paymentHistoryService = paymentHistoryService;
        }

        public async Task<CommonResultResponseDto<string>> AddCardknoxPaymentMethod(AddCardknoxPaymentMethodDto addCardknoxPaymentMethodDto)
        {
            var paymentMethodId = await _cardknoxRepository.AddCardknoxPaymentMethod(addCardknoxPaymentMethodDto);      
            return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Created }, null, paymentMethodId);
                         
        }       

        public async Task<CommonResultResponseDto<string>> DeleteCardknoxPayment(DeleteCardknoxPaymentRequestDto deleteCardknoxPaymentRequestDto)
        {
            var studentId = await _cardknoxRepository.DeleteCardknoxPayment(deleteCardknoxPaymentRequestDto);
            if (studentId > 0)
            {
                return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Deleted }, null, studentId);
            }
            else
            {
                return CommonResultResponseDto<string>.Failure(new string[] { "Something went wrong" }, null);
            }
        }

        public async Task<CommonResultResponseDto<PaginatedList<GetCardknoxPaymentMethodByFamilyIdResponseDto>>> GetCardknoxPaymentMethodByFamilyId(string filterModel, ServerRowsRequest commonRequest, string getSort, string familyId)
        {
            var (students, total) = await _cardknoxRepository.GetCardknoxPaymentMethodByFamilyId(filterModel, commonRequest, getSort, familyId);
            return CommonResultResponseDto<PaginatedList<GetCardknoxPaymentMethodByFamilyIdResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, new PaginatedList<GetCardknoxPaymentMethodByFamilyIdResponseDto>(students, total), 0);
        }       

        public async Task<CommonResultResponseDto<PaginatedList<GetTransactionByCustomerIdResponseDto>>> GetTransactionByCustomerId(string filterModel, ServerRowsRequest commonRequest, string getSort, string customerId)
        {
            var (transactions, total) = await _cardknoxRepository.GetTransactionByCustomerId(filterModel, commonRequest, getSort, customerId);
            return CommonResultResponseDto<PaginatedList<GetTransactionByCustomerIdResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, new PaginatedList<GetTransactionByCustomerIdResponseDto>(transactions, total), 0);
        }

        public async Task<CommonResultResponseDto<string>> AddTransactionByCustomerId(AddTransactionByCustomerIdRequestDto addTransactionByCustomerIdRequestDto)
        {
            if (!string.IsNullOrWhiteSpace(addTransactionByCustomerIdRequestDto.PaymentMethodId))
            {
                var result = await _paymentService.ProcessPaymentAsync(
                            addTransactionByCustomerIdRequestDto.CustomerId,
                            addTransactionByCustomerIdRequestDto.Amount,
                            addTransactionByCustomerIdRequestDto.Description,
                            addTransactionByCustomerIdRequestDto.PaymentMethodId);

                if (result.IsSuccess)
                {
                    await _paymentHistoryService.SaveSuccess(
                        addTransactionByCustomerIdRequestDto.CustomerId,
                        addTransactionByCustomerIdRequestDto.Amount,
                        result.GatewayRefNum,
                        result.FullResponse,
                        addTransactionByCustomerIdRequestDto.Description,
                        addTransactionByCustomerIdRequestDto.IsManual,
                        null,
                        null,
                        addTransactionByCustomerIdRequestDto.PaymentMethodId, null, false, addTransactionByCustomerIdRequestDto.ChargeId, result.Status);
                    return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Created }, null, 1);
                }
                else
                {
                    await _paymentHistoryService.SaveFailure(
                        addTransactionByCustomerIdRequestDto.CustomerId,
                        addTransactionByCustomerIdRequestDto.Amount,
                        result.Error,
                        result.FullResponse,
                        addTransactionByCustomerIdRequestDto.Description,
                        addTransactionByCustomerIdRequestDto.IsManual,
                        1,
                        addTransactionByCustomerIdRequestDto.PaymentMethodId, null, false, addTransactionByCustomerIdRequestDto.ChargeId, result.Status);
                    return CommonResultResponseDto<string>.Failure(new string[] { "Something went wrong" }, null);
                }
            }
            else
            {
                await _paymentHistoryService.SaveSuccess(
                        addTransactionByCustomerIdRequestDto.CustomerId,
                        addTransactionByCustomerIdRequestDto.Amount,
                        null,
                        null,
                        addTransactionByCustomerIdRequestDto.Description,
                        addTransactionByCustomerIdRequestDto.IsManual,
                        addTransactionByCustomerIdRequestDto.CheckNumber,
                        addTransactionByCustomerIdRequestDto.CheckDate,
                        null, null, false, addTransactionByCustomerIdRequestDto.ChargeId, "Approved");
                return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Created }, null, 1);
            }
                 
        }
        public async Task<CommonResultResponseDto<IList<GetPaymentMethodResponseDto>>> GetPaymentMethod(string cusomerId)
        {
            var paymentMethods = await _cardknoxRepository.GetPaymentMethod(cusomerId);
            return CommonResultResponseDto<IList<GetPaymentMethodResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, paymentMethods);
        }

        public async Task<CommonResultResponseDto<IList<GetPaymentMethodListByFamilyIdResponseDto>>> GetPaymentMethodListByFamilyId(int familyId)
        {
            var paymentMethods = await _cardknoxRepository.GetPaymentMethodListByFamilyId(familyId);
            return CommonResultResponseDto<IList<GetPaymentMethodListByFamilyIdResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, paymentMethods);
        }
    }
}

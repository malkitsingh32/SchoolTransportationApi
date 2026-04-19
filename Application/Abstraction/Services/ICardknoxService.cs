using Application.Common.Dtos;
using Application.Common.Response;
using DTO.Request.CardknoxPaymentMethod;
using DTO.Response;
using DTO.Response.CardknoxPaymentMethod;
using DTO.Response.Driver;

namespace Application.Abstraction.Services
{
    public interface ICardknoxService
    {
        Task<CommonResultResponseDto<PaginatedList<GetCardknoxPaymentMethodByFamilyIdResponseDto>>> GetCardknoxPaymentMethodByFamilyId(string filterModel, ServerRowsRequest commonRequest, string getSort, string familyId);
        Task<CommonResultResponseDto<PaginatedList<GetTransactionByCustomerIdResponseDto>>> GetTransactionByCustomerId(string filterModel, ServerRowsRequest commonRequest, string getSort, string customerId);
        Task<CommonResultResponseDto<string>> AddCardknoxPaymentMethod(AddCardknoxPaymentMethodDto addCardknoxPaymentMethodDto);
        Task<CommonResultResponseDto<string>> DeleteCardknoxPayment(DeleteCardknoxPaymentRequestDto deleteCardknoxPaymentRequestDto);
        Task<CommonResultResponseDto<string>> AddTransactionByCustomerId(AddTransactionByCustomerIdRequestDto addTransactionByCustomerIdRequestDto);
        Task<CommonResultResponseDto<IList<GetPaymentMethodResponseDto>>> GetPaymentMethod(string cusomerId);
        Task<CommonResultResponseDto<IList<GetPaymentMethodListByFamilyIdResponseDto>>> GetPaymentMethodListByFamilyId(int familyId);
    }
}

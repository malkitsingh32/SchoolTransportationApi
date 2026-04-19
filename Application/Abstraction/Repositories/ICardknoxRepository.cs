using Application.Common.Dtos;
using DTO.Request.CardknoxPaymentMethod;
using DTO.Response.CardknoxPaymentMethod;
using DTO.Response.Driver;

namespace Application.Abstraction.Repositories
{
    public interface ICardknoxRepository
    {
        Task<(List<GetCardknoxPaymentMethodByFamilyIdResponseDto>, int)> GetCardknoxPaymentMethodByFamilyId(string filterModel, ServerRowsRequest commonRequest, string getSort, string familyId);
        Task<(List<GetTransactionByCustomerIdResponseDto>, int)> GetTransactionByCustomerId(string filterModel, ServerRowsRequest commonRequest, string getSort, string customerId);
        Task<int> AddCardknoxPaymentMethod(AddCardknoxPaymentMethodDto addCardknoxPaymentMethodDto);
        Task<int> DeleteCardknoxPayment(DeleteCardknoxPaymentRequestDto deleteCardknoxPaymentRequestDto);
        Task<IList<GetPaymentMethodResponseDto>> GetPaymentMethod(string cusomerId);
        Task<IList<GetPaymentMethodListByFamilyIdResponseDto>> GetPaymentMethodListByFamilyId(int familyId);

    }
}

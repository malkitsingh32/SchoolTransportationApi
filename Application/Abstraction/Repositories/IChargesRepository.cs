using Application.Common.Dtos;
using DTO.Request.Charges;
using DTO.Response.Charges;

namespace Application.Abstraction.Repositories
{
    public interface IChargesRepository
    {
        Task<int> AddUpdateCharges(AddUpdateChargesRequestDto addUpdateChargesRequestDto);
        Task<(List<GetChargesResponseDto>, int)> GetCharges(string filterModel, ServerRowsRequest commonRequest, string getSort, int studentId);
    }
}

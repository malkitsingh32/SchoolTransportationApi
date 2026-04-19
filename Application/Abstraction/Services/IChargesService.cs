using Application.Common.Dtos;
using Application.Common.Response;
using DTO.Request.Charges;
using DTO.Response;
using DTO.Response.Charges;

namespace Application.Abstraction.Services
{
    public interface IChargesService
    {
        Task<CommonResultResponseDto<string>> AddUpdateCharges(AddUpdateChargesRequestDto addUpdateChargesRequestDto);
        Task<CommonResultResponseDto<PaginatedList<GetChargesResponseDto>>> GetCharges(string filterModel, ServerRowsRequest commonRequest, string getSort, int studentId);
    }
}

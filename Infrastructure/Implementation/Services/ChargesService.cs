using Application.Abstraction.Repositories;
using Application.Abstraction.Services;
using Application.Common.Dtos;
using Application.Common.Response;
using DTO.Request.Charges;
using DTO.Response;
using DTO.Response.Charges;
using Helper;
using Helper.Constant;

namespace Infrastructure.Implementation.Services
{
    public class ChargesService : IChargesService
    {
        private readonly IChargesRepository _chargesRepository;
        public ChargesService(IChargesRepository chargesRepository)
        {
            _chargesRepository = chargesRepository;
        }

        public async Task<CommonResultResponseDto<string>> AddUpdateCharges(AddUpdateChargesRequestDto AaddUpdateChargesRequestDto)
        {
            var chargeId = await _chargesRepository.AddUpdateCharges(AaddUpdateChargesRequestDto);
            if (chargeId > 0 && chargeId != AaddUpdateChargesRequestDto.ChargeId)
            {
                return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Created }, null, chargeId);
            }
            else if (chargeId < 0)
            {
                return CommonResultResponseDto<string>.Failure(new string[] { "Something went wrong" }, null);
            }
            else
            {
                return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Updated }, null, chargeId);
            }
        }

        public async Task<CommonResultResponseDto<PaginatedList<GetChargesResponseDto>>> GetCharges(string filterModel, ServerRowsRequest commonRequest, string getSort, int studentId)
        {
            var (charges, total) = await _chargesRepository.GetCharges(filterModel, commonRequest, getSort, studentId);
            return CommonResultResponseDto<PaginatedList<GetChargesResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, new PaginatedList<GetChargesResponseDto>(charges, total), 0);
        }
    }
}

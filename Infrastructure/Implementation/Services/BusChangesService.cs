using Application.Abstraction.Repositories;
using Application.Abstraction.Services;
using Application.Common.Dtos;
using Application.Common.Response;
using DTO.Request.BusChanges;
using DTO.Request.CardknoxPaymentMethod;
using DTO.Response;
using DTO.Response.BusChanges;
using DTO.Response.CardknoxPaymentMethod;
using DTO.Response.Students;
using Helper;
using Helper.Constant;
using Infrastructure.Implementation.Repositories;

namespace Infrastructure.Implementation.Services
{
    public class BusChangesService : IBusChangesService
    {
        private readonly IBusChangesRepository _busChangesRepository;
        private readonly IPaymentTransactionService _paymentService;
        private readonly IPaymentHistoryService _paymentHistoryService;
        public BusChangesService(IBusChangesRepository busChangesRepository, IPaymentTransactionService paymentService,
            IPaymentHistoryService paymentHistoryService)
        {
            _busChangesRepository = busChangesRepository;
            _paymentService = paymentService;
            _paymentHistoryService = paymentHistoryService;
        }

        public async Task<CommonResultResponseDto<string>> AddUpdateBusChanges(AddUpdateBusChangesRequestDto addUpdateBusChangesRequestDto)
        {
            if(addUpdateBusChangesRequestDto.Payment == "Yes")
            {
                PaymentResultDto result;
                if (!string.IsNullOrEmpty(addUpdateBusChangesRequestDto.PaymentMethodId))
                {
                    result = await _paymentService.ProcessPaymentAsync(addUpdateBusChangesRequestDto.CustomerId,addUpdateBusChangesRequestDto.Amount, "Bus Change Charges", addUpdateBusChangesRequestDto.PaymentMethodId);
                }
                else
                {
                    result = await _paymentService.ProcessPaymentWithCardAsync(addUpdateBusChangesRequestDto.CustomerId,addUpdateBusChangesRequestDto.Amount, "Bus Change Charges", addUpdateBusChangesRequestDto.CardNumber,addUpdateBusChangesRequestDto.ExpiryDate,addUpdateBusChangesRequestDto.CVV,addUpdateBusChangesRequestDto.CardHolderName);
                }

                if (result.IsSuccess)
                {
                    await _paymentHistoryService.SaveSuccess(addUpdateBusChangesRequestDto.CustomerId, addUpdateBusChangesRequestDto.Amount,result.GatewayRefNum,result.FullResponse, "Bus Change Charges", true,null,null, addUpdateBusChangesRequestDto.PaymentMethodId ?? null, addUpdateBusChangesRequestDto.CardNumber ?? null, true, null, result.Status);
                    var busChangesId = await _busChangesRepository.AddUpdateBusChanges(addUpdateBusChangesRequestDto);
                    if (busChangesId > 0 && busChangesId != addUpdateBusChangesRequestDto.Id)
                    {
                        return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Created }, null, busChangesId);
                    }
                    else if (busChangesId < 0)
                    {
                        return CommonResultResponseDto<string>.Failure(new string[] { "Something went wrong while changing bus." }, null);
                    }
                    else
                    {
                        return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Updated }, null, busChangesId);
                    }
                }
                else
                {
                    await _paymentHistoryService.SaveFailure(addUpdateBusChangesRequestDto.CustomerId,addUpdateBusChangesRequestDto.Amount,result.Error,result.FullResponse, "Bus Change Charges", true,1, addUpdateBusChangesRequestDto.PaymentMethodId ?? null, addUpdateBusChangesRequestDto.CardNumber ?? null, true, null, result.Status);
                    return CommonResultResponseDto<string>.Failure(new string[] { result.Error }, null);
                }
            }
            else
            {
                var busChangesId = await _busChangesRepository.AddUpdateBusChanges(addUpdateBusChangesRequestDto);
                if (busChangesId > 0 && busChangesId != addUpdateBusChangesRequestDto.Id)
                {
                    return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Created }, null, busChangesId);
                }
                else if (busChangesId < 0)
                {
                    return CommonResultResponseDto<string>.Failure(new string[] { "Something went wrong whhile changing bus." }, null);
                }
                else
                {
                    return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Updated }, null, busChangesId);
                }
            }

        }

        public async Task<CommonResultResponseDto<PaginatedList<GetBusChangesDto>>> GetBusChanges(string filterModel, ServerRowsRequest commonRequest, string getSort, int? studentId)
        {
            var (charges, total) = await _busChangesRepository.GetBusChanges(filterModel, commonRequest, getSort,studentId);
            return CommonResultResponseDto<PaginatedList<GetBusChangesDto>>.Success(new string[] { ActionStatusHelper.Success }, new PaginatedList<GetBusChangesDto>(charges, total), 0);
        }

        public async Task<CommonResultResponseDto<PaginatedList<GetStudentsResponseDto>>> GetBusChangeStudents(string filterModel, ServerRowsRequest commonRequest, int routId, string getSort, int streetId, int familyId, string ntId, string dob, string? district, string schoolStudentId, string schoolId, string grade, string? gender)
        {
            var (students, total) = await _busChangesRepository.GetBusChangeStudents(filterModel, commonRequest, routId, getSort, streetId, familyId, ntId, dob, district, schoolStudentId, schoolId, grade, gender);
            return CommonResultResponseDto<PaginatedList<GetStudentsResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, new PaginatedList<GetStudentsResponseDto>(students, total), 0);
        }
        public async Task<CommonResultResponseDto<PaginatedList<GetBusChargesByFamilyIdResponseDto>>> GetBusChargesByFamilyId(string filterModel, ServerRowsRequest commonRequest, string getSort, string familyId)
        {
            var (students, total) = await _busChangesRepository.GetBusChargesByFamilyId(filterModel, commonRequest, getSort, familyId);
            return CommonResultResponseDto<PaginatedList<GetBusChargesByFamilyIdResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, new PaginatedList<GetBusChargesByFamilyIdResponseDto>(students, total), 0);
        }
    }
}

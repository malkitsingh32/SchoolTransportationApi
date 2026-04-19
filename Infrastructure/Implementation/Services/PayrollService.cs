using Application.Abstraction.Repositories;
using Application.Abstraction.Services;
using Application.Common.Dtos;
using Application.Common.Response;
using DTO.Request.Payroll;
using DTO.Response;
using DTO.Response.Payroll;
using Helper;
using Helper.Constant;

namespace Infrastructure.Implementation.Services
{
    public class PayrollService : IPayrollService
    {
        private readonly IPayrollRepository _payrollRepository;
        public PayrollService(IPayrollRepository payrollRepository)
        {
            _payrollRepository = payrollRepository;
        }

        public async Task<CommonResultResponseDto<PaginatedList<GetPayrollResponseDto>>> GetPayroll(string filterModel, ServerRowsRequest commonRequest, string getSort)
        {
            var (payroll, total) = await _payrollRepository.GetPayroll(filterModel, commonRequest, getSort);
            return CommonResultResponseDto<PaginatedList<GetPayrollResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, new PaginatedList<GetPayrollResponseDto>(payroll, total), 0);
        }

        public async Task<CommonResultResponseDto<PaginatedList<GetWeeklyDriverSummaryResponseDto>>> GetWeeklyDriverSummary(string filterModel, ServerRowsRequest commonRequest, string getSort)
        {
            var (payroll, total) = await _payrollRepository.GetWeeklyDriverSummary(filterModel, commonRequest, getSort);
            return CommonResultResponseDto<PaginatedList<GetWeeklyDriverSummaryResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, new PaginatedList<GetWeeklyDriverSummaryResponseDto>(payroll, total), 0);
        }

        public async Task<CommonResultResponseDto<PaginatedList<GetRoutesHistoryByDriverResponseDto>>> GetRoutesHistoryByDriver(string filterModel, ServerRowsRequest commonRequest, string getSort, int driverId, DateTime date)
        {
            var (payroll, total) = await _payrollRepository.GetRoutesHistoryByDriver(filterModel, commonRequest, getSort, driverId, date);
            return CommonResultResponseDto<PaginatedList<GetRoutesHistoryByDriverResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, new PaginatedList<GetRoutesHistoryByDriverResponseDto>(payroll, total), 0);
        }

        public async Task<CommonResultResponseDto<string>> AddUpdatePayrollDetails(AddUpdatePayrollDetailsRequestDto addUpdatePayrollDetailsRequestDto)
        {
            var payrollId = await _payrollRepository.AddUpdatePayrollDetails(addUpdatePayrollDetailsRequestDto);
            if (payrollId > 0 && payrollId != addUpdatePayrollDetailsRequestDto.PayrollId)
            {
                return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Created }, null, payrollId);
            }
            else if (payrollId < 0)
            {
                return CommonResultResponseDto<string>.Failure(new string[] { "Something went wrong" }, null);
            }
            else
            {
                return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Updated }, null, payrollId);
            }
        }

        public async Task<CommonResultResponseDto<PaginatedList<GetPayrollByDateAndDriverIdResponseDto>>> GetPayrollByDateAndDriverId(string filterModel, ServerRowsRequest commonRequest, string getSort, int driverId, DateTime startDate, DateTime endDate)
        {
            var (payroll, total) = await _payrollRepository.GetPayrollByDateAndDriverId(filterModel, commonRequest, getSort, driverId, startDate, endDate);
            return CommonResultResponseDto<PaginatedList<GetPayrollByDateAndDriverIdResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, new PaginatedList<GetPayrollByDateAndDriverIdResponseDto>(payroll, total), 0);
        }

        public async Task<CommonResultResponseDto<string>> DeletePayroll(int driverId, DateTime startDate, DateTime endDate)
        {
            var payroll = await _payrollRepository.DeletePayroll(driverId, startDate, endDate);
            if (payroll > 0)
            {
                return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Deleted }, null, payroll);
            }
            else
            {
                return CommonResultResponseDto<string>.Failure(new string[] { "Something went wrong" }, null);
            }
        }

        public async Task<CommonResultResponseDto<IList<GetWeeklyBulkDriverPayrollResponseDto>>> GetWeeklyBulkDriverPayroll(GetWeeklyBulkDriverPayrollRequestDto getWeeklyBulkDriverPayrollRequestDto)
        {
            var address = await _payrollRepository.GetWeeklyBulkDriverPayroll(getWeeklyBulkDriverPayrollRequestDto);
            return CommonResultResponseDto<IList<GetWeeklyBulkDriverPayrollResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, address);
        }

        public async Task<CommonResultResponseDto<IList<GetPayrollForAllDriversByDateResponseDto>>> GetPayrollForAllDriversByDate(GetPayrollForAllDriversByDateRequestDto getPayrollForAllDriversByDateRequestDto)
        {
            var address = await _payrollRepository.GetPayrollForAllDriversByDate(getPayrollForAllDriversByDateRequestDto);
            return CommonResultResponseDto<IList<GetPayrollForAllDriversByDateResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, address);
        }
    }
}

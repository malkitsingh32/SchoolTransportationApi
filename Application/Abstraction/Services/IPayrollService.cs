using Application.Common.Dtos;
using Application.Common.Response;
using DTO.Request.Payroll;
using DTO.Response;
using DTO.Response.Payroll;

namespace Application.Abstraction.Services
{
    public interface IPayrollService
    {
        Task<CommonResultResponseDto<PaginatedList<GetPayrollResponseDto>>> GetPayroll(string filterModel, ServerRowsRequest commonRequest, string getSort);
        Task<CommonResultResponseDto<PaginatedList<GetWeeklyDriverSummaryResponseDto>>> GetWeeklyDriverSummary(string filterModel, ServerRowsRequest commonRequest, string getSort);
        Task<CommonResultResponseDto<PaginatedList<GetRoutesHistoryByDriverResponseDto>>> GetRoutesHistoryByDriver(string filterModel, ServerRowsRequest commonRequest, string getSort, int driverId, DateTime date);
        Task<CommonResultResponseDto<string>> AddUpdatePayrollDetails(AddUpdatePayrollDetailsRequestDto addUpdatePayrollDetailsRequestDto);
        Task<CommonResultResponseDto<PaginatedList<GetPayrollByDateAndDriverIdResponseDto>>> GetPayrollByDateAndDriverId(string filterModel, ServerRowsRequest commonRequest, string getSort, int driverId, DateTime startDate, DateTime endDate);
        Task<CommonResultResponseDto<string>> DeletePayroll(int driverId, DateTime startDate, DateTime endDate);
        Task<CommonResultResponseDto<IList<GetWeeklyBulkDriverPayrollResponseDto>>> GetWeeklyBulkDriverPayroll(GetWeeklyBulkDriverPayrollRequestDto getWeeklyBulkDriverPayrollRequestDto);
        Task<CommonResultResponseDto<IList<GetPayrollForAllDriversByDateResponseDto>>> GetPayrollForAllDriversByDate(GetPayrollForAllDriversByDateRequestDto getPayrollForAllDriversByDateRequestDto);

    }
}


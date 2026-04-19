using Application.Common.Dtos;
using DTO.Request.Payroll;
using DTO.Response.Payroll;

namespace Application.Abstraction.Repositories
{
    public interface IPayrollRepository
    {
        Task<(List<GetPayrollResponseDto>, int)> GetPayroll(string filterModel, ServerRowsRequest commonRequest, string getSort);
        Task<(List<GetWeeklyDriverSummaryResponseDto>, int)> GetWeeklyDriverSummary(string filterModel, ServerRowsRequest commonRequest, string getSort);
        Task<(List<GetRoutesHistoryByDriverResponseDto>, int)> GetRoutesHistoryByDriver(string filterModel, ServerRowsRequest commonRequest, string getSort, int driverId, DateTime date);
        Task<int> AddUpdatePayrollDetails(AddUpdatePayrollDetailsRequestDto addUpdatePayrollDetailsRequestDto);
        Task<(List<GetPayrollByDateAndDriverIdResponseDto>, int)> GetPayrollByDateAndDriverId(string filterModel, ServerRowsRequest commonRequest, string getSort, int driverId, DateTime startDate, DateTime endDate);
        Task<int> DeletePayroll(int driverId, DateTime startDate, DateTime endDate);
        Task<IList<GetWeeklyBulkDriverPayrollResponseDto>> GetWeeklyBulkDriverPayroll(GetWeeklyBulkDriverPayrollRequestDto getWeeklyBulkDriverPayrollRequestDto);
        Task<IList<GetPayrollForAllDriversByDateResponseDto>> GetPayrollForAllDriversByDate(GetPayrollForAllDriversByDateRequestDto getPayrollForAllDriversByDateRequestDto);

    }
}

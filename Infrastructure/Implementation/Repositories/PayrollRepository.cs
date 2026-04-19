using Application.Abstraction.DataBase;
using Application.Abstraction.Repositories;
using Application.Common.Dtos;
using Dapper;
using DTO.Request.Payroll;
using DTO.Response.Payroll;
using System.Data;

namespace Infrastructure.Implementation.Repositories
{
    public class PayrollRepository : IPayrollRepository
    {
        private readonly IDbContext _dbContext;
        private readonly IParameterManager _parameterManager;
        public PayrollRepository(IDbContext dbContext, IParameterManager parameterManager)
        {
            _dbContext = dbContext;
            _parameterManager = parameterManager;
        }
        public async Task<(List<GetPayrollResponseDto>, int)> GetPayroll(string filterModel, ServerRowsRequest commonRequest, string getSort)
        {
            List<GetPayrollResponseDto> contact;
            int total = 0;
            using (var dbConnection = _dbContext.GetDbConnection())
            {

                var result = await dbConnection.QueryMultipleAsync(
            "usp_GetPayrollByGroup", _dbContext.GetDapperDynamicParameters
            (_parameterManager.Get("@StartRow", commonRequest.StartRow),
              _parameterManager.Get("@EndRow", commonRequest.EndRow),
              _parameterManager.Get("@FilterModel", filterModel),
              _parameterManager.Get("@OrderBy", getSort),
              _parameterManager.Get("@SearchText", commonRequest.SearchText)
            ),
            commandType: CommandType.StoredProcedure);
                total = result.Read<int>().FirstOrDefault();
                contact = result.Read<GetPayrollResponseDto>().ToList();
                dbConnection.Close();
            }
            return (contact, total);
        }
        public async Task<(List<GetWeeklyDriverSummaryResponseDto>, int)> GetWeeklyDriverSummary(string filterModel, ServerRowsRequest commonRequest, string getSort)
        {
            List<GetWeeklyDriverSummaryResponseDto> contact;
            int total = 0;
            using (var dbConnection = _dbContext.GetDbConnection())
            {

                var result = await dbConnection.QueryMultipleAsync(
            "usp_GetWeeklyDriverSummary", _dbContext.GetDapperDynamicParameters
            (_parameterManager.Get("@StartRow", commonRequest.StartRow),
              _parameterManager.Get("@EndRow", commonRequest.EndRow),
              _parameterManager.Get("@FilterModel", filterModel),
              _parameterManager.Get("@OrderBy", getSort),
              _parameterManager.Get("@SearchText", commonRequest.SearchText)
            ),
            commandType: CommandType.StoredProcedure);
                total = result.Read<int>().FirstOrDefault();
                contact = result.Read<GetWeeklyDriverSummaryResponseDto>().ToList();
                dbConnection.Close();
            }
            return (contact, total);
        }

        public async Task<(List<GetRoutesHistoryByDriverResponseDto>, int)> GetRoutesHistoryByDriver(string filterModel, ServerRowsRequest commonRequest, string getSort, int driverId, DateTime date)
        {
            List<GetRoutesHistoryByDriverResponseDto> contact;
            int total = 0;
            using (var dbConnection = _dbContext.GetDbConnection())
            {

                var result = await dbConnection.QueryMultipleAsync(
            "usp_GetRoutesHistoryByDriver", _dbContext.GetDapperDynamicParameters
            (_parameterManager.Get("@StartRow", commonRequest.StartRow),
              _parameterManager.Get("@EndRow", commonRequest.EndRow),
              _parameterManager.Get("@FilterModel", filterModel),
              _parameterManager.Get("@OrderBy", getSort),
              _parameterManager.Get("@SearchText", commonRequest.SearchText),
              _parameterManager.Get("@DriverId", driverId),
              _parameterManager.Get("@Date", date)
            ),
            commandType: CommandType.StoredProcedure);
                total = result.Read<int>().FirstOrDefault();
                contact = result.Read<GetRoutesHistoryByDriverResponseDto>().ToList();
                dbConnection.Close();
            }
            return (contact, total);
        }

        public async Task<int> AddUpdatePayrollDetails(AddUpdatePayrollDetailsRequestDto addUpdatePayrollDetailsRequestDto)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_AddUpdatePayrollDetails",
            _parameterManager.Get("PayrollId", addUpdatePayrollDetailsRequestDto.PayrollId, ParameterDirection.Input, DbType.Int32),
            _parameterManager.Get("Date", addUpdatePayrollDetailsRequestDto.Date, ParameterDirection.Input, DbType.DateTime),
            _parameterManager.Get("Driver", addUpdatePayrollDetailsRequestDto.Driver, ParameterDirection.Input, DbType.Int32),
            _parameterManager.Get("Amount", addUpdatePayrollDetailsRequestDto.Amount, ParameterDirection.Input, DbType.Decimal),
            _parameterManager.Get("TotalRoutes", ConvertToDataTable(addUpdatePayrollDetailsRequestDto.TotalRoutes), ParameterDirection.Input, DbType.Object),
            _parameterManager.Get("RegularRoutes", ConvertToDataTable(addUpdatePayrollDetailsRequestDto.RegularRoutes), ParameterDirection.Input, DbType.Object),
            _parameterManager.Get("PickedUpRoutes", ConvertToDataTable(addUpdatePayrollDetailsRequestDto.PickedUpRoutes), ParameterDirection.Input, DbType.Object),
            _parameterManager.Get("AbsentRoutes", ConvertToDataTable(addUpdatePayrollDetailsRequestDto.AbsentRoutes), ParameterDirection.Input, DbType.Object),
            _parameterManager.Get("UserId", addUpdatePayrollDetailsRequestDto.UserId, ParameterDirection.Input, DbType.Int32));
        }

        public async Task<(List<GetPayrollByDateAndDriverIdResponseDto>, int)> GetPayrollByDateAndDriverId(string filterModel, ServerRowsRequest commonRequest, string getSort, int driverId, DateTime startDate, DateTime endDate)
        {

            List<GetPayrollByDateAndDriverIdResponseDto> contact;
            int total = 0;
            using (var dbConnection = _dbContext.GetDbConnection())
            {

                var result = await dbConnection.QueryMultipleAsync(
            "usp_GetPayrollWithExculsivePaidByDriverAndDate", _dbContext.GetDapperDynamicParameters
            (_parameterManager.Get("@StartRow", commonRequest.StartRow),
              _parameterManager.Get("@EndRow", commonRequest.EndRow),
              _parameterManager.Get("@FilterModel", filterModel),
              _parameterManager.Get("@OrderBy", getSort),
              _parameterManager.Get("@SearchText", commonRequest.SearchText),
              _parameterManager.Get("@DriverID", driverId),
              _parameterManager.Get("@StartDate", startDate),
              _parameterManager.Get("@EndDate", endDate)
            ),
            commandType: CommandType.StoredProcedure);
                total = result.Read<int>().FirstOrDefault();
                contact = result.Read<GetPayrollByDateAndDriverIdResponseDto>().ToList();
                dbConnection.Close();
            }
            return (contact, total);
        }

        static DataTable ConvertToDataTable(int[] array)
        {
            DataTable table = new DataTable();
            table.Columns.Add("Value", typeof(int));

            foreach (int num in array)
            {
                table.Rows.Add(num);
            }

            return table;
        }
        public async Task<int> DeletePayroll(int driverId, DateTime startDate, DateTime endDate)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_DeletePayroll",
               _parameterManager.Get("DriverId", driverId, ParameterDirection.Input, DbType.Int32),
               _parameterManager.Get("StartDate", startDate, ParameterDirection.Input, DbType.DateTime),
               _parameterManager.Get("EndDate", endDate, ParameterDirection.Input, DbType.DateTime)
            );
        }
        public async Task<IList<GetWeeklyBulkDriverPayrollResponseDto>> GetWeeklyBulkDriverPayroll(GetWeeklyBulkDriverPayrollRequestDto getWeeklyBulkDriverPayrollRequestDto)
        {
            return await _dbContext.ExecuteStoredProcedureList<GetWeeklyBulkDriverPayrollResponseDto>("usp_GetDriverWeeklyPrintPayroll",
             _parameterManager.Get("StartDate", getWeeklyBulkDriverPayrollRequestDto.StartDate),
             _parameterManager.Get("EndDate", getWeeklyBulkDriverPayrollRequestDto.EndDate)
         );
        }

        public async Task<IList<GetPayrollForAllDriversByDateResponseDto>> GetPayrollForAllDriversByDate(GetPayrollForAllDriversByDateRequestDto getPayrollForAllDriversByDateRequestDto)
        {
            return await _dbContext.ExecuteStoredProcedureList<GetPayrollForAllDriversByDateResponseDto>("usp_GetPayrollForAllDriversByDate",
              _parameterManager.Get("StartDate", getPayrollForAllDriversByDateRequestDto.StartDate),
              _parameterManager.Get("EndDate", getPayrollForAllDriversByDateRequestDto.EndDate)
          );
        }
    }
}

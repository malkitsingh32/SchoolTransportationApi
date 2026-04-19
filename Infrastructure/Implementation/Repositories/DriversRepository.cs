using Application.Abstraction.DataBase;
using Application.Abstraction.Repositories;
using Application.Common.Dtos;
using Dapper;
using Domain.Entities;
using DTO.Request.Drivers;
using DTO.Request.Routes;
using DTO.Response.Bus;
using DTO.Response.Driver;
using DTO.Response.SystemValues;
using System.Data;

namespace Infrastructure.Implementation.Repositories
{
    public class DriversRepository : IDriversRepository
    {
        private readonly IDbContext _dbContext;
        private readonly IParameterManager _parameterManager;
        public DriversRepository(IDbContext dbContext, IParameterManager parameterManager)
        {
            _dbContext = dbContext;
            _parameterManager = parameterManager;
        }
             
        public async Task<int> AddUpdateDriversDetails(AddUpdateDriversDetailsRequestDto addUpdateDriversDetailsRequestDto)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_AddUpdateDriversDetails",
            _parameterManager.Get("DriverID", addUpdateDriversDetailsRequestDto.DriverID, ParameterDirection.Input, DbType.Int32),
            _parameterManager.Get("FirstName", addUpdateDriversDetailsRequestDto.FirstName, ParameterDirection.Input, DbType.String),
            _parameterManager.Get("LastName", addUpdateDriversDetailsRequestDto.LastName, ParameterDirection.Input, DbType.String),
            _parameterManager.Get("PhoneNumber", addUpdateDriversDetailsRequestDto.PhoneNumber, ParameterDirection.Input, DbType.String),
            _parameterManager.Get("Status", addUpdateDriversDetailsRequestDto.Status, ParameterDirection.Input, DbType.String),           
            _parameterManager.Get("Address", addUpdateDriversDetailsRequestDto.Address, ParameterDirection.Input, DbType.String),
            _parameterManager.Get("Email", addUpdateDriversDetailsRequestDto.Email, ParameterDirection.Input, DbType.String),
            _parameterManager.Get("UserName", addUpdateDriversDetailsRequestDto.UserName, ParameterDirection.Input, DbType.String),
            _parameterManager.Get("UserId", addUpdateDriversDetailsRequestDto.UserId, ParameterDirection.Input, DbType.String),
            _parameterManager.Get("RouteId", addUpdateDriversDetailsRequestDto.Route, ParameterDirection.Input, DbType.String),
            _parameterManager.Get("BusId", addUpdateDriversDetailsRequestDto.Bus, ParameterDirection.Input, DbType.String),
            _parameterManager.Get("Bus", addUpdateDriversDetailsRequestDto.BusId, ParameterDirection.Input, DbType.String),
            _parameterManager.Get("TempBus", addUpdateDriversDetailsRequestDto.TempBus, ParameterDirection.Input, DbType.String),
            _parameterManager.Get("RunType", addUpdateDriversDetailsRequestDto.RunType, ParameterDirection.Input, DbType.String),
            _parameterManager.Get("TempBusStartTime", addUpdateDriversDetailsRequestDto.TempBusStartTime, ParameterDirection.Input, DbType.DateTime),
            _parameterManager.Get("TempBusEndTime", addUpdateDriversDetailsRequestDto.TempBusEndTime, ParameterDirection.Input, DbType.DateTime),
            _parameterManager.Get("DriverType", addUpdateDriversDetailsRequestDto.DriverType, ParameterDirection.Input, DbType.Int32),
            _parameterManager.Get("IsDelete", addUpdateDriversDetailsRequestDto.IsDelete, ParameterDirection.Input, DbType.Boolean),
            _parameterManager.Get("IsOverWrite", addUpdateDriversDetailsRequestDto.IsOverWrite, ParameterDirection.Input, DbType.Boolean),
            _parameterManager.Get("PayrollAccountBalance", addUpdateDriversDetailsRequestDto.PayRate, ParameterDirection.Input, DbType.Decimal)
            );
        }
        
        public async Task<int> DeductReservedAccountBalance(DeductReservedAccountBalanceRequestDto deductReservedAccountBalanceRequestDto)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_DeductReservedAccountBalance",
            _parameterManager.Get("DriverId", deductReservedAccountBalanceRequestDto.DriverId, ParameterDirection.Input, DbType.Int32),
            _parameterManager.Get("AssignedDriverId", deductReservedAccountBalanceRequestDto.AssignedDriverId, ParameterDirection.Input, DbType.Int32),
            _parameterManager.Get("RouteId", deductReservedAccountBalanceRequestDto.RouteId, ParameterDirection.Input, DbType.Int32),
            _parameterManager.Get("RouteGroupId", deductReservedAccountBalanceRequestDto.RouteGroupId, ParameterDirection.Input, DbType.Guid),
            _parameterManager.Get("RouteDate", deductReservedAccountBalanceRequestDto.RouteDate, ParameterDirection.Input, DbType.DateTime),
            _parameterManager.Get("DeductionAmount", deductReservedAccountBalanceRequestDto.deductionAmount, ParameterDirection.Input, DbType.Decimal),
            _parameterManager.Get("isCreditToTodayDriver", deductReservedAccountBalanceRequestDto.isCreditToTodayDriver, ParameterDirection.Input, DbType.Boolean),
            _parameterManager.Get("UserId", deductReservedAccountBalanceRequestDto.UserId, ParameterDirection.Input, DbType.Int32),
            _parameterManager.Get("RouteDetailId", deductReservedAccountBalanceRequestDto.RouteDetailId, ParameterDirection.Input, DbType.Int32)
            );
        }

        public async Task<string> DeleteDrivers(int id, bool IsFromRoute, int? RouteId)
        {
            return await _dbContext.ExecuteStoredProcedure<string>("usp_DeleteDrivers",
           _parameterManager.Get("Id", id, ParameterDirection.Input, DbType.Int32),
           _parameterManager.Get("RouteId", RouteId, ParameterDirection.Input, DbType.Int32),
           _parameterManager.Get("IsFromRoute", IsFromRoute, ParameterDirection.Input, DbType.Boolean));
        }

        public async Task<(List<GetDriversResponseDto>, int)> GetDrivers(string filterModel, ServerRowsRequest commonRequest, string getSort, int routeId)
        {
            List<GetDriversResponseDto> contact;
            int total = 0;
            using (var dbConnection = _dbContext.GetDbConnection())
            {

                var result = await dbConnection.QueryMultipleAsync(
            "usp_GetDrivers", _dbContext.GetDapperDynamicParameters
            (_parameterManager.Get("@StartRow", commonRequest.StartRow),
              _parameterManager.Get("@EndRow", commonRequest.EndRow),
              _parameterManager.Get("@FilterModel", filterModel),
              _parameterManager.Get("@OrderBy", getSort),
              _parameterManager.Get("@SearchText", commonRequest.SearchText),
              _parameterManager.Get("@RouteId", routeId)
            ),
            commandType: CommandType.StoredProcedure);
                total = result.Read<int>().FirstOrDefault();
                contact = result.Read<GetDriversResponseDto>().ToList();
                dbConnection.Close();
            }
            return (contact, total);
        }
        
        public async Task<(List<GetDriversBalanceHistoryResponseDto>, int)> GetDriversBalanceHistory(string filterModel, ServerRowsRequest commonRequest, string getSort, int driverId)
        {
            List<GetDriversBalanceHistoryResponseDto> contact;
            int total = 0;
            using (var dbConnection = _dbContext.GetDbConnection())
            {

                var result = await dbConnection.QueryMultipleAsync(
            "usp_GetDriversBalanceHistory", _dbContext.GetDapperDynamicParameters
            (_parameterManager.Get("@StartRow", commonRequest.StartRow),
              _parameterManager.Get("@EndRow", commonRequest.EndRow),
              _parameterManager.Get("@FilterModel", filterModel),
              _parameterManager.Get("@OrderBy", getSort),
              _parameterManager.Get("@SearchText", commonRequest.SearchText),
              _parameterManager.Get("@DriverId", driverId)
            ),
            commandType: CommandType.StoredProcedure);
                total = result.Read<int>().FirstOrDefault();
                contact = result.Read<GetDriversBalanceHistoryResponseDto>().ToList();
                dbConnection.Close();
            }
            return (contact, total);
        }

        public async Task<IList<GetDriversListResponseDto>> GetDriversList()
        {
            return await _dbContext.ExecuteStoredProcedureList<GetDriversListResponseDto>("usp_GetDriversList");
        }

        public async Task<IList<GetDriversResponseDto>> GetDriversByBusId(string? id)
        {
            return await _dbContext.ExecuteStoredProcedureList<GetDriversResponseDto>("usp_GetDriversByBusId",
            _parameterManager.Get("@BusId", id, ParameterDirection.Input, DbType.String));
        }

        public async Task<IList<GetDriverTypeResponseDto>> GetDriverTypeList()
        {
            return await _dbContext.ExecuteStoredProcedureList<GetDriverTypeResponseDto>("usp_GetDriverTypeList");
        }

        public async Task<string> ResetDriverPassword(Drivers drivers, ResetDriverPasswordDto resetDriverPasswordDto)
        {
            return await _dbContext.ExecuteStoredProcedure<string>("usp_ResetDriverPassword",
         _parameterManager.Get("@DriverId", resetDriverPasswordDto.DriverId),
         _parameterManager.Get("@Email", resetDriverPasswordDto.Email),
         _parameterManager.Get("@PasswordHash", drivers.PasswordHash, ParameterDirection.Input, DbType.Binary),
         _parameterManager.Get("@PasswordSalt", drivers.PasswordSalt, ParameterDirection.Input, DbType.Binary)
         );
        }

        public async Task<Drivers> GetDriverByEmail(string email, int driverId)
        {
                var ss = await _dbContext.ExecuteStoredProcedure<Drivers>("usp_GetDriverByEmail",
                _parameterManager.Get("@Email", email),
                _parameterManager.Get("@DriverId", driverId)
                );
            return ss;
        }

        public async Task<string> SendOtpOnEmail(Drivers drivers)
        {
            return await _dbContext.ExecuteStoredProcedure<string>("usp_SendOtpOnEmail",
               _parameterManager.Get("@Email", drivers.Email),
               _parameterManager.Get("@OtpCode", drivers.OtpCode)
            );
          
        }

        public async Task<Drivers> CheckOTP(CheckOTPRequestDto checkOTPRequestDto)
        {
            return await _dbContext.ExecuteStoredProcedure<Drivers>("usp_CheckOTP",
                _parameterManager.Get("@Email", checkOTPRequestDto.Email),
                _parameterManager.Get("@OTP", checkOTPRequestDto.Otp)
            );
        }

        public async Task<string> ResetUserPassword(Users users, ResetDriverPasswordDto resetDriverPasswordDto)
        {
            return await _dbContext.ExecuteStoredProcedure<string>("usp_ResetUserPassword",
        _parameterManager.Get("@Email", resetDriverPasswordDto.Email),
        _parameterManager.Get("@PasswordHash", users.PasswordHash, ParameterDirection.Input, DbType.Binary),
        _parameterManager.Get("@PasswordSalt", users.PasswordSalt, ParameterDirection.Input, DbType.Binary)
        );
        }

        public async Task<int> UpdateDriversDetail(Users users, byte[]? passwordHash, byte[]? passwordSalt)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_UpdateDriversDetail",
             _parameterManager.Get("DriverID", users.Id, ParameterDirection.Input, DbType.Int32),
             _parameterManager.Get("FirstName", users.FirstName, ParameterDirection.Input, DbType.String),
             _parameterManager.Get("LastName", users.LastName, ParameterDirection.Input, DbType.String),
             _parameterManager.Get("Email", users.Email, ParameterDirection.Input, DbType.String),
             _parameterManager.Get("UserName", users.UserName, ParameterDirection.Input, DbType.String),
             _parameterManager.Get("PhoneNumber", users.PhoneNumber, ParameterDirection.Input, DbType.String),
             _parameterManager.Get("Status", users.Status, ParameterDirection.Input, DbType.String),
             _parameterManager.Get("@PasswordHash", passwordHash, ParameterDirection.Input, DbType.Binary),
             _parameterManager.Get("@PasswordSalt", passwordSalt, ParameterDirection.Input, DbType.Binary)
             );
        }

        public async Task<int> IsExistEmail(Users users)
        {
            var result = await _dbContext.ExecuteStoredProcedure<int>(
                "usp_IsExistEmail",
                _parameterManager.Get("@Email", users.Email),
                _parameterManager.Get("@Id", users.Id)
            );

            return result; 
        }

        public async Task<int> AssignRouteToDriver(AssignRouteToDriverRequestDto assignRouteToDriverRequestDto)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_AssignRouteToDriver",
             _parameterManager.Get("DriverId", assignRouteToDriverRequestDto.DriverId, ParameterDirection.Input, DbType.Int32),
             _parameterManager.Get("RouteId", assignRouteToDriverRequestDto.RouteId, ParameterDirection.Input, DbType.Int32),
             _parameterManager.Get("RouteDate", assignRouteToDriverRequestDto.RouteDate, ParameterDirection.Input, DbType.DateTime)
             );
        }

        public async Task<int> DeleteBusAndDriverRoute(DeleteBusAndDriverRouteRequestDto deleteBusAndDriverRouteRequestDto)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_DeleteBusAndDriverRoute",
          _parameterManager.Get("RouteDetailID", deleteBusAndDriverRouteRequestDto.RouteDetailID, ParameterDirection.Input, DbType.Int32),
          _parameterManager.Get("IsDeleteDriver", deleteBusAndDriverRouteRequestDto.IsDeleteDriver, ParameterDirection.Input, DbType.Boolean));
        }

        public async Task<CheckDriverHasBusResponseDto> CheckBusHasDriver(AddUpdateDriversDetailsRequestDto addUpdateDriversDetailsRequestDto)
        {

            return await _dbContext.ExecuteStoredProcedure<CheckDriverHasBusResponseDto>("usp_CheckBusHasDriver",
            _parameterManager.Get("Bus", addUpdateDriversDetailsRequestDto.Bus, ParameterDirection.Input, DbType.Int64)
            );
        }

        public async Task<IList<ExportDriversListDto>> ExportDriversList()
        {
            List<ExportDriversListDto> newApplications;

            using (var dbConnection = _dbContext.GetDbConnection())
            {
                var result = await dbConnection.QueryMultipleAsync(
                    "usp_ExportDriversList",
                    commandType: CommandType.StoredProcedure
                );

                newApplications = result.Read<ExportDriversListDto>().ToList();

                return (newApplications);
            }
        }
    }
}

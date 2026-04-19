using Application.Abstraction.DataBase;
using Application.Abstraction.Repositories;
using Application.Common.Dtos;
using Dapper;
using DTO.Request.BusDetails;
using DTO.Response.Bus;
using DTO.Response.BusStatus;
using System.Data;

namespace Infrastructure.Implementation.Repositories
{
    public class BusDetailsRepository : IBusDetailsRepository
    {
        private readonly IDbContext _dbContext;
        private readonly IParameterManager _parameterManager;
        public BusDetailsRepository(IDbContext dbContext, IParameterManager parameterManager)
        {
            _dbContext = dbContext;
            _parameterManager = parameterManager;
        }

        public async Task<int> AddUpdateBusDetails(AddUpdateBusDetailsRequestDto addUpdateBusDetailsRequestDto)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_AddUpdateBusDetails",
      _parameterManager.Get("@BusDetailID", addUpdateBusDetailsRequestDto.BusDetailID, ParameterDirection.Input, DbType.Int32),
      _parameterManager.Get("@BusName", addUpdateBusDetailsRequestDto.BusName, ParameterDirection.Input, DbType.String),
      _parameterManager.Get("@Station", addUpdateBusDetailsRequestDto.Station, ParameterDirection.Input, DbType.String),
      _parameterManager.Get("@BusNumber", addUpdateBusDetailsRequestDto.BusNumber, ParameterDirection.Input, DbType.String),
      _parameterManager.Get("@YearOfManufacture", addUpdateBusDetailsRequestDto.YearOfManufacture, ParameterDirection.Input, DbType.String),
      _parameterManager.Get("@UserId", addUpdateBusDetailsRequestDto.UserId, ParameterDirection.Input, DbType.Int32),
       _parameterManager.Get("@Route", addUpdateBusDetailsRequestDto.Route, ParameterDirection.Input, DbType.Int64),
       _parameterManager.Get("@DefaultDriver", addUpdateBusDetailsRequestDto.DefaultDriver, ParameterDirection.Input, DbType.String),
       _parameterManager.Get("@RunType", addUpdateBusDetailsRequestDto.RunType, ParameterDirection.Input, DbType.String),
       _parameterManager.Get("@TempDefaultDriver", addUpdateBusDetailsRequestDto.TempDefaultDriver, ParameterDirection.Input, DbType.String),
       _parameterManager.Get("@TempRunType", addUpdateBusDetailsRequestDto.TempRunType, ParameterDirection.Input, DbType.String),
       _parameterManager.Get("@BusStatus", addUpdateBusDetailsRequestDto.Status, ParameterDirection.Input, DbType.Int64),
       _parameterManager.Get("@PreviousBusId", addUpdateBusDetailsRequestDto.PreviousBusId, ParameterDirection.Input, DbType.Int64),
       _parameterManager.Get("@IsDelete", addUpdateBusDetailsRequestDto.IsDelete, ParameterDirection.Input, DbType.Boolean),
       _parameterManager.Get("IsOverWrite", addUpdateBusDetailsRequestDto.IsOverWrite, ParameterDirection.Input, DbType.Boolean)

            );
        }

        public async Task<IList<CheckDriverHasBusResponseDto>> CheckDriverHasBus(AddUpdateBusDetailsRequestDto addUpdateBusDetailsRequestDto)
        {
            return await _dbContext.ExecuteStoredProcedureList<CheckDriverHasBusResponseDto>("usp_CheckDriverHasBus",
            _parameterManager.Get("@BusDetailID", addUpdateBusDetailsRequestDto.BusDetailID, ParameterDirection.Input, DbType.Int32),
            _parameterManager.Get("@DefaultDriver", addUpdateBusDetailsRequestDto.DefaultDriver)          
            );
        }
        public async Task<IList<CheckDriverHasBusResponseDto>> CheckDriverHasTempBus(AddUpdateBusDetailsRequestDto addUpdateBusDetailsRequestDto)
        {
            return await _dbContext.ExecuteStoredProcedureList<CheckDriverHasBusResponseDto>("usp_CheckDriverHasTempBus",
            _parameterManager.Get("@BusDetailID", addUpdateBusDetailsRequestDto.BusDetailID, ParameterDirection.Input, DbType.Int32),
            _parameterManager.Get("@TempDefaultDriver", addUpdateBusDetailsRequestDto.TempDefaultDriver)          
            );
        }

        public async Task<string> DeleteBusDetails(int id)
        {
            return await _dbContext.ExecuteStoredProcedure<string>("usp_DeleteBusDetails",
         _parameterManager.Get("Id", id, ParameterDirection.Input, DbType.Int32));
        }

        public async Task<IList<GetBusesResponseDto>> GetAllBusDetails()
        {
                return await _dbContext.ExecuteStoredProcedureList<GetBusesResponseDto>("usp_GetAllBusDetails");
        }

        public async Task<(List<GetBusesResponseDto>, int)> GetBuses(string filterModel, ServerRowsRequest commonRequest, string getSort, int routId)
        {
            List<GetBusesResponseDto> contact;
            int total = 0;
            using (var dbConnection = _dbContext.GetDbConnection())
            {
                var result = await dbConnection.QueryMultipleAsync(
                "usp_GetBuses", _dbContext.GetDapperDynamicParameters
                    (_parameterManager.Get("@StartRow", commonRequest.StartRow),
                      _parameterManager.Get("@EndRow", commonRequest.EndRow),
                      _parameterManager.Get("@FilterModel", filterModel),
                      _parameterManager.Get("@OrderBy", getSort),
                      _parameterManager.Get("@SearchText", commonRequest.SearchText),
                      _parameterManager.Get("@RouteId", routId)
                    ),
                commandType: CommandType.StoredProcedure);
                total = result.Read<int>().FirstOrDefault();
                contact = result.Read<GetBusesResponseDto>().ToList();
                dbConnection.Close();
            }
            return (contact, total);
        }

        public async Task<IList<GetBusStatusResponseDto>> GetBusStatus()
        {
            return await _dbContext.ExecuteStoredProcedureList<GetBusStatusResponseDto>("usp_GetBusStatus");
        }

        public async Task<IList<BusPosition>> GetRecentBusPositions(string busName, int routeId)
        {
            return await _dbContext.ExecuteStoredProcedureList<BusPosition>("usp_GetRecentBusPositions",
       _parameterManager.Get("BusName", busName, ParameterDirection.Input, DbType.String),
       _parameterManager.Get("RouteId", routeId, ParameterDirection.Input, DbType.Int64)
       );
        }
    }
}

using Application.Abstraction.DataBase;
using Application.Abstraction.Repositories;
using Application.Common.Dtos;
using Dapper;
using DTO.Request.Street;
using DTO.Response.Streets;
using System.Data;

namespace Infrastructure.Implementation.Repositories
{
    public class StreetsRepository : IStreetsRepository
    {
        private readonly IDbContext _dbContext;
        private readonly IParameterManager _parameterManager;
        public StreetsRepository(IDbContext dbContext, IParameterManager parameterManager)
        {
            _dbContext = dbContext;
            _parameterManager = parameterManager;
        }

        public async Task<int> AddUpdateStreets(AddUpdateStreetsDto addUpdateStreetsDto)                
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_AddUpdateStreets",
      _parameterManager.Get("ID", addUpdateStreetsDto.ID, ParameterDirection.Input, DbType.Int32),
      _parameterManager.Get("StreetName", addUpdateStreetsDto.StreetName, ParameterDirection.Input, DbType.String),
      _parameterManager.Get("Area", addUpdateStreetsDto.Area, ParameterDirection.Input, DbType.Int32),
      _parameterManager.Get("FromNumber", addUpdateStreetsDto.FromNumber, ParameterDirection.Input, DbType.Int32),
      _parameterManager.Get("ToNumber", addUpdateStreetsDto.ToNumber, ParameterDirection.Input, DbType.Int32),
      _parameterManager.Get("NumberOfRoutes", addUpdateStreetsDto.NumberOfRoutes, ParameterDirection.Input, DbType.Int32),
      _parameterManager.Get("UserId", addUpdateStreetsDto.UserId, ParameterDirection.Input, DbType.Int32),
      _parameterManager.Get("RouteId", addUpdateStreetsDto.RouteId, ParameterDirection.Input, DbType.Int32),
       _parameterManager.Get("IsDelete", addUpdateStreetsDto.IsDelete, ParameterDirection.Input, DbType.Boolean),
       _parameterManager.Get("AddStreet", addUpdateStreetsDto.AddStreet, ParameterDirection.Input, DbType.Boolean)
);
        }

        public async Task<int> DeleteStreets(int id)
        {
         return await _dbContext.ExecuteStoredProcedure<int>("usp_DeleteStreets",
         _parameterManager.Get("Id", id, ParameterDirection.Input, DbType.Int32));
        }

        public async Task<(List<GetStreetsResponseDto>, int)> GetStreets(string filterModel, ServerRowsRequest commonRequest, string getSort, int routeId)
        {
            List<GetStreetsResponseDto> contact;
            int total = 0;
            using (var dbConnection = _dbContext.GetDbConnection())
            {

                var result = await dbConnection.QueryMultipleAsync(
            "usp_GetStreets", _dbContext.GetDapperDynamicParameters
            (_parameterManager.Get("@StartRow", commonRequest.StartRow),
              _parameterManager.Get("@EndRow", commonRequest.EndRow),
              _parameterManager.Get("@FilterModel", filterModel),
              _parameterManager.Get("@OrderBy", getSort),
              _parameterManager.Get("@SearchText", commonRequest.SearchText),
               _parameterManager.Get("@RouteId", routeId)
            ),
            commandType: CommandType.StoredProcedure);
                total = result.Read<int>().FirstOrDefault();
                contact = result.Read<GetStreetsResponseDto>().ToList();
                dbConnection.Close();
            }
            return (contact, total);
        }
        public async Task<IList<GetStreetListResponseDto>> GetStreetList()
        {
            return await _dbContext.ExecuteStoredProcedureList<GetStreetListResponseDto>("usp_GetStreetList");
        }

        public async Task<(List<GetStreetsResponseDto>, int)> GetStreetsByRouteAndArea(string filterModel, ServerRowsRequest commonRequest, string getSort, int routeId)
        {
            List<GetStreetsResponseDto> contact;
            int total = 0;
            using (var dbConnection = _dbContext.GetDbConnection())
            {

                var result = await dbConnection.QueryMultipleAsync(
            "usp_GetStreetsByRouteAndArea", _dbContext.GetDapperDynamicParameters
            (_parameterManager.Get("@StartRow", commonRequest.StartRow),
              _parameterManager.Get("@EndRow", commonRequest.EndRow),
              _parameterManager.Get("@FilterModel", filterModel),
              _parameterManager.Get("@OrderBy", getSort),
              _parameterManager.Get("@SearchText", commonRequest.SearchText),
               _parameterManager.Get("@RouteId", routeId)
            ),
            commandType: CommandType.StoredProcedure);
                total = result.Read<int>().FirstOrDefault();
                contact = result.Read<GetStreetsResponseDto>().ToList();
                dbConnection.Close();
            }
            return (contact, total);
        }

        public async Task<int> UpdateStreetRouteMapping(string streetJson)
        {
                return await _dbContext.ExecuteStoredProcedure<int>("usp_UpdateStreetRouteMapping",
                     _parameterManager.Get("StreetJSON", streetJson, ParameterDirection.Input, DbType.String)
                );
        }

        public async Task<int> AddSchoolBuildingBranchMapping(string schoolJson)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_AddSchoolBuildingBranchMapping",
                     _parameterManager.Get("SchoolJson", schoolJson, ParameterDirection.Input, DbType.String)
                );
        }
    }
}

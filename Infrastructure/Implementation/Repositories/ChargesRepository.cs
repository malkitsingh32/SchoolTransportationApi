using Application.Abstraction.DataBase;
using Application.Abstraction.Repositories;
using Application.Common.Dtos;
using Dapper;
using DTO.Request.Charges;
using DTO.Response.Charges;
using System.Data;

namespace Infrastructure.Implementation.Repositories
{
    public class ChargesRepository : IChargesRepository
    {
        private readonly IDbContext _dbContext;
        private readonly IParameterManager _parameterManager;
        public ChargesRepository(IDbContext dbContext, IParameterManager parameterManager)
        {
            _dbContext = dbContext;
            _parameterManager = parameterManager;
        }

        public async Task<int> AddUpdateCharges(AddUpdateChargesRequestDto addUpdateChargesRequestDto)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_AddUpdateCharges",
            _parameterManager.Get("ChargeId", addUpdateChargesRequestDto.ChargeId, ParameterDirection.Input, DbType.Int32),
            _parameterManager.Get("StudentId", addUpdateChargesRequestDto.StudentId, ParameterDirection.Input, DbType.String),
            _parameterManager.Get("ChargeAmount", addUpdateChargesRequestDto.ChargeAmount, ParameterDirection.Input, DbType.String),
            _parameterManager.Get("ChargeDate", addUpdateChargesRequestDto.ChargeDate, ParameterDirection.Input, DbType.String),
            _parameterManager.Get("ChargeStatus", addUpdateChargesRequestDto.ChargeStatus, ParameterDirection.Input, DbType.String),
            _parameterManager.Get("UserId", addUpdateChargesRequestDto.UserId, ParameterDirection.Input, DbType.String),
            _parameterManager.Get("IsDelete", addUpdateChargesRequestDto.IsDelete, ParameterDirection.Input, DbType.Boolean)
            );
        }

        public async Task<(List<GetChargesResponseDto>, int)> GetCharges(string filterModel, ServerRowsRequest commonRequest, string getSort, int studentId)
        {
            List<GetChargesResponseDto> contact;
            int total = 0;
            using (var dbConnection = _dbContext.GetDbConnection())
            {

                var result = await dbConnection.QueryMultipleAsync(
            "usp_GetCharges", _dbContext.GetDapperDynamicParameters
            (_parameterManager.Get("@StartRow", commonRequest.StartRow),
              _parameterManager.Get("@EndRow", commonRequest.EndRow),
              _parameterManager.Get("@FilterModel", filterModel),
              _parameterManager.Get("@OrderBy", getSort),
              _parameterManager.Get("@SearchText", commonRequest.SearchText),
              _parameterManager.Get("@StudentId", studentId)
            ),
            commandType: CommandType.StoredProcedure);
                total = result.Read<int>().FirstOrDefault();
                contact = result.Read<GetChargesResponseDto>().ToList();
                dbConnection.Close();
            }
            return (contact, total);
        }
    }
}

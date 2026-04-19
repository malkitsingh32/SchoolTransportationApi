using Application.Abstraction.DataBase;
using Application.Abstraction.Repositories;
using Application.Common.Dtos;
using Dapper;
using Domain.Entities;
using DTO.Request.BusChanges;
using DTO.Response.BusChanges;
using DTO.Response.Students;
using System.Data;
using System.Runtime.InteropServices;

namespace Infrastructure.Implementation.Repositories
{
    public class BusChangesRepository : IBusChangesRepository
    {
        private readonly IDbContext _dbContext;
        private readonly IParameterManager _parameterManager;
        public BusChangesRepository(IDbContext dbContext, IParameterManager parameterManager)
        {
            _dbContext = dbContext;
            _parameterManager = parameterManager;
        }

        public async Task<int> AddUpdateBusChanges(AddUpdateBusChangesRequestDto request)
        {
            return await _dbContext.ExecuteStoredProcedure<int>(
                "usp_AddUpdateBusChanges",
                _parameterManager.Get("Id", request.Id, ParameterDirection.Input, DbType.Int32),
                _parameterManager.Get("StudentId", request.StudentId, ParameterDirection.Input, DbType.Int32),
                _parameterManager.Get("FamilyId", request.FamilyId, ParameterDirection.Input, DbType.Int32),
                _parameterManager.Get("StudentName", request.StudentName, ParameterDirection.Input, DbType.String),
                _parameterManager.Get("StudentOriginalAddress", request.StudentOriginalAddress, ParameterDirection.Input, DbType.String),
                _parameterManager.Get("BusChangeAddress", request.BusChangeAddress, ParameterDirection.Input, DbType.String),
                _parameterManager.Get("Area", request.Area, ParameterDirection.Input, DbType.String),
                _parameterManager.Get("SchoolId", request.SchoolId, ParameterDirection.Input, DbType.Int32),
                _parameterManager.Get("Gender", request.Gender, ParameterDirection.Input, DbType.Int32),
                _parameterManager.Get("Grade", request.Grade, ParameterDirection.Input, DbType.Int32),
                _parameterManager.Get("BranchId", request.BranchId, ParameterDirection.Input, DbType.Int32),
                _parameterManager.Get("StartDate", request.StartDate, ParameterDirection.Input, DbType.Date),
                _parameterManager.Get("EndDate", request.EndDate, ParameterDirection.Input, DbType.Date),
                _parameterManager.Get("StartFrom", request.StartFrom, ParameterDirection.Input, DbType.String),
                _parameterManager.Get("Include", request.Include, ParameterDirection.Input, DbType.String),
                _parameterManager.Get("BusChangePhone", request.BusChangePhone, ParameterDirection.Input, DbType.String),
                _parameterManager.Get("MotherCell", request.MotherCell, ParameterDirection.Input, DbType.String),
                _parameterManager.Get("RouteId", request.RouteId, ParameterDirection.Input, DbType.String),
                _parameterManager.Get("Payment", request.Payment, ParameterDirection.Input, DbType.String),
                _parameterManager.Get("ProcessPaymentMethod", request.ProcessPaymentMethod, ParameterDirection.Input, DbType.String)
            );
        }

        public async Task<(List<GetBusChangesDto>, int)> GetBusChanges(string filterModel, ServerRowsRequest commonRequest, string getSort, int? studentId)
        {
            List<GetBusChangesDto> contact;
            int total = 0;
            using (var dbConnection = _dbContext.GetDbConnection())
            {

                var result = await dbConnection.QueryMultipleAsync(
            "usp_GetBusChanges", _dbContext.GetDapperDynamicParameters
            (_parameterManager.Get("@StartRow", commonRequest.StartRow),
              _parameterManager.Get("@EndRow", commonRequest.EndRow),
              _parameterManager.Get("@FilterModel", filterModel),
              _parameterManager.Get("@OrderBy", getSort),
              _parameterManager.Get("@SearchText", commonRequest.SearchText),
              _parameterManager.Get("@StudentId", studentId)
            ),
            commandType: CommandType.StoredProcedure);
                total = result.Read<int>().FirstOrDefault();
                contact = result.Read<GetBusChangesDto>().ToList();
                dbConnection.Close();
            }
            return (contact, total);
        }

        public async Task<(List<GetStudentsResponseDto>, int)> GetBusChangeStudents(string filterModel, ServerRowsRequest commonRequest, int routId, string getSort, int streetId, int familyId, string ntId, string dob, string? district, string schoolStudentId, string schoolId, string grade, string? gender)
        {
            List<GetStudentsResponseDto> contact;
            int total = 0;
            using (var dbConnection = _dbContext.GetDbConnection())
            {
                var result = await dbConnection.QueryMultipleAsync(
                "usp_GetBusChangeStudents", _dbContext.GetDapperDynamicParameters
                    (_parameterManager.Get("@StartRow", commonRequest.StartRow),
                     _parameterManager.Get("@EndRow", commonRequest.EndRow),
                     _parameterManager.Get("@FilterModel", filterModel),
                     _parameterManager.Get("@OrderBy", getSort),
                     _parameterManager.Get("@SearchText", commonRequest.SearchText),
                     _parameterManager.Get("@RouteId", routId),
                      _parameterManager.Get("@StreetId", streetId),
                      _parameterManager.Get("@FamilyId", familyId),
                      _parameterManager.Get("@NtId", ntId),
                      _parameterManager.Get("@Dob", dob),
                      _parameterManager.Get("@District", district),
                      _parameterManager.Get("@SchoolStudentId", schoolStudentId),
                      _parameterManager.Get("@SchoolId", schoolId),
                      _parameterManager.Get("@Grade", grade),
                      _parameterManager.Get("@Gender", gender)
                    ),
                commandType: CommandType.StoredProcedure);
                total = result.Read<int>().FirstOrDefault();
                contact = result.Read<GetStudentsResponseDto>().ToList();
                dbConnection.Close();
            }
            return (contact, total);
        }
        public async Task<(List<GetBusChargesByFamilyIdResponseDto>, int)> GetBusChargesByFamilyId(string filterModel, ServerRowsRequest commonRequest, string getSort, string familyId)
        {
            List<GetBusChargesByFamilyIdResponseDto> response = new();
            int total = 0;

            using (var dbConnection = _dbContext.GetDbConnection())
            {
                var result = await dbConnection.QueryMultipleAsync(
                    "usp_GetBusChangeChargesByFamilyId",
                    _dbContext.GetDapperDynamicParameters(
                        _parameterManager.Get("@StartRow", commonRequest.StartRow),
                        _parameterManager.Get("@EndRow", commonRequest.EndRow),
                        _parameterManager.Get("@FilterModel", filterModel),
                        _parameterManager.Get("@OrderBy", getSort),
                        _parameterManager.Get("@SearchText", commonRequest.SearchText),
                        _parameterManager.Get("@FamilyId", familyId)
                    ),
                commandType: CommandType.StoredProcedure);
                total = result.Read<int>().FirstOrDefault();
                response = result.Read<GetBusChargesByFamilyIdResponseDto>().ToList();
                dbConnection.Close();
            }
            return (response, total);
        }

    }
}

using Application.Abstraction.DataBase;
using Application.Abstraction.Repositories;
using Application.Common.Dtos;
using Application.Common.Response;
using Dapper;
using Domain.Entities;
using DTO.Request.Admin;
using DTO.Response;
using DTO.Response.Admin;
using DTO.Response.SystemValues;
using DTO.Response.User;
using Helper;
using Newtonsoft.Json;
using System.Data;

namespace Infrastructure.Implementation.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly IDbContext _dbContext;
        private readonly IParameterManager _parameterManager;
        public AdminRepository(IDbContext dbContext, IParameterManager parameterManager)
        {
            _dbContext = dbContext;
            _parameterManager = parameterManager;
        }

        public async Task<List<Permissions>> GetPermissionsByRoleId(int id)
        {
            List<Permissions> permissions;
            List<Permissions> otherPermissions;
            int total = 0;
            using (var dbConnection = _dbContext.GetDbConnection())
            {

                var result = await dbConnection.QueryMultipleAsync(
            "usp_GetPermissionsByRole", _dbContext.GetDapperDynamicParameters
            (_parameterManager.Get("RoleId", id)
            ),
            commandType: CommandType.StoredProcedure);
                permissions = result.Read<Permissions>().ToList();
                dbConnection.Close();
            }
            return permissions;

        }


        public async Task<(List<GetStreetAndAreaMappedResponseDto>, int)> GetStreetAndAreaMapped(string filterModel, ServerRowsRequest commonRequest, string getSort)
        {
            List<GetStreetAndAreaMappedResponseDto> contact;
            int total = 0;
            using (var dbConnection = _dbContext.GetDbConnection())
            {

                var result = await dbConnection.QueryMultipleAsync(
            "usp_GetStreetAreaMapping", _dbContext.GetDapperDynamicParameters
            (_parameterManager.Get("@StartRow", commonRequest.StartRow),
              _parameterManager.Get("@EndRow", commonRequest.EndRow),
              _parameterManager.Get("@FilterModel", filterModel),
              _parameterManager.Get("@OrderBy", getSort),
              _parameterManager.Get("@SearchText", commonRequest.SearchText)
            ),
            commandType: CommandType.StoredProcedure);
                total = result.Read<int>().FirstOrDefault();
                contact = result.Read<GetStreetAndAreaMappedResponseDto>().ToList();
                dbConnection.Close();
            }
            return (contact, total);
        }

        public async Task<(List<GetAreasResponseDto>, int)> GetAreas(string filterModel, ServerRowsRequest commonRequest, string getSort)
        {
            List<GetAreasResponseDto> contact;
            int total = 0;
            using (var dbConnection = _dbContext.GetDbConnection())
            {

                var result = await dbConnection.QueryMultipleAsync(
            "usp_GetAreas", _dbContext.GetDapperDynamicParameters
            (_parameterManager.Get("@StartRow", commonRequest.StartRow),
              _parameterManager.Get("@EndRow", commonRequest.EndRow),
              _parameterManager.Get("@FilterModel", filterModel),
              _parameterManager.Get("@OrderBy", getSort),
              _parameterManager.Get("@SearchText", commonRequest.SearchText)
            ),
            commandType: CommandType.StoredProcedure);
                total = result.Read<int>().FirstOrDefault();
                contact = result.Read<GetAreasResponseDto>().ToList();
                dbConnection.Close();
            }
            return (contact, total);
        }
        public async Task<IList<GetAllRolesResponseDto>> GetRoles()
        {
            return await _dbContext.ExecuteStoredProcedureList<GetAllRolesResponseDto>("usp_GetRoles");
        }
        public async Task<IList<GetPredefinedSMSMessagesResponseDto>> GetPredefinedSMSMessages()
        {
            return await _dbContext.ExecuteStoredProcedureList<GetPredefinedSMSMessagesResponseDto>("usp_GetPredefinedMessages");
        }

        public async Task<int> UpdatePermissionByRoleId(int permissionId, int roleId, string permissionType, bool canView, bool canEdit)
        {

            return await _dbContext.ExecuteStoredProcedure<int>("usp_UpdatePermissionByRoleId",
                            _parameterManager.Get("PermissionId", permissionId, ParameterDirection.Input, DbType.Int32),
                            _parameterManager.Get("RoleId", roleId, ParameterDirection.Input, DbType.Int32),
                            _parameterManager.Get("PermissionType", permissionType, ParameterDirection.Input, DbType.String),
                            _parameterManager.Get("CanView", canView, ParameterDirection.Input, DbType.Boolean),
                            _parameterManager.Get("CanEdit", canEdit, ParameterDirection.Input, DbType.Boolean));


        }

        public async Task<(List<GetSchoolYearsResponseDto>, int)> GetSchoolYears(string filterModel, ServerRowsRequest commonRequest, string getSort)
        {
            List<GetSchoolYearsResponseDto> contact;
            int total = 0;
            using (var dbConnection = _dbContext.GetDbConnection())
            {
                var result = await dbConnection.QueryMultipleAsync(
               "usp_GetSchoolYears", _dbContext.GetDapperDynamicParameters
               (_parameterManager.Get("@StartRow", commonRequest.StartRow),
                 _parameterManager.Get("@EndRow", commonRequest.EndRow),
                 _parameterManager.Get("@FilterModel", filterModel),
                 _parameterManager.Get("@OrderBy", getSort),
                 _parameterManager.Get("@SearchText", commonRequest.SearchText)
               ),
               commandType: CommandType.StoredProcedure);
                total = result.Read<int>().FirstOrDefault();
                contact = result.Read<GetSchoolYearsResponseDto>().ToList();
                dbConnection.Close();
            }
            return (contact, total);
        }

        public async Task<(List<GetGenderResponseDto>, int)> GetGender(string filterModel, ServerRowsRequest commonRequest, string getSort)
        {
            List<GetGenderResponseDto> contact;
            int total = 0;
            using (var dbConnection = _dbContext.GetDbConnection())
            {
                var result = await dbConnection.QueryMultipleAsync(
                "usp_GetGender", _dbContext.GetDapperDynamicParameters
                (_parameterManager.Get("@StartRow", commonRequest.StartRow),
                  _parameterManager.Get("@EndRow", commonRequest.EndRow),
                  _parameterManager.Get("@FilterModel", filterModel),
                  _parameterManager.Get("@OrderBy", getSort),
                  _parameterManager.Get("@SearchText", commonRequest.SearchText)
                ),
                commandType: CommandType.StoredProcedure);
                total = result.Read<int>().FirstOrDefault();
                contact = result.Read<GetGenderResponseDto>().ToList();
                dbConnection.Close();
            }
            return (contact, total);
        }

        public async Task<(List<GetDistrictResponseDto>, int)> GetDistrict(string filterModel, ServerRowsRequest commonRequest, string getSort)
        {
            List<GetDistrictResponseDto> contact;
            int total = 0;
            using (var dbConnection = _dbContext.GetDbConnection())
            {
                var result = await dbConnection.QueryMultipleAsync(
               "usp_GetDistrict", _dbContext.GetDapperDynamicParameters
               (_parameterManager.Get("@StartRow", commonRequest.StartRow),
                 _parameterManager.Get("@EndRow", commonRequest.EndRow),
                 _parameterManager.Get("@FilterModel", filterModel),
                 _parameterManager.Get("@OrderBy", getSort),
                 _parameterManager.Get("@SearchText", commonRequest.SearchText)
               ),
               commandType: CommandType.StoredProcedure);
                total = result.Read<int>().FirstOrDefault();
                contact = result.Read<GetDistrictResponseDto>().ToList();
                dbConnection.Close();
            }
            return (contact, total);
        }

        public async Task<(List<GetSchoolsResponseDto>, int)> GetSchools(string filterModel, ServerRowsRequest commonRequest, string getSort)
        {
            List<GetSchoolsResponseDto> contact;
            int total = 0;
            using (var dbConnection = _dbContext.GetDbConnection())
            {

                var result = await dbConnection.QueryMultipleAsync(
            "usp_GetSchools", _dbContext.GetDapperDynamicParameters
            (_parameterManager.Get("@StartRow", commonRequest.StartRow),
              _parameterManager.Get("@EndRow", commonRequest.EndRow),
              _parameterManager.Get("@FilterModel", filterModel),
              _parameterManager.Get("@OrderBy", getSort),
              _parameterManager.Get("@SearchText", commonRequest.SearchText)
            ),
            commandType: CommandType.StoredProcedure);
                total = result.Read<int>().FirstOrDefault();
                contact = result.Read<GetSchoolsResponseDto>().ToList();
                dbConnection.Close();
            }
            return (contact, total);
        }

        public async Task<int> AddRole(string roleName)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_InsertRoleWithPermissions",
                       _parameterManager.Get("RoleName", roleName, ParameterDirection.Input, DbType.String));
        }

        public async Task<(List<GetBuildingResponseDto>, int)> GetBuilding(string filterModel, ServerRowsRequest commonRequest, string getSort)
        {
            List<GetBuildingResponseDto> contact;
            int total = 0;
            using (var dbConnection = _dbContext.GetDbConnection())
            {
                var result = await dbConnection.QueryMultipleAsync(
            "usp_GetBuilding", _dbContext.GetDapperDynamicParameters
            (_parameterManager.Get("@StartRow", commonRequest.StartRow),
              _parameterManager.Get("@EndRow", commonRequest.EndRow),
              _parameterManager.Get("@FilterModel", filterModel),
              _parameterManager.Get("@OrderBy", getSort),
              _parameterManager.Get("@SearchText", commonRequest.SearchText)
            ),
            commandType: CommandType.StoredProcedure);
                total = result.Read<int>().FirstOrDefault();
                contact = result.Read<GetBuildingResponseDto>().ToList();
                dbConnection.Close();
            }
            return (contact, total);
        }

        public async Task<(List<GetCcResponseDto>, int)> GetCc(string filterModel, ServerRowsRequest commonRequest, string getSort)
        {
            List<GetCcResponseDto> contact;
            int total = 0;
            using (var dbConnection = _dbContext.GetDbConnection())
            {
                var result = await dbConnection.QueryMultipleAsync(
            "usp_GetCc", _dbContext.GetDapperDynamicParameters
            (_parameterManager.Get("@StartRow", commonRequest.StartRow),
              _parameterManager.Get("@EndRow", commonRequest.EndRow),
              _parameterManager.Get("@FilterModel", filterModel),
              _parameterManager.Get("@OrderBy", getSort),
              _parameterManager.Get("@SearchText", commonRequest.SearchText)
            ),
            commandType: CommandType.StoredProcedure);
                total = result.Read<int>().FirstOrDefault();
                contact = result.Read<GetCcResponseDto>().ToList();
                dbConnection.Close();
            }
            return (contact, total);
        }

        public async Task<(List<GetChargeStructureResponseDto>, int)> GetChargeStructure(string filterModel, ServerRowsRequest commonRequest, string getSort)
        {
            List<GetChargeStructureResponseDto> contact;
            int total = 0;
            using (var dbConnection = _dbContext.GetDbConnection())
            {
                var result = await dbConnection.QueryMultipleAsync(
                "usp_GetChargeStructure", _dbContext.GetDapperDynamicParameters
                (_parameterManager.Get("@StartRow", commonRequest.StartRow),
                  _parameterManager.Get("@EndRow", commonRequest.EndRow),
                  _parameterManager.Get("@FilterModel", filterModel),
                  _parameterManager.Get("@OrderBy", getSort),
                  _parameterManager.Get("@SearchText", commonRequest.SearchText)
                ),
                commandType: CommandType.StoredProcedure);
                total = result.Read<int>().FirstOrDefault();
                contact = result.Read<GetChargeStructureResponseDto>().ToList();
                dbConnection.Close();
            }
            return (contact, total);
        }

        public async Task<(List<GetNTResponseDto>, int)> GetNT(string filterModel, ServerRowsRequest commonRequest, string getSort)
        {
            List<GetNTResponseDto> contact;
            int total = 0;
            using (var dbConnection = _dbContext.GetDbConnection())
            {
                var result = await dbConnection.QueryMultipleAsync(
                "usp_GetNT", _dbContext.GetDapperDynamicParameters
                (_parameterManager.Get("@StartRow", commonRequest.StartRow),
                  _parameterManager.Get("@EndRow", commonRequest.EndRow),
                  _parameterManager.Get("@FilterModel", filterModel),
                  _parameterManager.Get("@OrderBy", getSort),
                  _parameterManager.Get("@SearchText", commonRequest.SearchText)
                ),
                commandType: CommandType.StoredProcedure);
                total = result.Read<int>().FirstOrDefault();
                contact = result.Read<GetNTResponseDto>().ToList();
                dbConnection.Close();
            }
            return (contact, total);
        }

        public async Task<(List<GetBusTypeResponseDto>, int)> GetBusType(string filterModel, ServerRowsRequest commonRequest, string getSort)
        {
            List<GetBusTypeResponseDto> contact;
            int total = 0;
            using (var dbConnection = _dbContext.GetDbConnection())
            {
                var result = await dbConnection.QueryMultipleAsync(
                "usp_GetRouteType", _dbContext.GetDapperDynamicParameters
                (_parameterManager.Get("@StartRow", commonRequest.StartRow),
                  _parameterManager.Get("@EndRow", commonRequest.EndRow),
                  _parameterManager.Get("@FilterModel", filterModel),
                  _parameterManager.Get("@OrderBy", getSort),
                  _parameterManager.Get("@SearchText", commonRequest.SearchText)
                ),
                commandType: CommandType.StoredProcedure);
                total = result.Read<int>().FirstOrDefault();
                contact = result.Read<GetBusTypeResponseDto>().ToList();
                dbConnection.Close();
            }
            return (contact, total);
        }

        public async Task<(List<GetDriverTypeResponseDto>, int)> GetDriverType(string filterModel, ServerRowsRequest commonRequest, string getSort)
        {
            List<GetDriverTypeResponseDto> contact;
            int total = 0;
            using (var dbConnection = _dbContext.GetDbConnection())
            {
                var result = await dbConnection.QueryMultipleAsync(
                "usp_GetDriverType", _dbContext.GetDapperDynamicParameters
                (_parameterManager.Get("@StartRow", commonRequest.StartRow),
                  _parameterManager.Get("@EndRow", commonRequest.EndRow),
                  _parameterManager.Get("@FilterModel", filterModel),
                  _parameterManager.Get("@OrderBy", getSort),
                  _parameterManager.Get("@SearchText", commonRequest.SearchText)
                ),
                commandType: CommandType.StoredProcedure);
                total = result.Read<int>().FirstOrDefault();
                contact = result.Read<GetDriverTypeResponseDto>().ToList();
                dbConnection.Close();
            }
            return (contact, total);
        }

        public async Task<(List<GetDecductionAmountResponseDto>, int)> GetDecductionAmount(string filterModel, ServerRowsRequest commonRequest, string getSort)
        {
            List<GetDecductionAmountResponseDto> contact;
            int total = 0;
            using (var dbConnection = _dbContext.GetDbConnection())
            {
                var result = await dbConnection.QueryMultipleAsync(
                "usp_GetDecductionAmount", _dbContext.GetDapperDynamicParameters
                (_parameterManager.Get("@StartRow", commonRequest.StartRow),
                  _parameterManager.Get("@EndRow", commonRequest.EndRow),
                  _parameterManager.Get("@FilterModel", filterModel),
                  _parameterManager.Get("@OrderBy", getSort),
                  _parameterManager.Get("@SearchText", commonRequest.SearchText)
                ),
                commandType: CommandType.StoredProcedure);
                total = result.Read<int>().FirstOrDefault();
                contact = result.Read<GetDecductionAmountResponseDto>().ToList();
                dbConnection.Close();
            }
            return (contact, total);
        }

        public async Task<(List<GetSearchLocationResponseDto>, int)> GetSearchLocation(string filterModel, ServerRowsRequest commonRequest, string getSort)
        {
            List<GetSearchLocationResponseDto> contact;
            int total = 0;
            using (var dbConnection = _dbContext.GetDbConnection())
            {
                var result = await dbConnection.QueryMultipleAsync(
                "usp_GetSearchLocation", _dbContext.GetDapperDynamicParameters
                (_parameterManager.Get("@StartRow", commonRequest.StartRow),
                  _parameterManager.Get("@EndRow", commonRequest.EndRow),
                  _parameterManager.Get("@FilterModel", filterModel),
                  _parameterManager.Get("@OrderBy", getSort),
                  _parameterManager.Get("@SearchText", commonRequest.SearchText)
                ),
                commandType: CommandType.StoredProcedure);
                total = result.Read<int>().FirstOrDefault();
                contact = result.Read<GetSearchLocationResponseDto>().ToList();
                dbConnection.Close();
            }
            return (contact, total);
        }

        public async Task<IList<GetSearchLocationResponseDto>> GetSearchLocation()
        {
            return await _dbContext.ExecuteStoredProcedureList<GetSearchLocationResponseDto>("usp_GetSearchCurrentLocation");
        }
        public async Task<(List<GetSeasonFolderResponseDto>, int)> GetSeasonFolder(string filterModel, ServerRowsRequest commonRequest, string getSort)
        {
            List<GetSeasonFolderResponseDto> contact;
            int total = 0;
            using (var dbConnection = _dbContext.GetDbConnection())
            {
                var result = await dbConnection.QueryMultipleAsync(
                "usp_GetSeasonFolder", _dbContext.GetDapperDynamicParameters
                (_parameterManager.Get("@StartRow", commonRequest.StartRow),
                  _parameterManager.Get("@EndRow", commonRequest.EndRow),
                  _parameterManager.Get("@FilterModel", filterModel),
                  _parameterManager.Get("@OrderBy", getSort),
                  _parameterManager.Get("@SearchText", commonRequest.SearchText)
                ),
                commandType: CommandType.StoredProcedure);
                total = result.Read<int>().FirstOrDefault();
                contact = result.Read<GetSeasonFolderResponseDto>().ToList();
                dbConnection.Close();
            }
            return (contact, total);
        }

        public async Task<int> AddStreetsAreaMapping(int id, string streetName, int areaId, int userId, int districtId)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_AddStreetAreaMapping",
                   _parameterManager.Get("Id", id, ParameterDirection.Input, DbType.Int32),
                   _parameterManager.Get("StreetName", streetName, ParameterDirection.Input, DbType.String),
                   _parameterManager.Get("AreaId", areaId, ParameterDirection.Input, DbType.Int64),
                   _parameterManager.Get("DistrictId", districtId, ParameterDirection.Input, DbType.Int64),
                   _parameterManager.Get("@UserId", userId, ParameterDirection.Input, DbType.Int64));
        }

        public async Task<int> AddAreas(int id, string areaName, int userId, string shortName)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_AddAreas",
                    _parameterManager.Get("@Id", id, ParameterDirection.Input, DbType.Int32),
                   _parameterManager.Get("@AreaName", areaName, ParameterDirection.Input, DbType.String),
                   _parameterManager.Get("@ShortName", shortName, ParameterDirection.Input, DbType.String),
                   _parameterManager.Get("@UserId", userId, ParameterDirection.Input, DbType.Int64)
                   );
        }

        public async Task<int> AddSchoolYears(int id, int schoolYear, int numberOfStudents, int userId)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_AddSchoolYears",
                    _parameterManager.Get("@Id", id, ParameterDirection.Input, DbType.Int32),
                   _parameterManager.Get("@schoolYear", schoolYear, ParameterDirection.Input, DbType.Int64),
                   _parameterManager.Get("@numberOfStudents", numberOfStudents, ParameterDirection.Input, DbType.Int64),
                   _parameterManager.Get("@UserId", userId, ParameterDirection.Input, DbType.Int64));
        }

        public async Task<int> AddGender(int id, string gender, int userId)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_AddGender",
                 _parameterManager.Get("@Id", id, ParameterDirection.Input, DbType.Int32),
        _parameterManager.Get("@Gender", gender, ParameterDirection.Input, DbType.String),
        _parameterManager.Get("@UserId", userId, ParameterDirection.Input, DbType.Int64));
        }

        public async Task<int> AddDistrict(int id, string districtName, int userId)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_AddDistrict",
                 _parameterManager.Get("@Id", id, ParameterDirection.Input, DbType.Int32),
        _parameterManager.Get("@DistrictName", districtName, ParameterDirection.Input, DbType.String),
        _parameterManager.Get("@UserId", userId, ParameterDirection.Input, DbType.Int64));
        }

        public async Task<int> AddSchool(int id, string schoolName, string legalName, int userId)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_AddSchool",
                _parameterManager.Get("@Id", id, ParameterDirection.Input, DbType.Int32),
            _parameterManager.Get("@SchoolName", schoolName, ParameterDirection.Input, DbType.String),
            _parameterManager.Get("@LegalName", legalName, ParameterDirection.Input, DbType.String),
            _parameterManager.Get("@UserId", userId, ParameterDirection.Input, DbType.Int64));
        }

        public async Task<int> AddBuilding(int id, string address, int schoolId, int userId, string buildingName)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_AddBuilding",
                _parameterManager.Get("@Id", id, ParameterDirection.Input, DbType.Int32),
            _parameterManager.Get("@Address", address, ParameterDirection.Input, DbType.String),
            _parameterManager.Get("@SchoolId", schoolId, ParameterDirection.Input, DbType.Int64),
            _parameterManager.Get("@UserId", userId, ParameterDirection.Input, DbType.Int64),
            _parameterManager.Get("@BuildingName", buildingName, ParameterDirection.Input, DbType.String));
        }

        public async Task<int> AddCC(int id, int cardnoxId, int familyId, int userId)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_AddCC",
                 _parameterManager.Get("@Id", id, ParameterDirection.Input, DbType.Int32),
             _parameterManager.Get("@CardnoxId", cardnoxId, ParameterDirection.Input, DbType.String),
             _parameterManager.Get("@FamilyId", familyId, ParameterDirection.Input, DbType.Int64),
             _parameterManager.Get("@UserId", userId, ParameterDirection.Input, DbType.Int64));
        }

       public async Task<int> AddChargeStructure(int id, int districtId, int? ntId, bool isFunded, int userId, decimal price, int? schoolId)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_addChargeStructure",
                 _parameterManager.Get("@Id", id, ParameterDirection.Input, DbType.Int32),
                 _parameterManager.Get("@DistrictId", districtId, ParameterDirection.Input, DbType.Int64),
                 _parameterManager.Get("@NtId", ntId, ParameterDirection.Input, DbType.Int64),
                 _parameterManager.Get("@IsFunded", isFunded, ParameterDirection.Input, DbType.Boolean),
                 _parameterManager.Get("@UserId", userId, ParameterDirection.Input, DbType.Int64),
                 _parameterManager.Get("@Price", price, ParameterDirection.Input, DbType.Decimal),
                  _parameterManager.Get("@SchoolId", schoolId, ParameterDirection.Input, DbType.Int32));
        }

        public async Task<int> AddNT(int id, string nTName, int userId)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_AddNT",
             _parameterManager.Get("@Id", id, ParameterDirection.Input, DbType.Int32),
             _parameterManager.Get("@NTName", nTName, ParameterDirection.Input, DbType.String),
             _parameterManager.Get("@UserId", userId, ParameterDirection.Input, DbType.Int64));
        }

        public async Task<int> AddBusType(AddBusTypeRequestDto addBusTypeRequestDto)
        {
            var jsonString = JsonConvert.SerializeObject(addBusTypeRequestDto.RulesPayload.Rules);
            return await _dbContext.ExecuteStoredProcedure<int>("usp_AddRouteType",
             _parameterManager.Get("@Id", addBusTypeRequestDto.Id, ParameterDirection.Input, DbType.Int32),
             _parameterManager.Get("@RouteType", addBusTypeRequestDto.BusType, ParameterDirection.Input, DbType.String),
             _parameterManager.Get("@UserId", addBusTypeRequestDto.UserId, ParameterDirection.Input, DbType.Int64),
             _parameterManager.Get("@IsRequired", addBusTypeRequestDto.IsRequired, ParameterDirection.Input, DbType.Boolean),
             _parameterManager.Get("@ExclusivelyPay", addBusTypeRequestDto.ExclusivelyPay, ParameterDirection.Input, DbType.Boolean),
             _parameterManager.Get("@Amount", addBusTypeRequestDto.Amount, ParameterDirection.Input, DbType.Int64),
             _parameterManager.Get("@CreatedBy", addBusTypeRequestDto.RulesPayload.CreatedBy, ParameterDirection.Input, DbType.Int64),
             _parameterManager.Get("@RulesJson", jsonString),
               _parameterManager.Get("@Days", addBusTypeRequestDto.Days, ParameterDirection.Input, DbType.String)
             );
        }
        public async Task<int> UpdateFamilyTracking(UpdateFamilyTrackingRequestDto updateFamilyTrackingRequestDto)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_UpdateFamilyTracking",
             _parameterManager.Get("@Id", updateFamilyTrackingRequestDto.Id, ParameterDirection.Input, DbType.Int64),
             _parameterManager.Get("@UserId", updateFamilyTrackingRequestDto.UserId, ParameterDirection.Input, DbType.Int64),
             _parameterManager.Get("@IsTracking", updateFamilyTrackingRequestDto.IsTracking, ParameterDirection.Input, DbType.Boolean)
             );
        }
        public async Task<int> UpdateProfile(UpdateProfileRequestDto updateProfile)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_UpdateProfile",
                 _parameterManager.Get("@UserId", updateProfile.UserId, ParameterDirection.Input, DbType.Int32),
             _parameterManager.Get("@FirstName", updateProfile.FirstName, ParameterDirection.Input, DbType.String),
             _parameterManager.Get("@LastName", updateProfile.LastName, ParameterDirection.Input, DbType.String),
             _parameterManager.Get("@UserName", updateProfile.UserName, ParameterDirection.Input, DbType.String),
             _parameterManager.Get("@Email", updateProfile.Email, ParameterDirection.Input, DbType.String),
             _parameterManager.Get("@PasswordHash", updateProfile.PasswordHash, ParameterDirection.Input, DbType.Binary),
             _parameterManager.Get("@PasswordSalt", updateProfile.PasswordSalt, ParameterDirection.Input, DbType.Binary),
             _parameterManager.Get("@PhoneNumber", updateProfile.PhoneNumber, ParameterDirection.Input, DbType.String)
             );
        }

        public async Task<int> AddDriverType(int id, string driverType, decimal payRate, int userId)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_AddDriverType",
                 _parameterManager.Get("@Id", id, ParameterDirection.Input, DbType.Int32),
             _parameterManager.Get("@DriverType", driverType, ParameterDirection.Input, DbType.String),
              _parameterManager.Get("@PayRate", payRate, ParameterDirection.Input, DbType.String),
             _parameterManager.Get("@UserId", userId, ParameterDirection.Input, DbType.Int64));
        }

        public async Task<int> AddDeductionAmount(int id, decimal amount, int userId)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_AddDeductionAmount",
                 _parameterManager.Get("@Id", id, ParameterDirection.Input, DbType.Int32),
                 _parameterManager.Get("@Amount", amount, ParameterDirection.Input, DbType.String),
                 _parameterManager.Get("@UserId", userId, ParameterDirection.Input, DbType.Int64));
        }
        public async Task<int> AddSearchLocation(int id, string currentLocation, string currentLocationLongLat, int userId)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_AddSearchLocation",
                 _parameterManager.Get("@Id", id, ParameterDirection.Input, DbType.Int32),
                 _parameterManager.Get("@CurrentLocation", currentLocation, ParameterDirection.Input, DbType.String),
                 _parameterManager.Get("@CurrentLocationLongLat", currentLocationLongLat, ParameterDirection.Input, DbType.String),
                 _parameterManager.Get("@UserId", userId, ParameterDirection.Input, DbType.Int64));
        }

        public async Task<int> AddUpdateBranch(AddUpdateBranchRequestDto addUpdateBranchRequestDto)
        {
            var gradeIds = string.Join(",", addUpdateBranchRequestDto.Grade);

            return await _dbContext.ExecuteStoredProcedure<int>("usp_AddUpdateBranch",
                 _parameterManager.Get("@Id", addUpdateBranchRequestDto.Id, ParameterDirection.Input, DbType.Int32),
             _parameterManager.Get("@BranchName", addUpdateBranchRequestDto.BranchName, ParameterDirection.Input, DbType.String),
             _parameterManager.Get("@PrincipalName", addUpdateBranchRequestDto.PrincipalName, ParameterDirection.Input, DbType.String),
             _parameterManager.Get("@PrincipalCell", addUpdateBranchRequestDto.PrincipalCell, ParameterDirection.Input, DbType.String),
             _parameterManager.Get("@UserId", addUpdateBranchRequestDto.UserId, ParameterDirection.Input, DbType.Int64),
             _parameterManager.Get("@BuildingId", addUpdateBranchRequestDto.BuildingSys, ParameterDirection.Input, DbType.Int64),
             _parameterManager.Get("@GradeIds", gradeIds, ParameterDirection.Input, DbType.String),
             _parameterManager.Get("@GenderId", addUpdateBranchRequestDto.Gender, ParameterDirection.Input, DbType.Int64)
             );
        }

        public async Task<int> UpdateChargeStructure(int id, bool isFunded)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_UpdateChargeStructure",
                   _parameterManager.Get("Id", id, ParameterDirection.Input, DbType.Int32),
                   _parameterManager.Get("IsFunded", isFunded, ParameterDirection.Input, DbType.Boolean));
        }

        public async Task<int> AddUpdateGrade(AddUpdateGradeRequestDto addUpdateGradeRequestDto)
        {

            return await _dbContext.ExecuteStoredProcedure<int>("usp_AddUpdateGrade",
                 _parameterManager.Get("@Id", addUpdateGradeRequestDto.Id, ParameterDirection.Input, DbType.Int32),
             _parameterManager.Get("@GradeName", addUpdateGradeRequestDto.GradeName, ParameterDirection.Input, DbType.String),
             _parameterManager.Get("@UserId", addUpdateGradeRequestDto.UserId, ParameterDirection.Input, DbType.Int64),
             _parameterManager.Get("@GenderId", addUpdateGradeRequestDto.Gender, ParameterDirection.Input, DbType.Int64),
             _parameterManager.Get("@SchoolId", JsonConvert.SerializeObject(addUpdateGradeRequestDto.SchoolId))
             );
        }
        public async Task<int> AddSeasonFolder(AddSeasonFolderRequestDto addSeasonFolderRequestDto)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_AddSeasonFolder",
                _parameterManager.Get("@Id", addSeasonFolderRequestDto.Id, ParameterDirection.Input, DbType.Int32),
            _parameterManager.Get("@SeasonName", addSeasonFolderRequestDto.SeasonName, ParameterDirection.Input, DbType.String),
            _parameterManager.Get("@UserId", addSeasonFolderRequestDto.UserId, ParameterDirection.Input, DbType.Int64),
            _parameterManager.Get("@IsDefault", addSeasonFolderRequestDto.IsDefault, ParameterDirection.Input, DbType.Boolean)
            );
        }

        public async Task<IList<GetAreaListResponseDto>> GetAreaList()
        {
            return await _dbContext.ExecuteStoredProcedureList<GetAreaListResponseDto>("usp_GetAreaList");
        }

        public async Task<IList<GetAllDistrictsResponseDto>> GetAllDistricts()
        {
            return await _dbContext.ExecuteStoredProcedureList<GetAllDistrictsResponseDto>("usp_GetAllDistrict");
        }

        public async Task<IList<GetAllNTResponseDto>> GetAllNT()
        {
            return await _dbContext.ExecuteStoredProcedureList<GetAllNTResponseDto>("usp_GetAllNT");
        }

        public async Task<IList<GetAllSchoolsResponseDto>> GetAllSchools()
        {
            return await _dbContext.ExecuteStoredProcedureList<GetAllSchoolsResponseDto>("usp_GetAllSchools");
        }

        public async Task<int> DeleteStreetAreaMapping(int id)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_DeleteStreetsAreasMapping",
                      _parameterManager.Get("Id", id, ParameterDirection.Input, DbType.Int32));
        }

        public async Task<int> DeleteArea(int id)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_DeleteAreas",
                      _parameterManager.Get("Id", id, ParameterDirection.Input, DbType.Int32));
        }

        public async Task<int> DeleteSchoolYears(int id)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_DeleteSchoolYears",
                      _parameterManager.Get("Id", id, ParameterDirection.Input, DbType.Int32));
        }

        public async Task<int> DeleteGender(int id)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_DeleteGender",
                     _parameterManager.Get("Id", id, ParameterDirection.Input, DbType.Int32));
        }

        public async Task<int> DeleteDistrict(int id)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_DeleteDistrict",
                     _parameterManager.Get("Id", id, ParameterDirection.Input, DbType.Int32));
        }

        public async Task<int> DeleteSchools(int id)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_DeleteSchools",
                     _parameterManager.Get("Id", id, ParameterDirection.Input, DbType.Int32));
        }

        public async Task<int> DeleteBuilding(int id)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_DeleteBuilding",
                     _parameterManager.Get("Id", id, ParameterDirection.Input, DbType.Int32));
        }

        public async Task<int> DeleteCC(int id)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_DeleteCC",
                     _parameterManager.Get("Id", id, ParameterDirection.Input, DbType.Int32));
        }

        public async Task<int> DeleteChargeStructure(int id)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_DeleteChargeStructure",
                     _parameterManager.Get("Id", id, ParameterDirection.Input, DbType.Int32));
        }

        public async Task<int> DeleteNt(int id)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_DeleteNt",
                     _parameterManager.Get("Id", id, ParameterDirection.Input, DbType.Int32));
        }

        public async Task<int> DeleteBusType(int id)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_DeleteRouteType",
                     _parameterManager.Get("Id", id, ParameterDirection.Input, DbType.Int32));
        }
        public async Task<int> DeleteDriverType(int id)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_DeleteDriverType",
                     _parameterManager.Get("Id", id, ParameterDirection.Input, DbType.Int32));
        }
        public async Task<int> DeleteBranch(int id)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_DeleteBranch",
                     _parameterManager.Get("Id", id, ParameterDirection.Input, DbType.Int32));
        }
        public async Task<int> DeleteGrade(DeleteGradeRequestDto deleteGradeRequestDto)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_DeleteGrade",
                      _parameterManager.Get("Id", deleteGradeRequestDto.Id, ParameterDirection.Input, DbType.Int32));
        }
        public async Task<IList<GetAllGendersResponseDto>> GetAllGenders()
        {
            return await _dbContext.ExecuteStoredProcedureList<GetAllGendersResponseDto>("usp_GetAllGenders");
        }

        public async Task<IList<GetBuildingListResponseDto>> GetBuildingList()
        {
            return await _dbContext.ExecuteStoredProcedureList<GetBuildingListResponseDto>("usp_GetBuildingList");
        }
        public async Task<IList<GetSeasonFolderResponseDto>> GetAllSeasonFolder()
        {
            return await _dbContext.ExecuteStoredProcedureList<GetSeasonFolderResponseDto>("usp_GetAllSeasonFolder");
        }
        public async Task<IList<GetStatesResponseDto>> GetStates(int id)
        {
            return await _dbContext.ExecuteStoredProcedureList<GetStatesResponseDto>("usp_GetState",
                     _parameterManager.Get("@CountryId", id, ParameterDirection.Input, DbType.Int32));
        }

        public async Task<IList<GetCitiesResponseDto>> GetCities(int id)
        {
            return await _dbContext.ExecuteStoredProcedureList<GetCitiesResponseDto>("usp_GetCitiesByStateId",
              _parameterManager.Get("@StateId", id, ParameterDirection.Input, DbType.Int32));
        }

        public async Task<IList<GetRouteTypeRequiredRulesResponseDto>> GetRouteTypeRequiredRules(int? routeTypeId)
        {
            return await _dbContext.ExecuteStoredProcedureList<GetRouteTypeRequiredRulesResponseDto>("usp_GetRouteTypeRequiredRules",
              _parameterManager.Get("@RouteTypeId", routeTypeId, ParameterDirection.Input, DbType.Int32));
        }

        public async Task<IList<GetAllBusTypeResponseDto>> GetAllBusType()
        {
            return await _dbContext.ExecuteStoredProcedureList<GetAllBusTypeResponseDto>("usp_GetAllRouteType");
        }
        public async Task<int> AddLog(string message, string from, string messageType)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_AddLogs",
                      _parameterManager.Get("Message", message, ParameterDirection.Input, DbType.String),
                      _parameterManager.Get("From", from, ParameterDirection.Input, DbType.String),
                      _parameterManager.Get("MessageType", messageType, ParameterDirection.Input, DbType.String));
        }

        public async Task<(List<GetBranchResponseDto>, int)> GetBranch(string filterModel, ServerRowsRequest commonRequest, string getSort)
        {
            List<GetBranchResponseDto> branch;
            int total = 0;
            using (var dbConnection = _dbContext.GetDbConnection())
            {
                var result = await dbConnection.QueryMultipleAsync(
                "usp_GetBranch", _dbContext.GetDapperDynamicParameters
                (_parameterManager.Get("@StartRow", commonRequest.StartRow),
                  _parameterManager.Get("@EndRow", commonRequest.EndRow),
                  _parameterManager.Get("@FilterModel", filterModel),
                  _parameterManager.Get("@OrderBy", getSort),
                  _parameterManager.Get("@SearchText", commonRequest.SearchText)
                ),
                commandType: CommandType.StoredProcedure);
                total = result.Read<int>().FirstOrDefault();
                branch = result.Read<GetBranchResponseDto>().ToList();
                dbConnection.Close();
            }
            return (branch, total);
        }

        public async Task<(List<GetAllGradeResponseDto>, int)> GetAllGrade(string filterModel, ServerRowsRequest commonRequest, string getSort)
        {
            List<GetAllGradeResponseDto> grade;
            int total = 0;
            using (var dbConnection = _dbContext.GetDbConnection())
            {
                var result = await dbConnection.QueryMultipleAsync(
                "usp_GetAllGrade", _dbContext.GetDapperDynamicParameters
                (_parameterManager.Get("@StartRow", commonRequest.StartRow),
                  _parameterManager.Get("@EndRow", commonRequest.EndRow),
                  _parameterManager.Get("@FilterModel", filterModel),
                  _parameterManager.Get("@OrderBy", getSort),
                  _parameterManager.Get("@SearchText", commonRequest.SearchText)
                ),
                commandType: CommandType.StoredProcedure);
                total = result.Read<int>().FirstOrDefault();
                grade = result.Read<GetAllGradeResponseDto>().ToList();
                dbConnection.Close();
            }
            return (grade, total);
        }

        public async Task<(List<GetAllFamilyDetailResponseDto>, int)> GetAllFamilyDetail(string filterModel, ServerRowsRequest commonRequest, string getSort)
        {
            List<GetAllFamilyDetailResponseDto> family;
            int total = 0;
            using (var dbConnection = _dbContext.GetDbConnection())
            {
                var result = await dbConnection.QueryMultipleAsync(
                "usp_GetAllFamilyDetail", _dbContext.GetDapperDynamicParameters
                (_parameterManager.Get("@StartRow", commonRequest.StartRow),
                  _parameterManager.Get("@EndRow", commonRequest.EndRow),
                  _parameterManager.Get("@FilterModel", filterModel),
                  _parameterManager.Get("@OrderBy", getSort),
                  _parameterManager.Get("@SearchText", commonRequest.SearchText)
                ),
                commandType: CommandType.StoredProcedure);
                total = result.Read<int>().FirstOrDefault();
                family = result.Read<GetAllFamilyDetailResponseDto>().ToList();
                dbConnection.Close();
            }
            return (family, total);
        }

        public async Task<string> UpdateFamilyDetail(UpdateFamilyDetailRequestDto updateFamilyDetailRequestDto)
        {
            return await _dbContext.ExecuteStoredProcedure<string>("usp_UpdateFamilyDetail",
            _parameterManager.Get("@Id", updateFamilyDetailRequestDto.Id),
            _parameterManager.Get("@LastName", updateFamilyDetailRequestDto.LastName),
            _parameterManager.Get("@FatherFirstName", updateFamilyDetailRequestDto.FatherFirstName),
            _parameterManager.Get("@MotherFirstName", updateFamilyDetailRequestDto.MotherFirstName),
            _parameterManager.Get("@Address", updateFamilyDetailRequestDto.Address),
            _parameterManager.Get("@HomeNumber", updateFamilyDetailRequestDto.HomeNumber),
            _parameterManager.Get("@FatherCell", updateFamilyDetailRequestDto.FatherCell),
            _parameterManager.Get("@MotherCell", updateFamilyDetailRequestDto.MotherCell),
            _parameterManager.Get("@State", updateFamilyDetailRequestDto.State),
            _parameterManager.Get("@City", updateFamilyDetailRequestDto.City),
            _parameterManager.Get("@Zipcode", updateFamilyDetailRequestDto.Zipcode),
            _parameterManager.Get("@Area", updateFamilyDetailRequestDto.Area),
            _parameterManager.Get("@District", updateFamilyDetailRequestDto.District),
            _parameterManager.Get("@UserId", updateFamilyDetailRequestDto.UserId)
            );
        }

        public async Task<IList<GetAreaListResponseDto>> GetAllAreas()
        {
            return await _dbContext.ExecuteStoredProcedureList<GetAreaListResponseDto>("usp_GetAllAreas");
        }

        public async Task<int> ImportAreasKml(string areasJson)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_ImportAreasKml",
                    _parameterManager.Get("@AreasJson", areasJson, ParameterDirection.Input, DbType.String)
                   );
        }

        public async Task<int> CheckStudentAreaRelation(int id)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_CheckStudentAreaRelation",
                       _parameterManager.Get("Id", id, ParameterDirection.Input, DbType.Int32));
        }

        public async Task<int> UpdateGradeMapping(string gradeJson)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_UpdateGradeMapping",
                    _parameterManager.Get("GradeJSON", gradeJson, ParameterDirection.Input, DbType.String)
               );
        }

        public async Task<string> UpdateBulkGrade(UpdateBulkGradeDto updateBulkGradeDto)
        {
            return await _dbContext.ExecuteStoredProcedure<string>("usp_UpdateBulkGrade",
            _parameterManager.Get("@GenderId", updateBulkGradeDto.GenderId),
            _parameterManager.Get("@SchoolId", updateBulkGradeDto.SchoolId)
            );
        }
        public async Task<int> CheckDistrictBeforeDelete(int districtId)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_CheckDistrictBeforeDelete",
                _parameterManager.Get("@DistrictId", districtId, ParameterDirection.Input, DbType.Int32));
        }

        public async Task<int> CheckGenderBeforeDelete(int genderId)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_CheckGenderBeforeDelete",
               _parameterManager.Get("@GenderId", genderId, ParameterDirection.Input, DbType.Int32));
        }

        public async Task<int> CheckSchoolBeforeDelete(int schoolId)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_CheckSchoolBeforeDelete",
           _parameterManager.Get("@SchoolId", schoolId, ParameterDirection.Input, DbType.Int32));
        }

        public async Task<int> CheckBuildingBeforeDelete(int buildingId)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_CheckBuildingBeforeDelete",
           _parameterManager.Get("@BuildingId", buildingId, ParameterDirection.Input, DbType.Int32));
        }

        public async Task<int> CheckBranchBeforeDelete(int branchId)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_CheckBranchBeforeDelete",
          _parameterManager.Get("@BranchId", branchId, ParameterDirection.Input, DbType.Int32));
        }

        public async Task<int> CheckDriverTypeBeforeDelete(int driverTypeId)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_CheckDriverTypeBeforeDelete",
          _parameterManager.Get("@DriverTypeId", driverTypeId, ParameterDirection.Input, DbType.Int32));
        }

        public async Task<int> CheckGradeBeforeDelete(DeleteGradeRequestDto deleteGradeRequestDto)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_CheckGradeBeforeDelete",
          _parameterManager.Get("@GradeId", deleteGradeRequestDto.Id, ParameterDirection.Input, DbType.Int32));
        }
        public async Task<int> CheckSeasonFolderBeforeDelete(DeleteSeasonFolderRequestDto deleteSeasonFolderRequestDto)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_CheckSeasonFolderBeforeDelete",
          _parameterManager.Get("@SeasonFolderId", deleteSeasonFolderRequestDto.Id, ParameterDirection.Input, DbType.Int32));
        }

        public async Task<int> CheckNTBeforeDelete(int nt)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_CheckNTBeforeDelete",
          _parameterManager.Get("@NTId", nt, ParameterDirection.Input, DbType.Int32));
        }

        public async Task<int> CheckRouteTypeBeforeDelete(int routeType)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_CheckRouteTypeBeforeDelete",
          _parameterManager.Get("@RouteTypeId", routeType, ParameterDirection.Input, DbType.Int32));
        }

        public async Task<int> DeleteSeasonFolder(DeleteSeasonFolderRequestDto deleteSeasonFolderRequestDto)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_DeleteSeasonFolder",
            _parameterManager.Get("Id", deleteSeasonFolderRequestDto.Id, ParameterDirection.Input, DbType.Int32));
        }

        public async Task<string> SetRouteTypeRequiredRules(RouteTypeRequiredRulesDto routeTypeRequiredRulesDto)
        {

            var jsonString = JsonConvert.SerializeObject(routeTypeRequiredRulesDto.Rules);

            using (var dbConnection = _dbContext.GetDbConnection())
            {
                var rowsAffected = await dbConnection.ExecuteAsync(
                    "usp_SaveRouteTypeRequiredRules",
                    _dbContext.GetDapperDynamicParameters(
                        _parameterManager.Get("@RulesJson", jsonString),
                        _parameterManager.Get("@RouteTypeId", routeTypeRequiredRulesDto.RouteTypeId),
                        _parameterManager.Get("@CreatedBy", routeTypeRequiredRulesDto.CreatedBy)
                    ),
                    commandType: CommandType.StoredProcedure
                );

                return $"Successfully saved {rowsAffected} rules.";
            }
        }

        public static DataTable ConvertToDataTable(List<RouteTypeRule> rules)
        {
            var table = new DataTable();
            table.Columns.Add("SchoolId", typeof(int));
            table.Columns.Add("GenderId", typeof(int));
            table.Columns.Add("GradeId", typeof(int));

            foreach (var r in rules)
            {
                table.Rows.Add(r.SchoolId, r.GenderId, r.GradeId);
            }

            return table;
        }

        public async Task<GetTrackingTimeQueryResponseDto> GetTrackingTime()
        {
            return await _dbContext.ExecuteStoredProcedure<GetTrackingTimeQueryResponseDto>("usp_GetTrackingTime");
        }

        public async Task<int> UpdateTrackingTime(UpdateTrackingTimeRequestDto updateTrackingTimeRequestDto)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_UpdateTrackingTime",
             _parameterManager.Get("@TrackingTimeId", updateTrackingTimeRequestDto.TrackingTimeId, ParameterDirection.Input, DbType.Int32),
             _parameterManager.Get("@TrackingStartTime", updateTrackingTimeRequestDto.TrackingStartTime, ParameterDirection.Input, DbType.Int32),
             _parameterManager.Get("@TrackingEndTime", updateTrackingTimeRequestDto.TrackingEndTime, ParameterDirection.Input, DbType.Int32));
        }

        public async Task<int> AddRunType(AddRunTypeRequestDto addRunTypeRequestDto)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_AddRunType",
            _parameterManager.Get("@Id", addRunTypeRequestDto.Id, ParameterDirection.Input, DbType.Int32),
            _parameterManager.Get("@RunType", addRunTypeRequestDto.RunType, ParameterDirection.Input, DbType.String));
        }

        public async Task<(List<GetRunTypeResponseDto>, int)> GetRunType(string filterModel, ServerRowsRequest commonRequest, string getSort)
        {
            List<GetRunTypeResponseDto> runType;
            int total = 0;
            using (var dbConnection = _dbContext.GetDbConnection())
            {
                var result = await dbConnection.QueryMultipleAsync(
                "usp_GetRunType", _dbContext.GetDapperDynamicParameters
                (_parameterManager.Get("@StartRow", commonRequest.StartRow),
                  _parameterManager.Get("@EndRow", commonRequest.EndRow),
                  _parameterManager.Get("@FilterModel", filterModel),
                  _parameterManager.Get("@OrderBy", getSort),
                  _parameterManager.Get("@SearchText", commonRequest.SearchText)
                ),
                commandType: CommandType.StoredProcedure);
                total = result.Read<int>().FirstOrDefault();
                runType = result.Read<GetRunTypeResponseDto>().ToList();
                dbConnection.Close();
            }
            return (runType, total);
        }

        public async Task<int> DeleteRunType(DeleteRunTypeRequestDto deleteRunTypeRequestDto)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_DeleteRouteType",
                   _parameterManager.Get("Id", deleteRunTypeRequestDto.Id, ParameterDirection.Input, DbType.Int32));
        }

        public async Task<IList<GetAllRunTypeResponseDto>> GetAllRunType()
        {
            return await _dbContext.ExecuteStoredProcedureList<GetAllRunTypeResponseDto>("usp_GetAllRunType");
        }
        public async Task<IList<ExportFamilyListDto>> ExportFamilyList()
        {
            List<ExportFamilyListDto> newApplications;

            using (var dbConnection = _dbContext.GetDbConnection())
            {
                var result = await dbConnection.QueryMultipleAsync(
                    "usp_ExportFamilyList",
                    commandType: CommandType.StoredProcedure
                );

                newApplications = result.Read<ExportFamilyListDto>().ToList();

                return (newApplications);
            }
        }
        public async Task<(List<GetMessageResponseDto>, int)> GetMessages(string filterModel, ServerRowsRequest commonRequest, string getSort)
        {
            List<GetMessageResponseDto> messages;
            int total = 0;

            using (var dbConnection = _dbContext.GetDbConnection())
            {
                var result = await dbConnection.QueryMultipleAsync(
                    "usp_GetMessages",
                    _dbContext.GetDapperDynamicParameters(
                        _parameterManager.Get("@StartRow", commonRequest.StartRow),
                        _parameterManager.Get("@EndRow", commonRequest.EndRow),
                        _parameterManager.Get("@FilterModel", filterModel),
                        _parameterManager.Get("@OrderBy", getSort),
                        _parameterManager.Get("@SearchText", commonRequest.SearchText)
                    ),
                    commandType: CommandType.StoredProcedure
                );

                total = result.Read<int>().FirstOrDefault();
                messages = result.Read<GetMessageResponseDto>().ToList();
            }

            return (messages, total);
        }

        public async Task AddMessage(AddMessageRequestDto request)
        {
            await _dbContext.ExecuteStoredProcedure<int>(
                "usp_AddMessage",
                _parameterManager.Get("Id", request.Id, ParameterDirection.Input, DbType.Int32),
                _parameterManager.Get("Title", request.Title, ParameterDirection.Input, DbType.String),
                _parameterManager.Get("Message", request.Message, ParameterDirection.Input, DbType.String),
                _parameterManager.Get("CreatedBy", request.CreatedBy, ParameterDirection.Input, DbType.Int32)
            );
        }
        public async Task<int> DeleteMessage(DeleteMessageRequestDto request)
        {
            return await _dbContext.ExecuteStoredProcedure<int>(
                "usp_DeleteMessage",
                _parameterManager.Get("@Id", request.Id, ParameterDirection.Input, DbType.Int32)
            );
        }

        public async Task<List<GetDaysResponseDto>> GetDay()
        {
            using (var dbConnection = _dbContext.GetDbConnection())
            {
                var result = await dbConnection.QueryAsync<GetDaysResponseDto>(
                    "usp_GetDaysList",
                    commandType: CommandType.StoredProcedure
                );

                return result.ToList();
            }
        }
        public async Task<(List<GetDaysResponseDto>, int)> GetDays(string filterModel, ServerRowsRequest commonRequest, string getSort)
        {
            List<GetDaysResponseDto> days;
            int total = 0;
            using (var dbConnection = _dbContext.GetDbConnection())
            {
                var result =
                await dbConnection.QueryMultipleAsync("usp_GetDays",
                _dbContext.GetDapperDynamicParameters(
                _parameterManager.Get("@StartRow", commonRequest.StartRow),
                _parameterManager.Get("@EndRow",
                commonRequest.EndRow),

                _parameterManager.Get("@FilterModel",
                filterModel),

                _parameterManager.Get("@OrderBy",
                getSort),

                _parameterManager.Get("@SearchText",
                commonRequest.SearchText)

                ),

                commandType: CommandType.StoredProcedure);

                total = result.Read<int>().FirstOrDefault();

                days = result.Read<GetDaysResponseDto>().ToList();

                dbConnection.Close();

            }

            return (days, total);

        }

        public async Task<int> UpdateDayStatus(int id, bool isActive)
        {
            using (var dbConnection = _dbContext.GetDbConnection())
            {
                var result = await dbConnection.ExecuteAsync(
                    "usp_UpdateDayStatus",
                    _dbContext.GetDapperDynamicParameters(
                        _parameterManager.Get("@Id", id),
                        _parameterManager.Get("@IsActive", isActive)
                    ),
                    commandType: CommandType.StoredProcedure
                );

                dbConnection.Close();

                return result;
            }
        }
        public async Task<IList<RouteTypeDayDto>> GetRouteTypeDays(int routeTypeId)
        {
            return await _dbContext.ExecuteStoredProcedureList<RouteTypeDayDto>(
                "usp_GetRouteTypeDays",
                _parameterManager.Get("@RouteTypeId", routeTypeId, ParameterDirection.Input, DbType.Int32)
            );
        }
        public async Task<int> UpdateRouteTypeExclusivePay(int id, bool exclusivelyPay)
        {
            return await _dbContext.ExecuteStoredProcedure<int>(
                "usp_UpdateRouteTypeExclusivePay",
                _parameterManager.Get("@Id", id, ParameterDirection.Input, DbType.Int32),
                _parameterManager.Get("@ExclusivelyPay", exclusivelyPay, ParameterDirection.Input, DbType.Boolean)
            );
        }
        public async Task<(List<PredefinedColorDto>, int)> GetPredefinedColors(string filterModel,ServerRowsRequest commonRequest,string getSort)
        {
            List<PredefinedColorDto> colors;
            int total = 0;

            using (var dbConnection = _dbContext.GetDbConnection())
            {
                var result = await dbConnection.QueryMultipleAsync(
                    "usp_GetPredefinedColors",
                    _dbContext.GetDapperDynamicParameters(
                        _parameterManager.Get("@StartRow", commonRequest.StartRow),
                        _parameterManager.Get("@EndRow", commonRequest.EndRow),
                        _parameterManager.Get("@FilterModel", filterModel),
                        _parameterManager.Get("@OrderBy", getSort),
                        _parameterManager.Get("@SearchText", commonRequest.SearchText)
                    ),
                    commandType: CommandType.StoredProcedure
                );

                total = result.Read<int>().FirstOrDefault();
                colors = result.Read<PredefinedColorDto>().ToList();

                dbConnection.Close();
            }

            return (colors, total);
        }
        public async Task<int> AddUpdatePredefinedColor(AddUpdatePredefinedColorDto dto)
        {
            return await _dbContext.ExecuteStoredProcedure<int>(
                "usp_AddUpdatePredefinedColor",
                _parameterManager.Get("Id", dto.Id, ParameterDirection.Input, DbType.Int32),
                _parameterManager.Get("ColorName", dto.ColorName, ParameterDirection.Input, DbType.String),
                _parameterManager.Get("ColorCode", dto.ColorCode, ParameterDirection.Input, DbType.String)
            );
        }
        public async Task<int> DeletePredefinedColor(int id)
        {
            return await _dbContext.ExecuteStoredProcedure<int>(
                "usp_DeletePredefinedColor",
                _parameterManager.Get("Id", id, ParameterDirection.Input, DbType.Int32)
            );
        }
        public async Task<List<PredefinedColorDto>> GetPredefinedColorsDropdown()
        {
            using (var dbConnection = _dbContext.GetDbConnection())
            {
                var result = await dbConnection.QueryAsync<PredefinedColorDto>(
                    "usp_GetPredefinedColorsDropdown",
                    commandType: CommandType.StoredProcedure
                );

                dbConnection.Close();
                return result.ToList();
            }
        }
        public async Task<int> UpdateBusCharge(UpdateBusChargeRequestDto updateBusChargeRequestDto)
        {
            return await _dbContext.ExecuteStoredProcedure<int>(
                "usp_UpdateBusCharge",
                _parameterManager.Get("@BusChargeId", updateBusChargeRequestDto.BusChargeId, ParameterDirection.Input, DbType.Int32),
                _parameterManager.Get("@Amount", updateBusChargeRequestDto.Amount, ParameterDirection.Input, DbType.Decimal)
            );
        }
        public async Task<GetBusChargeQueryResponseDto> GetBusCharge()
        {
            return await _dbContext.ExecuteStoredProcedure<GetBusChargeQueryResponseDto>(
                "usp_GetBusCharge"
            );
        }
    }
}

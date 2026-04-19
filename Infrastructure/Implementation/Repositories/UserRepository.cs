using Application.Abstraction.DataBase;
using Application.Abstraction.Repositories;
using Application.Common.Dtos;
using Dapper;
using Domain.Entities;
using DTO.Response.Driver;
using DTO.Response.User;
using System.Data;

namespace Infrastructure.Implementation.Repositories
{
    internal class UserRepository: IUserRepository
    {
        private readonly IDbContext _dbContext;
        private readonly IParameterManager _parameterManager;
        public UserRepository(IDbContext dbContext, IParameterManager parameterManager)
        {
            _dbContext = dbContext;
            _parameterManager = parameterManager;
        }

        public async Task<int> InsertUser(Users user, byte[]? passwordHash, byte[]? passwordSalt)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_InsertUser",
                _parameterManager.Get("FirstName", user.FirstName, ParameterDirection.Input, DbType.String),
                _parameterManager.Get("UserId", user.UserId, ParameterDirection.Input, DbType.Int32),
                _parameterManager.Get("LastName", user.LastName, ParameterDirection.Input, DbType.String),
                _parameterManager.Get("PasswordHash", passwordHash, ParameterDirection.Input, DbType.Binary),
                _parameterManager.Get("PasswordSalt", passwordSalt, ParameterDirection.Input, DbType.Binary),
                _parameterManager.Get("Email", user.Email, ParameterDirection.Input, DbType.String),
                 _parameterManager.Get("PhoneNumber", user.PhoneNumber, ParameterDirection.Input, DbType.String),
                _parameterManager.Get("UserName", user.UserName, ParameterDirection.Input, DbType.String),
                _parameterManager.Get("Status", user.Status, ParameterDirection.Input, DbType.Boolean),
                 _parameterManager.Get("isGoogleLogin", user.isGoogleLogin, ParameterDirection.Input, DbType.Boolean),
                _parameterManager.Get("RoleId", user.RoleId, ParameterDirection.Input, DbType.Int32),
                 _parameterManager.Get("Id", user.Id, ParameterDirection.Input, DbType.Int32)
                );
        }



        public async Task<Users> GetUserByUserId(int userId)
        {
           return await _dbContext.ExecuteStoredProcedure<Users>("usp_GetUserById",
               _parameterManager.Get("Id", userId)
           );
        }

        public async Task<Users> GetUserByEmail(string email)
        {
            return await _dbContext.ExecuteStoredProcedure<Users>("usp_GetUserByEmail", 
              _parameterManager.Get("Email", email));
        }

        public async Task LogoutUser(int id, bool isLoggedIn)
        {
            await _dbContext.ExecuteStoredProcedure("usp_UpdateLoginInfo", true,
           _parameterManager.Get("Id", id),
           _parameterManager.Get("IsLoggedIn", isLoggedIn));
        }

        public async Task<Users> GetUserById(int id)
        {
            return await _dbContext.ExecuteStoredProcedure<Users>("usp_GetUserById", true,
             _parameterManager.Get("Id", id));
        }

        public async Task<int> DeleteUser(int id)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_DeleteUser",
                       _parameterManager.Get("Id", id, ParameterDirection.Input, DbType.Int32));
        }

        public async Task<(List<GetUsersResponseDto>, int)> GetUsers(string filterModel, ServerRowsRequest commonRequest, string getSort)
        {
            List<GetUsersResponseDto> contact;
            int total = 0;
            using (var dbConnection = _dbContext.GetDbConnection())
            {
                var result = await dbConnection.QueryMultipleAsync(
               "usp_GetUsers", _dbContext.GetDapperDynamicParameters
               (_parameterManager.Get("@StartRow", commonRequest.StartRow),
                 _parameterManager.Get("@EndRow", commonRequest.EndRow),
                 _parameterManager.Get("@FilterModel", filterModel),
                 _parameterManager.Get("@OrderBy", getSort),
                 _parameterManager.Get("@SearchText", commonRequest.SearchText)
               ),
               commandType: CommandType.StoredProcedure);
                total = result.Read<int>().FirstOrDefault();
                contact = result.Read<GetUsersResponseDto>().ToList();
                dbConnection.Close();
            }
            return (contact, total);
        }

        public async Task<string> SendOtpOnEmail(Users users)
        {
            return await _dbContext.ExecuteStoredProcedure<string>("usp_SendUserOtpOnEmail",
              _parameterManager.Get("@Email", users.Email),
              _parameterManager.Get("@OtpCode", users.OtpCode)
           );
        }

        public async Task<Drivers> CheckUserOTP(CheckOTPRequestDto checkOTPRequestDto)
        {
            return await _dbContext.ExecuteStoredProcedure<Drivers>("usp_CheckUserOTP",
                _parameterManager.Get("@Email", checkOTPRequestDto.Email),
                _parameterManager.Get("@OTP", checkOTPRequestDto.Otp)
            );
        }

        public async Task<int> DeleteDriver(int id)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_DeleteDriver",
           _parameterManager.Get("Id", id, ParameterDirection.Input, DbType.Int32));
        }
    }
}

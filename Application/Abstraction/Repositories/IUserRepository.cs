using Application.Common.Dtos;
using Domain.Entities;
using DTO.Response.Driver;
using DTO.Response.User;

namespace Application.Abstraction.Repositories
{
    public interface IUserRepository
    {
        Task<int> InsertUser(Users user, byte[]? passwordHash, byte[]? passwordSalt);
        Task<Users> GetUserByUserId(int userId);
        Task<Users> GetUserByEmail(string email);
        Task LogoutUser(int id, bool isLoggedIn);
        Task<Users> GetUserById(int id);
        Task<int> DeleteUser(int id);
        Task<int> DeleteDriver(int id);
        Task<(List<GetUsersResponseDto>, int)> GetUsers(string filterModel, ServerRowsRequest commonRequest, string getSort);
        Task<string> SendOtpOnEmail(Users users);
        Task<Drivers> CheckUserOTP(CheckOTPRequestDto checkOTPRequestDto);


    }
}

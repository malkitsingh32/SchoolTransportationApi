using Application.Common.Dtos;
using Application.Common.Response;
using Application.Handler.User.Dtos;
using Domain.Entities;
using DTO.Response;
using DTO.Response.User;

namespace Application.Abstraction.Services
{
    public interface IUserService
    {
        Task<CommonResultResponseDto<string>> AddUser(Users users, string password);
        Task<Users> GetUserByUserId(int userId);
        Task<AuthenticateDto> GetUserByCredentials(string email, string password, bool? isGoogleLogin);
        Task<CommonResultResponseDto<string>> DeleteUser(int id, string role);
        Task<CommonResultResponseDto<PaginatedList<GetUsersResponseDto>>> GetUsers(string filterModel, ServerRowsRequest commonRequest, string getSort);
    }
}

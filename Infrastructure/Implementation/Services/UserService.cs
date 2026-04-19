using Application.Abstraction.Repositories;
using Application.Abstraction.Services;
using Application.Common.Dtos;
using Application.Common.Response;
using Application.Handler.User.Dtos;
using Application.Settings;
using Domain.Entities;
using DTO.Response;
using DTO.Response.User;
using Helper;
using Helper.Constant;
using Mapster;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace Infrastructure.Implementation.Services
{
    internal class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly AppSettings _appSettings;
        private readonly IDriversRepository _driversRepository;


        public UserService(IUserRepository userRepository, IOptions<AppSettings> appSettings, IDriversRepository driversRepository)
        {
            _userRepository = userRepository;
            _appSettings = appSettings.Value;
            _driversRepository = driversRepository;

        }
        public async Task<CommonResultResponseDto<string>> AddUser(Users users, string password)
        {
            if (users.Role == "Driver")
            {
                int exists = await _driversRepository.IsExistEmail(users);

                if (exists == 1)
                {
                    return CommonResultResponseDto<string>.Failure(new string[] { "Email Already exists" }, null);
                }
                else
                {
                    byte[] passwordHash = null;
                    byte[] passwordSalt = null;

                    if (!string.IsNullOrWhiteSpace(password))
                    {
                        CreatePasswordHash(password, out passwordHash, out passwordSalt);
                    }

                    var busDetailsId = await _driversRepository.UpdateDriversDetail(users, passwordHash, passwordSalt);
                    return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Updated }, null, busDetailsId);
                }
            }

            var userByEmail = await GetUserByEmail(users.Email);
            if (userByEmail == null || users.Id > 0)
            {
                if (!users.isGoogleLogin)
                {
                    byte[] passwordHash = null;
                    byte[] passwordSalt = null;
                    if (!string.IsNullOrWhiteSpace(password))
                    {
                        CreatePasswordHash(password, out passwordHash, out passwordSalt);
                    }
                    var userId = await _userRepository.InsertUser(users, passwordHash, passwordSalt);
                    if (userId > 0)
                    {
                        return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Created }, null, userId);
                    }
                    else
                    {
                        return CommonResultResponseDto<string>.Failure(new string[] { "Something went wrong" }, null);
                    }
                }
                else
                {
                    var userId = await _userRepository.InsertUser(users, null, null);
                    if (userId > 0)
                    {
                        var userInfo = await GetUserByEmail(users.Email);
                        var result = await CreateJWTToken(userInfo);
                        string resultString = JsonSerializer.Serialize(result);
                        return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Created }, resultString);
                    }
                    else
                    {
                        return CommonResultResponseDto<string>.Failure(new string[] { "Something went wrong" }, null);
                    }
                }
            }
            else
            {
                if (!users.isGoogleLogin)
                {
                    return CommonResultResponseDto<string>.Failure(new string[] { "User already exists" }, null);
                }
                else
                {
                    var result = await CreateJWTToken(userByEmail);
                    string resultString = JsonSerializer.Serialize(result);
                    return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Created }, resultString);
                }
            }
        }

        public async Task<Users> GetUserByUserId(int userId)
        {
            return await _userRepository.GetUserByUserId(userId);
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            }

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<AuthenticateDto> GetUserByCredentials(string email, string? password, bool? isGoogleLogin = false)
        {
            var driver = await _driversRepository.GetDriverByEmail(email, 0);
            var user = await GetUserByEmail(email);
            if (driver == null)
            {
                if (user == null)
                {
                    throw new Exception("User email or password is incorrect");
                }

                if (user.PasswordHash == null)
                {
                    throw new Exception("Please reset the password.");
                }

                if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                {
                    throw new Exception("User email or password is incorrect");
                }
                var resultUser = await CreateJWTToken(user);
                return resultUser;
            }
            else
            {
                if (driver.PasswordHash == null)
                {
                    throw new Exception("Please reset the password.");
                }
                if (!VerifyPasswordHash(password, driver.PasswordHash, driver.PasswordSalt))
                {
                    throw new Exception("User email or password is incorrect");
                }

            }

            var result = await CreateJWTTokenForDriver(driver);
            return result;

        }

        public async Task<Users> GetUserByEmail(string email)
        {
            return await _userRepository.GetUserByEmail(email);
        }

        public async Task<AuthenticateDto> CreateJWTToken(Users user)
        {
            byte[] key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            DateTime expires = DateTime.Now.AddHours(12);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserId.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = expires,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenString = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));

            var result = user.Adapt<AuthenticateDto>();
            result.Token = tokenString;
            result.TokenExpire = expires;
            //await LogoutUser(user.UserId, true);
            return result;
        }

        public async Task<AuthenticateDto> CreateJWTTokenForDriver(Drivers user)
        {
            byte[] key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            DateTime expires = DateTime.Now.AddHours(12);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.DriverID.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = expires,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenString = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));

            var result = user.Adapt<AuthenticateDto>();
            result.Token = tokenString;
            result.TokenExpire = expires;
            //await LogoutUser(user.UserId, true);
            return result;
        }

        public async Task LogoutUser(int id, bool isLoggedIn)
        {
            await _userRepository.LogoutUser(id, isLoggedIn);
        }

        private bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            }

            if (storedHash.Length != 64)
            {
                throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            }

            if (storedSalt.Length != 128)
            {
                throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");
            }

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }
            return true;
        }

        public async Task<CommonResultResponseDto<PaginatedList<GetUsersResponseDto>>> GetUsers(string filterModel, ServerRowsRequest commonRequest, string getSort)
        {
            var (users, total) = await _userRepository.GetUsers(filterModel, commonRequest, getSort);
            return CommonResultResponseDto<PaginatedList<GetUsersResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, new PaginatedList<GetUsersResponseDto>(users, total), 0);
        }

        public async Task<CommonResultResponseDto<string>> DeleteUser(int id, string role)
        {
            int deletedId = 0;

            if (role == "Driver")
            {
                deletedId = await _userRepository.DeleteDriver(id);
            }
            else
            {
                deletedId = await _userRepository.DeleteUser(id);
            }

            if (deletedId > 0)
            {
                return CommonResultResponseDto<string>.Success(new[] { ActionStatusConstant.Deleted }, null, deletedId);
            }
            else
            {
                return CommonResultResponseDto<string>.Failure(new[] { "Something went wrong" }, null);
            }
        }


    }
}

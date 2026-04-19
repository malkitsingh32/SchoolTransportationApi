using Domain.Entities;

namespace DTO.Response.User
{
    public class UserAndTokenResponseDto
    {
        public Tokens Tokens { get; set; }
        public UserInfoDto UserInfoDto { get; set; }
        //public List<Permissions> Permissions { get; set; }
    }
}

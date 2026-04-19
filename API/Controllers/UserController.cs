using Application.Handler.User.Command.AddUser;
using Application.Handler.User.Command.AuthenticateUser;
using Application.Handler.User.Queries.GetUser;
using DTO.Request.User;
using DTO.Response;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    public class UserController : BaseController
    {
        #region Commands
        [HttpPost]
        [AllowAnonymous]
        [Route("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] AddUserDto addUserDto)
        {
            CommonResultResponseDto<string> result = await Mediator.Send(addUserDto.Adapt<AddUserCommand>());
            return Ok(result);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("AuthenticateUser")]
        public async Task<IActionResult> AuthenticateUser([FromBody] AuthenticateUserDto authenticateUserDto) => Ok(await Mediator.Send(authenticateUserDto.Adapt<AuthenticateUserCommand>()));
        #endregion Commands

        #region Queries
        [HttpGet]

        [Route("GetUserByUserId")]
        public async Task<IActionResult> GetUsersByUserId([FromQuery] UserByUserIdDto userByUserIdDto)
        {
            var query = userByUserIdDto.Adapt<GetUserByUserIdQuery>();
            var user = await Mediator.Send(query);
            return Ok(user);
        }
        #endregion Queries
    }
}

using Application.Handler.Notification.Command.SaveBulkMessage;
using Application.Handler.Notification.Queries.GetBulkMessages;
using DTO.Request.Notification;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : BaseController
    {
        #region Command
        [HttpPost]
        [AllowAnonymous]
        [Route("SendBulkMessage")]
        public async Task<IActionResult> SaveBulkMessage([FromBody] SaveBulkMessageRequestDto saveBulkMessageRequestDto) => Ok(await Mediator.Send(saveBulkMessageRequestDto.Adapt<SaveBulkMessageCommand>()));

        #endregion

        #region Query
        [HttpPost]
        [AllowAnonymous]
        [Route("GetBulkMessages")]
        public async Task<IActionResult> GetBulkMessages([FromBody] SaveBulkMessageRequestDto saveBulkMessageRequestDto)
        {
            var result =  await Mediator.Send(saveBulkMessageRequestDto.Adapt<GetBulkMessagesQuery>());
            return Ok(result);
        }
        #endregion
    }
}

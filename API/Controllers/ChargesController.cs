using Application.Handler.Charges.Command.AddUpdateCharges;
using Application.Handler.Charges.Queries;
using DTO.Request.Charges;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ChargesController : BaseController
    {

        #region Query

        [HttpPost]
        //[AllowAnonymous]
        [Route("AddUpdateCharges")]
        public async Task<IActionResult> AddUpdateCharges([FromBody] AddUpdateChargesRequestDto addUpdateChargesRequestDto) => Ok(await Mediator.Send(addUpdateChargesRequestDto.Adapt<AddUpdateChargesCommand>()));

        [HttpPost]
        //[AllowAnonymous]
        [Route("GetCharges")]
        public async Task<IActionResult> GetCharges([FromBody] GetChargesRequestDto getChargesRequestDto)
        {
            var result = await Mediator.Send(new GetChargesQuery
            {
                CommonRequest = getChargesRequestDto.CommonRequest,
                StudentId = getChargesRequestDto.StudentId
            });
            return Ok(result);
        }
        #endregion
    }
}

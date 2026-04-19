using Application.Handler.Bus.Command;
using Application.Handler.Bus.Command.AddUpdateBusDetails;
using Application.Handler.Bus.Command.DeleteBusDetails;
using Application.Handler.Bus.Queries.GetAllBusDetails;
using Application.Handler.Bus.Queries.GetBusStatus;
using DTO.Request.BusDetails;
using DTO.Response;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BusDetailsController : BaseController
    {
        #region Queries

        [HttpPost]
        //[AllowAnonymous]
        [Route("GetAllBusDetails")]
        public async Task<IActionResult> GetAllBusDetails()
        {
            var result = await Mediator.Send(new GetAllBusDetailsQuery());
            return Ok(result);
        }

        [HttpPost]
        //[AllowAnonymous]
        [Route("GetBusStatus")]
        public async Task<IActionResult> GetBusStatus()
        {
            var result = await Mediator.Send(new GetBusStatusQuery());
            return Ok(result);
        }

        #endregion Queries


        #region Command
        [HttpPost]
        //[AllowAnonymous]
        [Route("GetBuses")]
        public async Task<IActionResult> GetBuses([FromBody] GetBusesDetailsRequestDto getBusesDetailsRequestDto)
        {
            var result = await Mediator.Send(new GetBusesCommand
            {
                CommonRequest = getBusesDetailsRequestDto.CommonRequest,
                RouteId = getBusesDetailsRequestDto.RouteId
            });
            return Ok(result);
        }

        [HttpPost]
        //[AllowAnonymous]
        [Route("AddUpdateBusDetails")]
        public async Task<IActionResult> AddUpdateBusDetails([FromBody] AddUpdateBusDetailsRequestDto addUpdateBusDetailsRequestDto) => Ok(await Mediator.Send(addUpdateBusDetailsRequestDto.Adapt<AddUpdateBusDetailsCommand>()));


        [HttpPost]
        //[AllowAnonymous]
        [Route("DeleteBusDetails")]
        public async Task<IActionResult> DeleteBusDetails([FromQuery] DeleteBusDetailsRequestDto deleteBusDetailsRequestDto)
        {
            CommonResultResponseDto<string> result = await Mediator.Send(deleteBusDetailsRequestDto.Adapt<DeleteBusDetailsCommand>());
            return Ok(result);
        }

        #endregion Command
    }
}

using Application.Handler.Drivers.Command.AddUpdateDriversDetails;
using Application.Handler.Drivers.Command.AssignRouteToDriver;
using Application.Handler.Drivers.Command.CheckOTP;
using Application.Handler.Drivers.Command.DeductReservedAccountBalance;
using Application.Handler.Drivers.Command.DeleteBusAndDriverRoute;
using Application.Handler.Drivers.Command.DeleteDrivers;
using Application.Handler.Drivers.Command.ResetDriverPassword;
using Application.Handler.Drivers.Command.SendLinkToResetDriverPassword;
using Application.Handler.Drivers.Command.SendOtpOnEmail;
using Application.Handler.Drivers.Queries;
using Application.Handler.Drivers.Queries.ExportDriversList;
using Application.Handler.Drivers.Queries.GetDrivers;
using Application.Handler.Drivers.Queries.GetDriversBalanceHistory;
using Application.Handler.Drivers.Queries.GetDriverTypeList;
using DTO.Request.Drivers;
using DTO.Request.Routes;
using DTO.Response;
using DTO.Response.Driver;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DriversController : BaseController
    {
        #region Command
        [HttpPost]
        //[AllowAnonymous]
        [Route("DeleteDrivers")]
        public async Task<IActionResult> DeleteDrivers([FromQuery] DeleteDriversRequestDto deleteDriversRequestDto)
        {
            CommonResultResponseDto<string> result = await Mediator.Send(deleteDriversRequestDto.Adapt<DeleteDriversCommand>());
            return Ok(result);
        }

        [HttpPost]
        [Route("DeleteBusAndDriverRoute")]
        public async Task<IActionResult> DeleteBusAndDriverRoute([FromBody] DeleteBusAndDriverRouteRequestDto deleteBusAndDriverRouteRequestDto)
        {
            CommonResultResponseDto<string> result = await Mediator.Send(deleteBusAndDriverRouteRequestDto.Adapt<DeleteBusAndDriverRouteCommand>());
            return Ok(result);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("ResetDriverPassword")]
        public async Task<IActionResult> ResetDriverPassword([FromBody] ResetDriverPasswordDto resetDriverPasswordDto) => Ok(await Mediator.Send(resetDriverPasswordDto.Adapt<ResetDriverPasswordCommand>()));
        [AllowAnonymous]
        [HttpPost]
        [Route("SendOtpOnEmail")]
        public async Task<IActionResult> SendOtpOnEmail([FromBody] SendOtpOnEmailRequestDto sendOtpOnEmailRequestDto)
        {
            var result = await Mediator.Send(sendOtpOnEmailRequestDto.Adapt<SendOtpOnEmailQuery>());
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("CheckOTP")]
        public async Task<IActionResult> CheckOTP([FromBody] CheckOTPRequestDto checkOTPRequestDto)
        {
            var result = await Mediator.Send(checkOTPRequestDto.Adapt<CheckOTPQuery>());
            return Ok(result);
        }
        #endregion

        #region Query
        [HttpGet]
        //[AllowAnonymous]
        [Route("GetDriversList")]
        public async Task<IActionResult> GetDriversList()
        {
            var result = await Mediator.Send(new GetDriversListQuery());
            return Ok(result);
        }

        [HttpPost]
        //[AllowAnonymous]
        [Route("GetDrivers")]
        public async Task<IActionResult> GetDrivers([FromBody] GetDriversRequestDto getDriversRequestDto)
        {
            var result = await Mediator.Send(new GetDriversQuery
            {
                CommonRequest = getDriversRequestDto.CommonRequest,
                RouteId = getDriversRequestDto.RouteId
            });
            return Ok(result);
        }

        [HttpPost]
        //[AllowAnonymous]
        [Route("GetDriversBalanceHistory")]
        public async Task<IActionResult> GetDriversBalanceHistory([FromBody] GetDriversBalanceHistoryRequestDto getDriversBalanceHistoryRequestDto)
        {
            var result = await Mediator.Send(new GetDriversBalanceHistoryQuery
            {
                CommonRequest = getDriversBalanceHistoryRequestDto.CommonRequest,
                DriverId = getDriversBalanceHistoryRequestDto.DriverId
            });
            return Ok(result);
        }

        [HttpPost]
        //[AllowAnonymous]
        [Route("AddUpdateDriversDetails")]
        public async Task<IActionResult> AddUpdateDriversDetails([FromBody] AddUpdateDriversDetailsRequestDto addUpdateDriversDetailsRequestDto) => Ok(await Mediator.Send(addUpdateDriversDetailsRequestDto.Adapt<AddUpdateDriversDetailsCommand>()));

        [HttpPost]
        //[AllowAnonymous]
        [Route("DeductReservedAccountBalance")]
        public async Task<IActionResult> DeductReservedAccountBalance([FromBody] DeductReservedAccountBalanceRequestDto deductReservedAccountBalanceRequestDto) => Ok(await Mediator.Send(deductReservedAccountBalanceRequestDto.Adapt<DeductReservedAccountBalanceCommand>()));

        [HttpPost]
        //[AllowAnonymous]
        [Route("GetDriverTypeList")]
        public async Task<IActionResult> GetDriverTypeList( )
        {
            var result = await Mediator.Send(new GetDriverTypeListQuery());
            return Ok(result);
        }

        [HttpPost]
        //[AllowAnonymous]
        [Route("AssignRouteToDriver")]
        public async Task<IActionResult> AssignRouteToDriver([FromBody] AssignRouteToDriverRequestDto assignRouteToDriverRequestDto) => Ok(await Mediator.Send(assignRouteToDriverRequestDto.Adapt<AssignRouteToDriverCommand>()));

        [HttpPost]
        [AllowAnonymous]
        [Route("SendLinkToResetDriverPassword")]
        public async Task<IActionResult> SendLinkToResetDriverPassword([FromBody] SendLinkToResetDriverPasswordDto sendLinkToResetDriverPasswordDto) => Ok(await Mediator.Send(sendLinkToResetDriverPasswordDto.Adapt<SendLinkToResetDriverPasswordCommand>()));


        [HttpPost]
        [Route("ExportDriversList")]
        [AllowAnonymous]
        public async Task<IActionResult> ExportDriversList()
        {
            var result = await Mediator.Send(new ExportDriversListQuery());

            return File(result.FileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                result.FileName
            );
        }
        #endregion
    }
}

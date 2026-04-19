using Application.Common.Dtos;
using Application.Handler.Payroll.Command.addUpdatePayrollDetails;
using Application.Handler.Payroll.Command.DeletePayroll;
using Application.Handler.Payroll.Queries;
using Application.Handler.Payroll.Queries.GetPayrollByDateAndDriverId;
using Application.Handler.Payroll.Queries.GetPayrollForAllDriversByDate;
using Application.Handler.Payroll.Queries.GetRoutesHistoryByDriver;
using Application.Handler.Payroll.Queries.GetWeeklyBulkDriverPayroll;
using Application.Handler.Payroll.Queries.GetWeeklyDriverSummary;
using DTO.Request.Payroll;
using DTO.Response;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PayrollController : BaseController
    {
        #region Query

        [HttpPost]
        //[AllowAnonymous]
        [Route("GetPayroll")]
        public async Task<IActionResult> GetPayroll([FromBody] ServerRowsRequest serverRowsRequest)
        {
            var result = await Mediator.Send(new GetPayrollQuery { CommonRequest = serverRowsRequest });
            return Ok(result);
        }

        [HttpPost]
        // [AllowAnonymous]
        [Route("GetWeeklyDriverSummary")]
        public async Task<IActionResult> GetWeeklyDriverSummary([FromBody] ServerRowsRequest serverRowsRequest)
        {
            var result = await Mediator.Send(new GetWeeklyDriverSummaryQuery { CommonRequest = serverRowsRequest });
            return Ok(result);
        }

        [HttpPost]
        //[AllowAnonymous]
        [Route("GetRoutesHistoryByDriverId")]
        public async Task<IActionResult> GetRoutesHistoryByDriverId([FromBody] GetRoutesHistoryByDriverRequestDto getRoutesHistoryByDriverRequestDto)
        {
            var result = await Mediator.Send(new GetRoutesHistoryByDriverIdQuery
            {
                CommonRequest = getRoutesHistoryByDriverRequestDto.CommonRequest,
                DriverId = getRoutesHistoryByDriverRequestDto.DriverId,
                Date = getRoutesHistoryByDriverRequestDto.Date
            });
            return Ok(result);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("GetPayrollByDateAndDriverId")]
        public async Task<IActionResult> GetPayrollByDateAndDriverId([FromBody] GetPayrollByDateAndDriverIdRequestDto getPayrollByDateAndDriverIdRequestDto)
        {
            var result = await Mediator.Send(new GetPayrollByDateAndDriverIdQuery
            {
                CommonRequest = getPayrollByDateAndDriverIdRequestDto.CommonRequest,
                DriverId = getPayrollByDateAndDriverIdRequestDto.DriverId,
                StartDate = getPayrollByDateAndDriverIdRequestDto.StartDate,
                EndDate = getPayrollByDateAndDriverIdRequestDto.EndDate,
            });
            return Ok(result);
        }

        [HttpPost]
        [Route("GetWeeklyBulkDriverPayroll")]
        public async Task<IActionResult> GetWeeklyBulkDriverPayroll([FromBody] GetWeeklyBulkDriverPayrollRequestDto getWeeklyBulkDriverPayrollRequest)
        {
            var result = await Mediator.Send(getWeeklyBulkDriverPayrollRequest.Adapt<GetWeeklyBulkDriverPayrollQuery>());
            return Ok(result);
        }

        [HttpPost]
        [Route("GetPayrollForAllDriversByDate")]
        public async Task<IActionResult> GetPayrollForAllDriversByDate([FromBody] GetPayrollForAllDriversByDateRequestDto getPayrollForAllDriversByDateRequestDto)
        {
            var result = await Mediator.Send(getPayrollForAllDriversByDateRequestDto.Adapt<GetPayrollForAllDriversByDateQuery>());
            return Ok(result);
        }
        #endregion

        #region Command
        [HttpPost]
        //[AllowAnonymous]
        [Route("AddUpdatePayrollDetails")]
        public async Task<IActionResult> AddUpdatePayrollDetails([FromBody] AddUpdatePayrollDetailsRequestDto addUpdatePayrollDetailsRequestDto) => Ok(await Mediator.Send(addUpdatePayrollDetailsRequestDto.Adapt<AddUpdatePayrollDetailsCommand>()));

        [HttpPost]
        //[AllowAnonymous]
        [Route("DeletePayroll")]
        public async Task<IActionResult> DeletePayroll([FromBody] DeletePayrollRequestDto deletePayrollRequestDto)
        {
            CommonResultResponseDto<string> result = await Mediator.Send(deletePayrollRequestDto.Adapt<DeletePayrollCommand>());
            return Ok(result);
        }
        #endregion
    }
}

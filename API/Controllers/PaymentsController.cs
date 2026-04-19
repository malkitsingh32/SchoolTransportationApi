using Application.Common.Dtos;
using Application.Handler.Payments.Command.RecordPayment;
using Application.Handler.Payments.Queries;
using Application.Handler.Payments.Queries.GetAllTransactions;
using DTO.Request.Payments;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : BaseController
    {
        #region Query

        [HttpPost]
        //[AllowAnonymous]
        [Route("GetPayments")]
        public async Task<IActionResult> GetPayments([FromBody] GetPaymentsRequestDto getChargesRequestDto)
        {
            var result = await Mediator.Send(new GetPaymentsQuery
            {
                CommonRequest = getChargesRequestDto.CommonRequest,
                StudentId = getChargesRequestDto.StudentId
            });
            return Ok(result);
        }

        [HttpPost]
        //[AllowAnonymous]
        [Route("GetAllTransactions")]
        public async Task<IActionResult> GetAllTransactions([FromBody] ServerRowsRequest commonRequest)
        {
            var result = await Mediator.Send(new GetAllTransactionsQuery { CommonRequest = commonRequest });
            return Ok(result);
        }
        #endregion

        #region Command


        [HttpPost]
        //[AllowAnonymous]
        [Route("RecodePayment")]
        public async Task<IActionResult> RecodePayment([FromBody] RecodePaymentRequestDto recodePaymentRequestDto) => Ok(await Mediator.Send(recodePaymentRequestDto.Adapt<RecodePaymentCommand>()));
        #endregion
    }
}

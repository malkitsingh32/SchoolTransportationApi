using Application.Handler.BusChanges.Command.AddUpdateBusChanges;
using Application.Handler.BusChanges.Command.GetBusChangeChargesByFamilyId;
using Application.Handler.BusChanges.Command.GetBusChangeStudentsList;
using Application.Handler.BusChanges.Queries.GetBusChanges;
using Application.Handler.Students.Command;
using Application.Handler.Students.Queries;
using Application.Handler.Students.Queries.GetFamilyChargesByFamilyId;
using DTO.Request.BusChanges;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Application.Handler.Students.Command.GetStudentsCommandHandler;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BusChangesController : BaseController
    {

        #region Query
                      [HttpPost]
        //[AllowAnonymous]
        [Route("GetBusChanges")]
        public async Task<IActionResult> GetBusChanges([FromBody] GetBusChangesRequestDto getBusChangesRequestDto)
        {
            var result = await Mediator.Send(new GetBusChangesQuery
            {
                CommonRequest = getBusChangesRequestDto.CommonRequest,
                StudentId = getBusChangesRequestDto.StudentId
            });
            return Ok(result);
        }

        [HttpPost]
        //[AllowAnonymous]
        [Route("GetBusChangeStudentsList")]
        public async Task<IActionResult> GetBusChangeStudentsList([FromBody] GetStudentsRequestDto getStudentsRequestDto)
        {
            var result = await Mediator.Send(new GetBusChangeStudentsListCommand
            {
                CommonRequest = getStudentsRequestDto.CommonRequest,
                RouteId = getStudentsRequestDto.RouteId,
                StreetId = getStudentsRequestDto.streetId,
                FamilyId = getStudentsRequestDto.FamilyId,
                NtId = getStudentsRequestDto.NtId,
                Dob = getStudentsRequestDto.Dob,
                District = getStudentsRequestDto.District,
                SchoolStudentId = getStudentsRequestDto.SchoolStudentId,
                SchoolId = getStudentsRequestDto.SchoolId,
                Grade = getStudentsRequestDto.Grade,
                Gender = getStudentsRequestDto.Gender
            });
            return Ok(result);
        }
        #endregion
        #region Command


        [HttpPost]
        //[AllowAnonymous]
        [Route("AddUpdateBusChanges")]
        public async Task<IActionResult> AddUpdateBusChanges([FromBody] AddUpdateBusChangesRequestDto addUpdateBusChangesRequestDto) => Ok(await Mediator.Send(addUpdateBusChangesRequestDto.Adapt<AddUpdateBusChangesCommand>()));

        [HttpPost]
        [AllowAnonymous]
        [Route("GetBusChangeChargesByFamilyId")]
        public async Task<IActionResult> GetBusChangeChargesByFamilyId([FromBody] GetBusChargesByFamilyIdRequestDto getFamilyChargesByFamilyIdRequestDto)
        {
            var result = await Mediator.Send(new GetBusChangeChargesByFamilyIdQuery
            {
                CommonRequest = getFamilyChargesByFamilyIdRequestDto.CommonRequest,
                FamilyId = getFamilyChargesByFamilyIdRequestDto.FamilyId
            });
            return Ok(result);
        }
        #endregion Command

    }
}


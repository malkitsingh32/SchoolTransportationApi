using Application.Handler.Routes.Queries.GetStudentsWithChangedAddress;
using Application.Handler.Students.Command;
using Application.Handler.Students.Command.AddFamilyCharge;
using Application.Handler.Students.Command.AddUpdateBulkStudentsRouteId;
using Application.Handler.Students.Command.AddUpdateStudents;
using Application.Handler.Students.Command.AssignChangeAddressStudent;
using Application.Handler.Students.Command.AssignRouteToStudent;
using Application.Handler.Students.Command.DeleteStudent;
using Application.Handler.Students.Command.DownloadStudentApplicationForm;
using Application.Handler.Students.Command.GetStudentsForBulkRoutes;
using Application.Handler.Students.Command.GetUnassignedStudents;
using Application.Handler.Students.Command.ImportBulkStudents;
using Application.Handler.Students.Command.UpdateBulkNtStatus;
using Application.Handler.Students.Command.UpdateBusStopIndex;
using Application.Handler.Students.Command.UpdateFundedDetailsFromExcel;
using Application.Handler.Students.Command.UpdateStudentRouteNote;
using Application.Handler.Students.Command.UpdateStudentsIndex;
using Application.Handler.Students.Queries;
using Application.Handler.Students.Queries.ExportStudentsList;
using Application.Handler.Students.Queries.GetAllBranch;
using Application.Handler.Students.Queries.GetBranchByBuildingId;
using Application.Handler.Students.Queries.GetFamilyChargesByFamilyId;
using Application.Handler.Students.Queries.GetFamilyChargesForDropdown;
using Application.Handler.Students.Queries.GetFamilyDetails;
using Application.Handler.Students.Queries.GetFamilyList;
using Application.Handler.Students.Queries.GetGradeBranchList;
using Application.Handler.Students.Queries.GetGradeList;
using Application.Handler.Students.Queries.GetStudentsByFamilyId;
using Application.Handler.Students.Queries.GetStudentsWithBusChangeList;
using Application.Handler.Students.Queries.SearchLocation;
using DTO.Request.Students;
using DTO.Response;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Application.Handler.Routes.Queries.GetStudentsWithChangedAddress.GetStudentsWithChangedAddressQueryHandler;
using static Application.Handler.Students.Command.GetStudentsCommandHandler;
using static Application.Handler.Students.Command.GetStudentsForBulkRoutes.GetStudentsForBulkRoutesCommandHandler;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    public class StudentsController : BaseController
    {
        #region Command
        [HttpPost]
        //[AllowAnonymous]
        [Route("GetStudents")]
        public async Task<IActionResult> GetStudents([FromBody] GetStudentsRequestDto getStudentsRequestDto)
        {
            var result = await Mediator.Send(new GetStudentsCommand
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

        [HttpPost]
        //[AllowAnonymous]
        [Route("GetStudentsForBulkRoutes")]
        public async Task<IActionResult> GetStudentsForBulkRoutes([FromBody] GetStudentsForBulkRouteRequestDto getStudentsRequestDto)
        {
            var result = await Mediator.Send(new GetStudentsForBulkRoutesCommand
            {
                CommonRequest = getStudentsRequestDto.CommonRequest,
                Area = getStudentsRequestDto.Area,
                School = getStudentsRequestDto.School,
                Grade = getStudentsRequestDto.Grade,
                Gender = getStudentsRequestDto.Gender,
                Building = getStudentsRequestDto.Building,
                Branch = getStudentsRequestDto.Branch,
                Street = getStudentsRequestDto.Street,
                UniqueId = getStudentsRequestDto.UniqueId,
                UserId = getStudentsRequestDto.UserId,
                RouteId = getStudentsRequestDto.RouteId
            });
            return Ok(result);
        }

        [HttpPost]
        //[AllowAnonymous]
        [Route("GetUnassignedStudents")]
        public async Task<IActionResult> GetUnassignedStudents([FromBody] GetStudentsRequestDto getStudentsRequestDto)
        {
            var result = await Mediator.Send(new GetUnassignedStudentsCommand
            {
                CommonRequest = getStudentsRequestDto.CommonRequest,
                RouteId = getStudentsRequestDto.RouteId,
                StreetId = getStudentsRequestDto.streetId,
                FamilyId = getStudentsRequestDto.FamilyId,
                RouteTypeIds = getStudentsRequestDto.RouteTypeIds,
                GenderId = getStudentsRequestDto.GenderId

            });
            return Ok(result);
        }


        [HttpPost]
        //[AllowAnonymous]
        [Route("AddUpdateStudents")]
        public async Task<IActionResult> AddUpdateStudents([FromBody] AddUpdateStudentsDto addUpdateStudentsDto) => Ok(await Mediator.Send(addUpdateStudentsDto.Adapt<AddUpdateStudentsCommand>()));

        [HttpPost]
        //[AllowAnonymous]
        [Route("AddUpdateBulkStudentsRouteId")]
        public async Task<IActionResult> AddUpdateBulkStudentsRouteId([FromBody] AddUpdateBulkStudentsRouteIdDto addUpdateBulkStudentsRouteIdDto) => Ok(await Mediator.Send(addUpdateBulkStudentsRouteIdDto.Adapt<AddUpdateBulkStudentsRouteIdCommand>()));


        [HttpPost]
        //[AllowAnonymous]
        [Route("DeleteStudent")]
        public async Task<IActionResult> DeleteStudent([FromQuery] DeleteStudentRequestDto deleteStudentRequestDto)
        {
            CommonResultResponseDto<string> result = await Mediator.Send(deleteStudentRequestDto.Adapt<DeleteStudentCommand>());
            return Ok(result);
        }

        [HttpPost]
        //[AllowAnonymous]
        [Route("AssignRouteToStudent")]
        public async Task<IActionResult> AssignRouteToStudent([FromBody] AssignRouteToStudentRequestDto assignRouteToStudentRequestDto) => Ok(await Mediator.Send(assignRouteToStudentRequestDto.Adapt<AssignRouteToStudentCommand>()));
        
        [HttpPost]
        //[AllowAnonymous]
        [Route("AssignChangeAddressStudent")]
        public async Task<IActionResult> AssignChangeAddressStudent([FromBody] AssignChangeAddressStudentRequestDto assignRouteToStudentRequestDto) => Ok(await Mediator.Send(assignRouteToStudentRequestDto.Adapt<AssignChangeAddressStudentCommand>()));

        [HttpPost]
        //[AllowAnonymous]
        [Route("UpdateStudentsIndex")]
        public async Task<IActionResult> UpdateStudentsIndex([FromBody] UpdateStudentsIndexRquestDto updateStudentsIndexRquestDto) => Ok(await Mediator.Send(updateStudentsIndexRquestDto.Adapt<UpdateStudentsIndexCommand>()));

        [HttpPost]
        //[AllowAnonymous]
        [Route("UpdateBusStopIndex")]
        public async Task<IActionResult> UpdateBusStopIndex([FromBody] UpdateBusStopIndexDto updateBusStopIndexDto) => Ok(await Mediator.Send(updateBusStopIndexDto.Adapt<UpdateBusStopIndexCommand>()));

        [HttpPost]
        //[AllowAnonymous]
        [Route("UpdateStudentRouteNote")]
        public async Task<IActionResult> UpdateStudentRouteNote([FromBody] UpdateStudentRouteNoteDto updateStudentRouteNoteDto) => Ok(await Mediator.Send(updateStudentRouteNoteDto.Adapt<UpdateStudentRouteNoteCommand>()));

        [HttpPost]
        //[AllowAnonymous]
        [Route("UpdateBulkNtStatus")]
        public async Task<IActionResult> UpdateBulkNtStatus([FromBody] UpdateBulkNtStatusRequestDto updateBulkNtStatusRequestDto) => Ok(await Mediator.Send(updateBulkNtStatusRequestDto.Adapt<UpdateBulkNtStatusCommand>()));
        [HttpPost]
        [Route("ImportBulkStudents")]
        public async Task<IActionResult> ImportBulkStudents([FromBody] ImportBulkStudentsRequestDto importBulkStudentsRequestDto) => Ok(await Mediator.Send(importBulkStudentsRequestDto.Adapt<ImportBulkStudentsCommand>()));

        //[HttpPost]
        //[AllowAnonymous]
        //[Route("UpdateFundedDetailsFromExcel")]
        //public async Task<IActionResult> UpdateFundedDetailsFromExcel([FromForm] UpdateFundedDetailsFromExcelCommand updateFundedDetailsFromExcelCommand) => Ok(await Mediator.Send(updateFundedDetailsFromExcelCommand));
        [HttpPost]
        [AllowAnonymous]
        [Route("UpdateFundedDetailsFromExcel")]
        public async Task<IActionResult> UpdateFundedDetailsFromExcel(
    [FromForm] UpdateFundedDetailsFromExcelCommand command)
        {
            var result = await Mediator.Send(command);
            if (result?.Data?.FileBytes != null && result.Data.FileBytes.Length > 0)
            {
                return File(
                    result.Data.FileBytes,
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    string.IsNullOrWhiteSpace(result.Data.FileName) ? "FundedDetails.xlsx" : result.Data.FileName
                );
            }

            return Ok(result);
        }
        #endregion Command

        #region Query

        [HttpGet]
        //[AllowAnonymous]
        [Route("GetStudentsList")]
        public async Task<IActionResult> GetStudentsList()
        {
            var result = await Mediator.Send(new GetStudentsListQuery());
            return Ok(result);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("GetStudentsWithBusChangeList")]
        public async Task<IActionResult> GetStudentsWithBusChangeList()
        {
            var result = await Mediator.Send(new GetStudentsWithBusChangeListQuery());
            return Ok(result);
        }

        [HttpGet]
        //[AllowAnonymous]
        [Route("GetFamilyList")]
        public async Task<IActionResult> GetFamilyList()
        {
            var result = await Mediator.Send(new GetFamilyListQuery());
            return Ok(result);
        }

        [HttpPost]
        [Route("GetFamilyDetails")]
        public async Task<IActionResult> GetFamilyDetails([FromBody] GetFamilyDetailsRequestDto getFamilyDetailsRequestDto)
        {
            var result = await Mediator.Send(getFamilyDetailsRequestDto.Adapt<GetFamilyDetailsQuery>());
            return Ok(result);
        }

        [HttpGet]
        //[AllowAnonymous]
        [Route("GetAllBranch")]
        public async Task<IActionResult> GetAllBranch()
        {
            var result = await Mediator.Send(new GetAllBranchQuery());
            return Ok(result);
        } 
        
        [HttpPost]
        //[AllowAnonymous]
        [Route("GetBranchByBuildingId")]
        public async Task<IActionResult> GetBranchByBuildingId([FromBody] GetBranchByBuildingIdRequestDto getBranchByBuildingIdRequestDto)
        {
            var result = await Mediator.Send(getBranchByBuildingIdRequestDto.Adapt<GetBranchByBuildingIdQuery>());
            return Ok(result);
        }

        [HttpGet]
        //[AllowAnonymous]
        [Route("GetGradeList")]
        public async Task<IActionResult> GetGradeList()
        {
            var result = await Mediator.Send(new GetGradeListQuery());
            return Ok(result);
        }

        [HttpGet]
        //[AllowAnonymous]
        [Route("GetGradeBranchList")]
        public async Task<IActionResult> GetGradeBranchList()
        {
            var result = await Mediator.Send(new GetGradeBranchListQuery());
            return Ok(result);
        }


        [HttpPost]
        [Route("ExportStudentsList")]
        [AllowAnonymous]
        public async Task<IActionResult> ExportStudentsList([FromBody] ExportStudentsListQuery request)
        {
            var result = await Mediator.Send(request);

            return File(result.FileBytes,"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                result.FileName
            );
        }


        [HttpPost]
        [Route("Searchlocation")]
        [AllowAnonymous]
        public async Task<IActionResult> Searchlocation(SearchLocationRequestDto searchLocationRequestDto)
        {
            var result = await Mediator.Send(searchLocationRequestDto.Adapt<SearchLocationQuery>());
            return Ok(result);
        }

        [HttpPost]
        //[AllowAnonymous]
        [Route("GetStudentsWithChangedAddress")]
        public async Task<IActionResult> GetStudentsWithChangedAddress([FromBody] GetStudentsWithChangedAddressRequestDto getStudentsWithChangedAddressRequestDto)
        {
            var result = await Mediator.Send(new GetStudentsWithChangedAddressQuery
            {
                CommonRequest = getStudentsWithChangedAddressRequestDto.CommonRequest,
                RouteTypeIds = getStudentsWithChangedAddressRequestDto.RouteTypeIds,
                GenderId = getStudentsWithChangedAddressRequestDto.GenderId
            });
            return Ok(result);
        }

        [HttpPost]
        //[AllowAnonymous]
        [Route("GetStudentsByFamilyId")] 
        public async Task<IActionResult> GetStudentsByFamilyId([FromBody] GetStudentsByFamilyIdRequestDto getStudentsByFamilyIdRequestDto)
        {
            var result = await Mediator.Send(new GetStudentsByFamilyIdQuery
            {
                CommonRequest = getStudentsByFamilyIdRequestDto.CommonRequest,
                FamilyId = getStudentsByFamilyIdRequestDto.FamilyId
            });
            return Ok(result);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("DownloadStudentApplicationForm")]
        public async Task<IActionResult> DownloadStudentApplicationForm([FromQuery] DownloadStudentApplicationFormRequestDto downloadStudentApplicationFormRequestDto)
        {
            var result = await Mediator.Send(new DownloadStudentApplicationFormCommand { StudentId = downloadStudentApplicationFormRequestDto.StudentId });
            return File(result, "application/pdf", "ApplicationForm.pdf");
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("GetFamilyChargesByFamilyId")] 
        public async Task<IActionResult> GetFamilyChargesByFamilyId([FromBody] GetFamilyChargesByFamilyIdRequestDto getFamilyChargesByFamilyIdRequestDto)
        {
            var result = await Mediator.Send(new GetFamilyChargesByFamilyIdQuery
            {
                CommonRequest = getFamilyChargesByFamilyIdRequestDto.CommonRequest,
                FamilyId = getFamilyChargesByFamilyIdRequestDto.FamilyId
            });
            return Ok(result);
        }

        [HttpPost]
        [Route("AddFamilyCharge")]
        public async Task<IActionResult> AddFamilyCharge([FromBody] AddFamilyChargeDto dto)
        {
            return Ok(await Mediator.Send(dto.Adapt<AddFamilyChargeCommand>()));
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("GetFamilyChargesForDropdown")]
        public async Task<IActionResult> GetFamilyChargesForDropdown([FromBody] int familyId)
        {
            var result = await Mediator.Send(new GetFamilyChargesForDropdownQuery
            {
                FamilyId = familyId
            });

            return Ok(result);
        }

        #endregion Query
    }
}

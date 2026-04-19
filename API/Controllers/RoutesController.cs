using Application.Common.Dtos;
using Application.Handler.Routes.Command.AddBulkRoutesDetails;
using Application.Handler.Routes.Command.AddUpdateRoutesDetails;
using Application.Handler.Routes.Command.CheckRouteTypeStudent;
using Application.Handler.Routes.Command.DeleteRoutes;
using Application.Handler.Routes.Command.DeleteStudentFromRoute;
using Application.Handler.Routes.Command.DownloadPrintOrder;
using Application.Handler.Routes.Command.UndoRoutes;
using Application.Handler.Routes.Command.UpdateBulkRoutes;
using Application.Handler.Routes.Command.UpdateBulkRoutesDetails;
using Application.Handler.Routes.Command.UpdateDelayRoute;
using Application.Handler.Routes.Command.UpdateRouteGroup;
using Application.Handler.Routes.Command.UpdateSchoolBuildingBranchMapping;
using Application.Handler.Routes.Command.UpdateStop;
using Application.Handler.Routes.Command.UpdateTempBusDriverDetails;
using Application.Handler.Routes.Command.UpdateTodayDriver;
using Application.Handler.Routes.Queries.GetAddress;
using Application.Handler.Routes.Queries.GetAreasBySchoolAndGrade;
using Application.Handler.Routes.Queries.GetFilteredRoutesForBusChange;
using Application.Handler.Routes.Queries.GetHistoryByTab;
using Application.Handler.Routes.Queries.GetRoutes;
using Application.Handler.Routes.Queries.GetRoutesByTabs;
using Application.Handler.Routes.Queries.GetRoutesDetailList;
using Application.Handler.Routes.Queries.GetRoutesDetailsByTypeId;
using Application.Handler.Routes.Queries.GetRoutesList;
using Application.Handler.Routes.Queries.GetRoutesLists;
using Application.Handler.Routes.Queries.GetSchoolBuildingBranchByRouteId;
using Application.Handler.Routes.Queries.GetSchoolBuildingBranchDetails;
using Application.Handler.Routes.Queries.GetSchoolBuildingBranchList;
using Application.Handler.Routes.Queries.InsertNextDayRouteDetails;
using DTO.Request.Routes;
using DTO.Response;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Application.Handler.Routes.Queries.GetSchoolBuildingBranchByRouteId.GetSchoolBuildingBranchByRouteIdQueryHandler;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RoutesController : BaseController
    {

        #region Command


        [HttpPost]
        //[AllowAnonymous]
        [Route("DeleteRoutes")]
        public async Task<IActionResult> DeleteRoutes([FromQuery] DeleteRoutesRequestDto deleteRoutesRequestDto)
        {
            CommonResultResponseDto<string> result = await Mediator.Send(deleteRoutesRequestDto.Adapt<DeleteRoutesCommand>());
            return Ok(result);
        }
        
        [HttpPost]
        //[AllowAnonymous]
        [Route("UndoRoutes")]
        public async Task<IActionResult> UndoRoutes([FromQuery] UndoRoutesRequestDto undoRoutesRequestDto)
        {
            CommonResultResponseDto<string> result = await Mediator.Send(undoRoutesRequestDto.Adapt<UndoRoutesCommand>());
            return Ok(result);
        }

        [HttpPost]
        //[AllowAnonymous]
        [Route("DeleteStudentFromRoutes")]
        public async Task<IActionResult> DeleteStudentFromRoutes([FromQuery] DeleteStudentFromRoutesRequestDto deleteStudentFromRoutesRequestDto)
        {
            CommonResultResponseDto<string> result = await Mediator.Send(deleteStudentFromRoutesRequestDto.Adapt<DeleteStudentFromRouteCommand>());
            return Ok(result);
        }

        [HttpPost]
        //[AllowAnonymous]
        [Route("AddUpdateRoutesDetails")]
        public async Task<IActionResult> AddUpdateRoutesDetails([FromBody] AddUpdateRoutesDetailsRequestDto addUpdateRoutesDetailsRequestDto) => Ok(await Mediator.Send(addUpdateRoutesDetailsRequestDto.Adapt<AddUpdateRoutesDetailsCommand>()));

        [HttpPost]
        [Route("UpdateBulkRoutes")]
        public async Task<IActionResult> UpdateBulkRoutes([FromBody] UpdateBulkRoutesRequestDto updateBulkRoutesRequestDto) => Ok(await Mediator.Send(updateBulkRoutesRequestDto.Adapt<UpdateBulkRoutesCommand>()));

        [HttpPost]
        //[AllowAnonymous]
        [Route("AddBulkRoutesDetails")]
        public async Task<IActionResult> AddBulkRoutesDetails([FromBody] AddBulkRoutesDetailsRequestDto addBulkRoutesDetailsRequestDto) => Ok(await Mediator.Send(addBulkRoutesDetailsRequestDto.Adapt<AddBulkRoutesDetailsCommand>()));
        
        [HttpPost]
        //[AllowAnonymous]
        [Route("UpdateBulkRoutesDetails")]
        public async Task<IActionResult> UpdateBulkRoutesDetails([FromBody] UpdateBulkRoutesDetailsRequestDto addBulkRoutesDetailsRequestDto) => Ok(await Mediator.Send(addBulkRoutesDetailsRequestDto.Adapt<UpdateBulkRoutesDetailsCommand>()));

        [HttpPost]
        //[AllowAnonymous]
        [Route("CheckRouteTypeStudent")]
        public async Task<IActionResult> CheckRouteTypeStudent([FromBody] CheckRouteTypeStudentRequestDto addBulkRoutesDetailsRequestDto) => Ok(await Mediator.Send(addBulkRoutesDetailsRequestDto.Adapt<CheckRouteTypeStudentCommand>()));


        [HttpPost]
        //[AllowAnonymous]
        [Route("UpdateSchoolBuildingBranchMapping")]
        public async Task<IActionResult> UpdateSchoolBuildingBranchMapping([FromBody] UpdateSchoolBuildingBranchMappingDto updateSchoolBuildingBranchMappingDto) => Ok(await Mediator.Send(updateSchoolBuildingBranchMappingDto.Adapt<UpdateSchoolBuildingBranchMappingCommand>()));

        [HttpPost]
        //[AllowAnonymous]
        [Route("UpdateStop")]
        public async Task<IActionResult> UpdateStop([FromBody] UpdateStopDto updateStopDto) => Ok(await Mediator.Send(updateStopDto.Adapt<UpdateStopCommand>()));

        [HttpPost]
        //[AllowAnonymous]
        [Route("UpdateTempBusDriverDetails")]
        public async Task<IActionResult> UpdateTempBusDriverDetails([FromBody] UpdateTempBusDriverDetailsDto updateTempBusDriverDetailsDto) => Ok(await Mediator.Send(updateTempBusDriverDetailsDto.Adapt<UpdateTempBusDriverDetailsCommand>()));

        [HttpPost]
        //[AllowAnonymous]
        [Route("UpdateTodayDriver")]
        public async Task<IActionResult> UpdateTodayDriver([FromBody] UpdateTodayDriverDto updateTodayDriverDto) => Ok(await Mediator.Send(updateTodayDriverDto.Adapt<UpdateTodayDriverCommand>()));


        [HttpPost]
        //[AllowAnonymous]
        [Route("UpdateRouteGroup")]
        public async Task<IActionResult> UpdateRouteGroup([FromBody] UpdateRouteGroupDto updateTodayDriverDto) => Ok(await Mediator.Send(updateTodayDriverDto.Adapt<UpdateRouteGroupCommand>()));
          [HttpPost]
        //[AllowAnonymous]
        [Route("UpdateDelayRoute")]
        public async Task<IActionResult> UpdateDelayRoute([FromBody] UpdateDelayRouteDto updateTodayDriverDto) => Ok(await Mediator.Send(updateTodayDriverDto.Adapt<UpdateDelayRouteCommand>()));

        #endregion

        #region Querry

        [HttpPost]
        //[AllowAnonymous]
        [Route("GetRoutes")]
        public async Task<IActionResult> GetRoutes([FromBody] GetRoutesRequestDto getRoutesRequestDto)
        {
            var result = await Mediator.Send(new GetRoutesQuery
            {
                CommonRequest = getRoutesRequestDto.CommonRequest,
                BusDetailId = getRoutesRequestDto.BusDetailId,
                StreetId = getRoutesRequestDto.StreetId,
                DriverId = getRoutesRequestDto.DriverId,
                StudentId = getRoutesRequestDto.StudentId,
                SeasonFolderId = getRoutesRequestDto.SeasonFolderId,
                IsActiveRoutes = getRoutesRequestDto.IsActiveRoutes,
                Grade = getRoutesRequestDto.Grade,
                Role = getRoutesRequestDto.Role,
                School = getRoutesRequestDto.School,
                Gender = getRoutesRequestDto.Gender,
                RouteId = getRoutesRequestDto.RouteId,
            });
            return Ok(result);
        }

        [HttpPost]
        //[AllowAnonymous]
        [Route("GetRoutesByTabs")]
        public async Task<IActionResult> GetRoutesByTabs([FromBody] GetRoutesRequestDto getRoutesRequestDto)
        {
            var result = await Mediator.Send(new GetRoutesByTabsQuery
            {
                CommonRequest = getRoutesRequestDto.CommonRequest,
                BusDetailId = getRoutesRequestDto.BusDetailId,
                StreetId = getRoutesRequestDto.StreetId,
                DriverId = getRoutesRequestDto.DriverId,
                StudentId = getRoutesRequestDto.StudentId,
                SeasonFolderId = getRoutesRequestDto.SeasonFolderId,
                Grade = getRoutesRequestDto.Grade,
                Role = getRoutesRequestDto.Role
            });
            return Ok(result);
        }

        [HttpPost]
        //[AllowAnonymous]
        [Route("GetRoutesDetailsByTypeId")]
        public async Task<IActionResult> GetRoutesDetailsByTypeId([FromBody] GetRoutesDetailsByTypeIdRequestDto getRoutesRequestDto)
        {
            var result = await Mediator.Send(new GetRoutesDetailsByTypeIdQuery
            {
                CommonRequest = getRoutesRequestDto.CommonRequest,
                RouteTypeId = getRoutesRequestDto.RouteTypeId
            });
            return Ok(result);
        }

        [HttpGet]
        //[AllowAnonymous]
        [Route("GetRoutesList")]
        public async Task<IActionResult> GetRoutesList()
        {
            var result = await Mediator.Send(new GetRoutesListQuery());
            return Ok(result);
        } 
        
        [HttpPost]
        [AllowAnonymous]
        [Route("InsertNextDayRouteDetails")]
        public async Task<IActionResult> InsertNextDayRouteDetails()
        {
            var result = await Mediator.Send(new InsertNextDayRouteDetailsQuery());
            return Ok(result);
        }
        

        [HttpPost]
        //[AllowAnonymous]
        [Route("GetRoutesLists")]
        public async Task<IActionResult> GetRoutesLists([FromBody] GetRoutesListsRequestDto getRoutesListsRequestDto)
        {
            var result = await Mediator.Send(getRoutesListsRequestDto.Adapt<GetRoutesListsQuery>());
            return Ok(result);
        }



        [HttpGet]
        //[AllowAnonymous]
        [Route("GetRoutesDetailList")]
        public async Task<IActionResult> GetRoutesDetailList()
        {
            var result = await Mediator.Send(new GetRoutesDetailListQuery());
            return Ok(result);
        }

        [HttpPost]
        //[AllowAnonymous]
        [Route("GetHistoryByTab")]
        public async Task<IActionResult> GetHistoryByTab([FromBody] GetHistoryByTabRequestDto getHistoryByTabRequestDto)
        {
            var result = await Mediator.Send(new GetHistoryByTabQuery
            {
                CommonRequest = getHistoryByTabRequestDto.CommonRequest,
                Tab = getHistoryByTabRequestDto.Tab,
                Id = getHistoryByTabRequestDto.Id
            });
            return Ok(result);
        }

        [HttpPost]
        //[AllowAnonymous]
        [Route("GetSchoolBuildingBranchDetails")]
        public async Task<IActionResult> GetSchoolBuildingBranchDetails()
        {
            var result = await Mediator.Send(new GetSchoolBuildingBranchDetailsQuery());
            return Ok(result);
        }
        [HttpPost]
        //[AllowAnonymous]
        [Route("GetSchoolBuildingBranchList")]
        public async Task<IActionResult> GetSchoolBuildingBranchList([FromBody] ServerRowsRequest serverRowsRequest)
        {
            var result = await Mediator.Send(new GetSchoolBuildingBranchListQuery { CommonRequest = serverRowsRequest });
            return Ok(result);
        }

        [HttpPost]
        //[AllowAnonymous]
        [Route("GetSchoolBuildingBranchByRouteId")]
        public async Task<IActionResult> GetSchoolBuildingBranchByRouteId([FromBody] GetSchoolBuildingBranchByRouteIdRequestDto getRoutesRequestDto)
        {
            var result = await Mediator.Send(new GetSchoolBuildingBranchByRouteIdQuery
            {
                CommonRequest = getRoutesRequestDto.CommonRequest,
                RouteId = getRoutesRequestDto.RouteId
            });
            return Ok(result);
        }

      
        [HttpGet]
        //[AllowAnonymous]
        [Route("GetAddress")]
        public async Task<IActionResult> GetAddress()
        {
            var result = await Mediator.Send(new GetAddressQuery());
            return Ok(result);
        }
        [HttpPost]
        [Route("GetAreasBySchoolAndGrade")]
        public async Task<IActionResult> GetAreasBySchoolAndGrade([FromBody] GetAreasBySchoolAndGradeRequestDto getAreasBySchoolAndGradeRequestDto)
        {
            var result = await Mediator.Send(getAreasBySchoolAndGradeRequestDto.Adapt<GetAreasBySchoolAndGradeQuery>());
            return Ok(result);
        }

        [HttpPost]
        [Route("GetFilteredRoutesForBusChange")]
        public async Task<IActionResult> GetFilteredRoutesForBusChange([FromBody] GetFilteredRoutesForBusChangeRequestDto getFilteredRoutesForBusChangeRequestDto)
        {
            var result = await Mediator.Send(getFilteredRoutesForBusChangeRequestDto.Adapt<GetFilteredRoutesForBusChangeQuery>());
            return Ok(result);
        }

        [HttpGet]
        [Route("DownloadPrintForRoutes")]
        public async Task<IActionResult> DownloadPrintOrder([FromQuery] DownloadPrintForRoutesRequestDto downloadPrintForRoutesRequestDto)
        {
            var result = await Mediator.Send(new DownloadPrintOrderCommand { RouteId = downloadPrintForRoutesRequestDto.RouteId, Date = downloadPrintForRoutesRequestDto.Date });
            return File(result, "application/pdf", "RouteDetails.pdf");
        }
        #endregion
    }
}

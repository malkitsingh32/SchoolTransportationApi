using Application.Handler.Streets.Command.AddSchoolBuildingBranchMapping;
using Application.Handler.Streets.Command.AddUpdateStreets;
using Application.Handler.Streets.Command.DeleteStreet;
using Application.Handler.Streets.Command.UpdateStreetRouteMapping;
using Application.Handler.Streets.Queries.GetDriversList;
using Application.Handler.Streets.Queries.GetStreets;
using Application.Handler.Streets.Queries.GetStreetsByRouteAndArea;
using DTO.Request.Street;
using DTO.Response;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StreetsController : BaseController
    {
        #region Command
        [HttpPost]
        //[AllowAnonymous]
        [Route("AddUpdateStreets")]
        public async Task<IActionResult> AddUpdateStreets([FromBody] AddUpdateStreetsDto addUpdateStreetsDto) => Ok(await Mediator.Send(addUpdateStreetsDto.Adapt<AddUpdateStreetsCommand>()));
        
        [HttpPost]
        //[AllowAnonymous]
        [Route("UpdateStreetRouteMapping")]
        public async Task<IActionResult> UpdateStreetRouteMapping([FromBody] UpdateStreetRouteMappingDto updateStreetRouteMappingDto) => Ok(await Mediator.Send(updateStreetRouteMappingDto.Adapt<UpdateStreetRouteMappingCommand>()));

        [HttpPost]
        //[AllowAnonymous]
        [Route("AddSchoolBuildingBranchMapping")]
        public async Task<IActionResult> AddSchoolBuildingBranchMapping([FromBody] AddSchoolBuildingBranchMappingDto addSchoolBuildingBranchMappingDto) => Ok(await Mediator.Send(addSchoolBuildingBranchMappingDto.Adapt<AddSchoolBuildingBranchMappingCommand>()));


        [HttpPost]
        //[AllowAnonymous]
        [Route("DeleteStreets")]
        public async Task<IActionResult> DeleteStreets([FromQuery] DeleteStreetsRequestDto deleteStreetsRequestDto)
        {
            CommonResultResponseDto<string> result = await Mediator.Send(deleteStreetsRequestDto.Adapt<DeleteStreetsCommand>());
            return Ok(result);
        }

        #endregion

        #region Queries

        [HttpPost]
        //[AllowAnonymous]
        [Route("GetStreets")]
        public async Task<IActionResult> GetStreets([FromBody] GetStreetRequestDto getStreetRequestDto)
        {
            var result = await Mediator.Send(new GetStreetsQuery
            {
                CommonRequest = getStreetRequestDto.CommonRequest,
                RouteId = getStreetRequestDto.RouteId
            });
            return Ok(result);
        }

        [HttpGet]
        //[AllowAnonymous]
        [Route("GetStreetList")]
        public async Task<IActionResult> GetStreetList()
        {
            var result = await Mediator.Send(new GetStreetListQuery());
            return Ok(result);
        }

        [HttpPost]
        //[AllowAnonymous]
        [Route("GetStreetsByRouteAndArea")]
        public async Task<IActionResult> GetStreetsByRouteAndArea([FromBody] GetStreetRequestDto getStreetRequestDto)
        {
            var result = await Mediator.Send(new GetStreetsByRouteAndAreaQuery
            {
                CommonRequest = getStreetRequestDto.CommonRequest,
                RouteId = getStreetRequestDto.RouteId,
                //AreaId = getStreetRequestDto.AreaId
            });
            return Ok(result);
        }
        #endregion
    }
}



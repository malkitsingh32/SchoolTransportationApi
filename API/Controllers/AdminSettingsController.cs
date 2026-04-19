using Application.Common.Dtos;
using Application.Handler.Admin.Command.AddAreas;
using Application.Handler.Admin.Command.AddBuilding;
using Application.Handler.Admin.Command.AddBusType;
using Application.Handler.Admin.Command.AddCC;
using Application.Handler.Admin.Command.AddChargeStructure;
using Application.Handler.Admin.Command.AddDeductionAmount;
using Application.Handler.Admin.Command.AddDistrict;
using Application.Handler.Admin.Command.AddDriverType;
using Application.Handler.Admin.Command.AddGender;
using Application.Handler.Admin.Command.AddMessages;
using Application.Handler.Admin.Command.AddNT;
using Application.Handler.Admin.Command.AddRole;
using Application.Handler.Admin.Command.AddRunType;
using Application.Handler.Admin.Command.AddSchool;
using Application.Handler.Admin.Command.AddSchoolYears;
using Application.Handler.Admin.Command.AddSearchLocation;
using Application.Handler.Admin.Command.AddSeasonFolder;
using Application.Handler.Admin.Command.AddStreetsAreaMapping;
using Application.Handler.Admin.Command.AddUpdateBranch;
using Application.Handler.Admin.Command.AddUpdateGrade;
using Application.Handler.Admin.Command.AddUpdatePredefinedColor;
using Application.Handler.Admin.Command.DeleteAreas;
using Application.Handler.Admin.Command.DeleteBranch;
using Application.Handler.Admin.Command.DeleteBuilding;
using Application.Handler.Admin.Command.DeleteBusType;
using Application.Handler.Admin.Command.DeleteCC;
using Application.Handler.Admin.Command.DeleteChargeStructure;
using Application.Handler.Admin.Command.DeleteDistrict;
using Application.Handler.Admin.Command.DeleteDriverType;
using Application.Handler.Admin.Command.DeleteGender;
using Application.Handler.Admin.Command.DeleteGrade;
using Application.Handler.Admin.Command.DeleteMessage;
using Application.Handler.Admin.Command.DeleteNt;
using Application.Handler.Admin.Command.DeletePredefinedColor;
using Application.Handler.Admin.Command.DeleteRunType;
using Application.Handler.Admin.Command.DeleteSchools;
using Application.Handler.Admin.Command.DeleteSchoolYears;
using Application.Handler.Admin.Command.DeleteSeasonFolder;
using Application.Handler.Admin.Command.DeleteStreetAreaMapping;
using Application.Handler.Admin.Command.ImportKml;
using Application.Handler.Admin.Command.RouteTypeRequiredRules;
using Application.Handler.Admin.Command.UpdateBulkGrade;
using Application.Handler.Admin.Command.UpdateBusCharge;
using Application.Handler.Admin.Command.UpdateChargeStructure;
using Application.Handler.Admin.Command.UpdateDayStatus;
using Application.Handler.Admin.Command.UpdateFamilyDetail;
using Application.Handler.Admin.Command.UpdateFamilyTracking;
using Application.Handler.Admin.Command.UpdateGradeMapping;
using Application.Handler.Admin.Command.UpdatePermissions;
using Application.Handler.Admin.Command.UpdateProfileRequestDto;
using Application.Handler.Admin.Command.UpdateRouteTypeExclusivePay;
using Application.Handler.Admin.Command.UpdateTrackingTime;
using Application.Handler.Admin.Queries.ExportFamilyList;
using Application.Handler.Admin.Queries.GetAllBusType;
using Application.Handler.Admin.Queries.GetAllDistricts;
using Application.Handler.Admin.Queries.GetAllFamilyDetail;
using Application.Handler.Admin.Queries.GetAllGenders;
using Application.Handler.Admin.Queries.GetAllGrade;
using Application.Handler.Admin.Queries.GetAllNt;
using Application.Handler.Admin.Queries.GetAllRunType;
using Application.Handler.Admin.Queries.GetAllSchools;
using Application.Handler.Admin.Queries.GetAllSeasonFolder;
using Application.Handler.Admin.Queries.GetAreaList;
using Application.Handler.Admin.Queries.GetAreas;
using Application.Handler.Admin.Queries.GetBranch;
using Application.Handler.Admin.Queries.GetBuilding;
using Application.Handler.Admin.Queries.GetBuildingList;
using Application.Handler.Admin.Queries.GetBusCharge;
using Application.Handler.Admin.Queries.GetBusType;
using Application.Handler.Admin.Queries.GetCc;
using Application.Handler.Admin.Queries.GetChargeStructure;
using Application.Handler.Admin.Queries.GetDay;
using Application.Handler.Admin.Queries.GetDays;
using Application.Handler.Admin.Queries.GetDecductionAmount;
using Application.Handler.Admin.Queries.GetDistrict;
using Application.Handler.Admin.Queries.GetDriverType;
using Application.Handler.Admin.Queries.GetGender;
using Application.Handler.Admin.Queries.GetMessages;
using Application.Handler.Admin.Queries.GetNT;
using Application.Handler.Admin.Queries.GetPermissions;
using Application.Handler.Admin.Queries.GetPredefinedColors;
using Application.Handler.Admin.Queries.GetPredefinedSMSMessages;
using Application.Handler.Admin.Queries.GetRoles;
using Application.Handler.Admin.Queries.GetRouteTypeDays;
using Application.Handler.Admin.Queries.GetRunType;
using Application.Handler.Admin.Queries.GetSchool;
using Application.Handler.Admin.Queries.GetSchoolYears;
using Application.Handler.Admin.Queries.GetSearchLocation;
using Application.Handler.Admin.Queries.GetSeasonFolder;
using Application.Handler.Admin.Queries.GetStreetAndAreaMapped;
using Application.Handler.Admin.Queries.GetTrackingTime;
using Application.Handler.Bus.Queries.GetAllBusDetails;
using Application.Handler.SystemValues.Queries;
using Application.Handler.SystemValues.Queries.GetCities;
using Application.Handler.SystemValues.Queries.GetRouteTypeRequiredRules;
using Application.Handler.User.Command.DeleteUser;
using Application.Handler.User.Queries.GetUsers;
using DTO.Request.Admin;
using DTO.Request.SystemValues;
using DTO.Request.User;
using DTO.Response;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
   // [Authorize]
    [ApiController]
    public class AdminSettingsController : BaseController
    {
        #region Queries


        [HttpGet]
        [Route("GetBusCharge")]
        public async Task<IActionResult> GetBusCharge()
        {
            return Ok(await Mediator.Send(new GetBusChargeQuery()));
        }

        [HttpPost]
        [Route("GetPredefinedColorsDropdown")]
        public async Task<IActionResult> GetPredefinedColorsDropdown()
        {
            var result = await Mediator.Send(new GetPredefinedColorsDropdownQuery());
            return Ok(result);
        }

        [HttpPost]
        [Route("GetPredefinedColors")]
        public async Task<IActionResult> GetPredefinedColors([FromBody] ServerRowsRequest serverRowsRequest)
        {
            var result = await Mediator.Send(new GetPredefinedColorsQuery
            {
                CommonRequest = serverRowsRequest
            });

            return Ok(result);
        }

        [HttpPost]
        //[AllowAnonymous]
        [Route("GetStreetAndAreaMapped")]
        public async Task<IActionResult> GetStreetAndAreaMapped([FromBody] ServerRowsRequest serverRowsRequest)
        {
            var result = await Mediator.Send(new GetStreetAndAreaMappedQuery { CommonRequest = serverRowsRequest });
            return Ok(result);
        }

        [HttpPost]
        //[AllowAnonymous]
        [Route("GetAreas")]
        public async Task<IActionResult> GetAreas([FromBody] ServerRowsRequest serverRowsRequest)
        {
            var result = await Mediator.Send(new GetAreasQuery { CommonRequest = serverRowsRequest });
            return Ok(result);
        }

        [HttpPost]
        //[AllowAnonymous]
        [Route("GetSchoolYears")]
        public async Task<IActionResult> GetSchoolYears([FromBody] ServerRowsRequest serverRowsRequest)
        {
            var result = await Mediator.Send(new GetSchoolYearsQuery { CommonRequest = serverRowsRequest });
            return Ok(result);
        }

        [HttpPost]
        //[AllowAnonymous]
        [Route("GetGender")]
        public async Task<IActionResult> GetGender([FromBody] ServerRowsRequest serverRowsRequest)
        {
            var result = await Mediator.Send(new GetGenderQuery { CommonRequest = serverRowsRequest });
            return Ok(result);
        }

        [HttpPost]
        //[AllowAnonymous]
        [Route("GetDistrict")]
        public async Task<IActionResult> GetDistrict([FromBody] ServerRowsRequest serverRowsRequest)
        {
            var result = await Mediator.Send(new GetDistrictQuery { CommonRequest = serverRowsRequest });
            return Ok(result);
        }

        [HttpPost]
        //[AllowAnonymous]
        [Route("GetSchools")]
        public async Task<IActionResult> GetSchools([FromBody] ServerRowsRequest serverRowsRequest)
        {
            var result = await Mediator.Send(new GetSchoolQuery { CommonRequest = serverRowsRequest });
            return Ok(result);
        }

        [HttpPost]
        //[AllowAnonymous]
        [Route("GetBuilding")]
        public async Task<IActionResult> GetBuilding([FromBody] ServerRowsRequest serverRowsRequest)
        {
            var result = await Mediator.Send(new GetBuildingQuery { CommonRequest = serverRowsRequest });
            return Ok(result);
        }

        [HttpPost]
        //[AllowAnonymous]
        [Route("GetCc")]
        public async Task<IActionResult> GetCc([FromBody] ServerRowsRequest serverRowsRequest)
        {
            var result = await Mediator.Send(new GetCcQuery { CommonRequest = serverRowsRequest });
            return Ok(result);
        }

        [HttpPost]
        //[AllowAnonymous]
        [Route("GetChargeStructure")]
        public async Task<IActionResult> GetChargeStructure([FromBody] ServerRowsRequest serverRowsRequest)
        {
            var result = await Mediator.Send(new GetChargeStructureQuery { CommonRequest = serverRowsRequest });
            return Ok(result);
        }

        [HttpPost]
        //[AllowAnonymous]
        [Route("GetNT")]
        public async Task<IActionResult> GetNT([FromBody] ServerRowsRequest serverRowsRequest)
        {
            var result = await Mediator.Send(new GetNTQuery { CommonRequest = serverRowsRequest });
            return Ok(result);
        }

        [HttpPost]
        //[AllowAnonymous]
        [Route("GetRouteType")]
        public async Task<IActionResult> GetRouteType([FromBody] ServerRowsRequest serverRowsRequest)
        {
            var result = await Mediator.Send(new GetBusTypeQuery { CommonRequest = serverRowsRequest });
            return Ok(result);
        }

        [HttpPost]
        //[AllowAnonymous]
        [Route("GetRunType")]
        public async Task<IActionResult> GetRunType([FromBody] ServerRowsRequest serverRowsRequest)
        {
            var result = await Mediator.Send(new GetRunTypeQuery { CommonRequest = serverRowsRequest });
            return Ok(result);
        }

        [HttpPost]
        //[AllowAnonymous]
        [Route("GetDriverType")]
        public async Task<IActionResult> GetDriverType([FromBody] ServerRowsRequest serverRowsRequest)
        {
            var result = await Mediator.Send(new GetDriverTypeQuery { CommonRequest = serverRowsRequest });
            return Ok(result);
        }

        [HttpPost]
        //[AllowAnonymous]
        [Route("GetDeductionAmount")]
        public async Task<IActionResult> GetDecductionAmount([FromBody] ServerRowsRequest serverRowsRequest)
        {
            var result = await Mediator.Send(new GetDecductionAmountQuery { CommonRequest = serverRowsRequest });
            return Ok(result);
        }

        [HttpPost]
        //[AllowAnonymous]
        [Route("GetSearchLocation")]
        public async Task<IActionResult> GetSearchLocation([FromBody] ServerRowsRequest serverRowsRequest)
        {
            var result = await Mediator.Send(new GetSearchLocationQuery { CommonRequest = serverRowsRequest });
            return Ok(result);
        }

        [HttpPost]
        //[AllowAnonymous]
        [Route("GetSeasonFolder")]
        public async Task<IActionResult> GetSeasonFolder([FromBody] ServerRowsRequest serverRowsRequest)
        {
            var result = await Mediator.Send(new GetSeasonFolderQuery { CommonRequest = serverRowsRequest });
            return Ok(result);
        }

        [HttpPost]
        //[AllowAnonymous]
        [Route("GetUesrsWithRoles")]
        public async Task<IActionResult> GetUsers([FromBody] ServerRowsRequest serverRowsRequest)
        {
            var result = await Mediator.Send(new GetUsersQuery { CommonRequest = serverRowsRequest });
            return Ok(result);
        }

        [HttpPost]
        //[AllowAnonymous]
        [Route("GetRoles")]
        public async Task<IActionResult> GetRoles()
        {
            var result = await Mediator.Send(new GetRolesQuery());
            return Ok(result);
        }

        [HttpGet]
        //[AllowAnonymous]
        [Route("GetPermissions")]
        public async Task<IActionResult> GetPermissions([FromQuery] PermissionsByRoleIdDto permissionsByRoleIdDto)
        {
            var query = permissionsByRoleIdDto.Adapt<GetPermissionsByRoleIdQuery>();
            var permissions = await Mediator.Send(query);
            return Ok(permissions);
        }

        [HttpGet]
        //[AllowAnonymous]
        [Route("GetAreaList")]
        public async Task<IActionResult> GetAreaList()
        {
            var result = await Mediator.Send(new GetAreaListQuery());
            return Ok(result);
        }

        [HttpPost]
        //[AllowAnonymous]
        [Route("GetAllDistricts")]
        public async Task<IActionResult> GetAllDistricts()
        {
            var result = await Mediator.Send(new GetAllDistrictsQuery());
            return Ok(result);
        }

        [HttpPost]
        //[AllowAnonymous]
        [Route("GetTrackingTime")]
        public async Task<IActionResult> GetTrackingTime()
        {
            var result = await Mediator.Send(new GetTrackingTimeQuery());
            return Ok(result);
        }

        [HttpPost]
        //[AllowAnonymous]
        [Route("GetAllNT")]
        public async Task<IActionResult> GetAllNT()
        {
            var result = await Mediator.Send(new GetAllNTQuery());
            return Ok(result);
        }

        [HttpPost]
        //[AllowAnonymous]
        [Route("GetAllSchools")]
        public async Task<IActionResult> GetAllSchools()
        {
            var result = await Mediator.Send(new GetAllSchoolsQuery());
            return Ok(result);
        }

        [HttpPost]
        //[AllowAnonymous]
        [Route("GetAllGenders")]
        public async Task<IActionResult> GetAllGenders()
        {
            var result = await Mediator.Send(new GetAllGendersQuery());
            return Ok(result);
        }

        [HttpPost]
        //[AllowAnonymous]
        [Route("GetAllRouteType")]
        public async Task<IActionResult> GetAllRouteType()
        {
            var result = await Mediator.Send(new GetAllBusTypeQuery());
            return Ok(result);
        }

        [HttpPost]
        //[AllowAnonymous]
        [Route("GetAllRunType")]
        public async Task<IActionResult> GetAllRunType()
        {
            var result = await Mediator.Send(new GetAllRunTypeQuery());
            return Ok(result);
        }



        [HttpPost]
        //[AllowAnonymous]
        [Route("GetBuildingList")]
        public async Task<IActionResult> GetBuildingList()
        {
            var result = await Mediator.Send(new GetBuildingListQuery());
            return Ok(result);
        }

        [HttpPost]
        //[AllowAnonymous]
        [Route("GetAllSeasonFolder")]
        public async Task<IActionResult> GetAllSeasonFolder()
        {
            var result = await Mediator.Send(new GetAllSeasonFolderQuery());
            return Ok(result);
        }


        [HttpPost]
        //[AllowAnonymous]
        [Route("GetStates")]
        public async Task<IActionResult> GetStates([FromBody] GetStatesRequestDto getStudentsRequestDto)
        {

            var result = await Mediator.Send(getStudentsRequestDto.Adapt<GetStatesQuery>());
            return Ok(result);
        }


        [HttpPost]
        //[AllowAnonymous]
        [Route("GetCities")]
        public async Task<IActionResult> GetCities([FromBody] GetCitiesRequestDto getCitiesRequestDto)
        {
            var result = await Mediator.Send(getCitiesRequestDto.Adapt<GetCitiesQuery>());
            return Ok(result);
        }

        [HttpPost]
        //[AllowAnonymous]
        [Route("GetBranch")]
        public async Task<IActionResult> GetBranch([FromBody] ServerRowsRequest serverRowsRequest)
        {
            var result = await Mediator.Send(new GetBranchQuery { CommonRequest = serverRowsRequest });
            return Ok(result);
        }

        [HttpPost]
        //[AllowAnonymous]
        [Route("GetAllGrade")]
        public async Task<IActionResult> GetAllGrade([FromBody] ServerRowsRequest serverRowsRequest)
        {
            var result = await Mediator.Send(new GetAllGradeQuery { CommonRequest = serverRowsRequest });
            return Ok(result);
        }
        [HttpPost]
        //[AllowAnonymous]
        [Route("GetAllFamilyDetail")]
        public async Task<IActionResult> GetAllFamilyDetail([FromBody] ServerRowsRequest serverRowsRequest)
        {
            var result = await Mediator.Send(new GetAllFamilyDetailQuery { CommonRequest = serverRowsRequest });
            return Ok(result);
        }

        [HttpPost]
        //[AllowAnonymous]
        [Route("GetRouteTypeRequiredRules")]
        public async Task<IActionResult> GetRouteTypeRequiredRules([FromBody] GetRouteTypeRequiredRulesDto getRouteTypeRequiredRulesDto)
        {
            var result = await Mediator.Send(getRouteTypeRequiredRulesDto.Adapt<GetRouteTypeRequiredRulesQuery>());
            return Ok(result);
        }

        [HttpPost]
        [Route("ExportFamilyList")]
        [AllowAnonymous]
        public async Task<IActionResult> ExportFamilyList()
        {
            var result = await Mediator.Send(new ExportFamilyListQuery());

            return File(result.FileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                result.FileName
            );
        }

        [HttpPost]
        [Route("GetMessages")]
        public async Task<IActionResult> GetMessages([FromBody] ServerRowsRequest serverRowsRequest)
        {
            var result = await Mediator.Send(new GetMessagesQuery { CommonRequest = serverRowsRequest });
            return Ok(result);
        }

        [HttpGet]
        [Route("GetPredefinedSMSMessages")]
        public async Task<IActionResult> GetPredefinedSMSMessages()
        {
            var result = await Mediator.Send(new GetPredefinedSMSMessagesQuery());
            return Ok(result);
        }

        [HttpGet]
        [Route("GetDay")]
        public async Task<IActionResult> GetDay()
        {
            var result = await Mediator.Send(new GetDayQuery());
            return Ok(result);
        }

        [HttpPost]
        [Route("GetDays")]
        public async Task<IActionResult> GetDays([FromBody] ServerRowsRequest serverRowsRequest)
        {
            var result = await Mediator.Send(new GetDaysQuery { CommonRequest = serverRowsRequest });
            return Ok(result);

        }
        
        [HttpGet]
        [Route("GetRouteTypeDays")]
            public async Task<IActionResult> GetRouteTypeDays([FromQuery] int routeTypeId)
            {
                return Ok(await Mediator.Send(new GetRouteTypeDaysQuery
                {
                    RouteTypeId = routeTypeId
                }));
            }
            #endregion Queries

            #region Commands
            [HttpPost]
        //[AllowAnonymous]
        [Route("UpdatePermissionByRoleId")]
        public async Task<IActionResult> UpdatePermissionByRoleId([FromBody] UpdatePermissionByRoleIdRequestDto updatePermissionByRoleIdRequestDto) => Ok(await Mediator.Send(updatePermissionByRoleIdRequestDto.Adapt<UpdatePermissionByRoleIdCommand>()));

        [HttpPost]
        //[AllowAnonymous]
        [Route("AddRole")]
        public async Task<IActionResult> AddRole([FromBody] AddRoleDto addRoleRequestDto) => Ok(await Mediator.Send(addRoleRequestDto.Adapt<AddRoleCommand>()));

        [HttpPost]
        //[AllowAnonymous]
        [Route("DeleteUser")]
        public async Task<IActionResult> DeleteUser([FromQuery] DeleteUserRequestDto deleteUserRequestDto)
        {
            CommonResultResponseDto<string> result = await Mediator.Send(deleteUserRequestDto.Adapt<DeleteUserCommand>());
            return Ok(result);
        }

        [HttpPost]
        //[AllowAnonymous]
        [Route("AddStreetsAreaMapping")]
        public async Task<IActionResult> AddStreetsAreaMapping([FromBody] AddStreetsAreaMappingDto addStreetsAreaMappingDto) => Ok(await Mediator.Send(addStreetsAreaMappingDto.Adapt<AddStreetsAreaMappingCommand>()));

        [HttpPost]
        //[AllowAnonymous]
        [Route("AddAreas")]
        public async Task<IActionResult> AddAreas([FromBody] AddAreasDto addAreasDto) => Ok(await Mediator.Send(addAreasDto.Adapt<AddAreasCommand>()));

        [HttpPost]
        //[AllowAnonymous]
        [Route("AddSchoolYears")]
        public async Task<IActionResult> AddSchoolYears([FromBody] AddSchoolYearsDto schoolYearsDto) => Ok(await Mediator.Send(schoolYearsDto.Adapt<AddSchoolYearsCommand>()));

        [HttpPost]
        //[AllowAnonymous]
        [Route("AddGender")]
        public async Task<IActionResult> AddGender([FromBody] AddGenderDto addGenderDto) => Ok(await Mediator.Send(addGenderDto.Adapt<AddGenderCommand>()));

        [HttpPost]
        //[AllowAnonymous]
        [Route("AddDistrict")]
        public async Task<IActionResult> AddDistrict([FromBody] AddDistrictDto addDistrictDto) => Ok(await Mediator.Send(addDistrictDto.Adapt<AddDistrictCommand>()));

        [HttpPost]
        //[AllowAnonymous]
        [Route("AddSchool")]
        public async Task<IActionResult> AddSchool([FromBody] AddSchoolDto addSchoolDto) => Ok(await Mediator.Send(addSchoolDto.Adapt<AddSchoolCommand>()));

        [HttpPost]
        //[AllowAnonymous]
        [Route("AddBuilding")]
        public async Task<IActionResult> AddBuilding([FromBody] AddBuildingDto addBuildingDto) => Ok(await Mediator.Send(addBuildingDto.Adapt<AddBuildingCommand>()));

        [HttpPost]
        //[AllowAnonymous]
        [Route("AddCC")]
        public async Task<IActionResult> AddCC([FromBody] AddCCDto addCCDto) => Ok(await Mediator.Send(addCCDto.Adapt<AddCCCommand>()));

        [HttpPost]
        //[AllowAnonymous]
        [Route("AddChargeStructure")]
        public async Task<IActionResult> AddChargeStructure([FromBody] AddChargeStructureDto addChargeStructureDto) => Ok(await Mediator.Send(addChargeStructureDto.Adapt<AddChargeStructureCommand>()));

        [HttpPost]
        //[AllowAnonymous]
        [Route("AddNT")]
        public async Task<IActionResult> AddNT([FromBody] AddNTDto addNTDto) => Ok(await Mediator.Send(addNTDto.Adapt<AddNTCommand>()));

        [HttpPost]
        //[AllowAnonymous]
        [Route("AddRouteType")]
        public async Task<IActionResult> AddRouteType([FromBody] AddBusTypeRequestDto addNTDto) => Ok(await Mediator.Send(addNTDto.Adapt<AddBusTypeCommand>()));

        [HttpPost]
        //[AllowAnonymous]
        [Route("AddRunType")]
        public async Task<IActionResult> AddRunType([FromBody] AddRunTypeRequestDto addRunTypeRequestDto) => Ok(await Mediator.Send(addRunTypeRequestDto.Adapt<AddRunTypeCommand>()));

        [HttpPost]
        //[AllowAnonymous]
        [Route("UpdateFamilyTracking")]
        public async Task<IActionResult> UpdateFamilyTracking([FromBody] UpdateFamilyTrackingRequestDto updateFamilyTrackingRequestDto) => Ok(await Mediator.Send(updateFamilyTrackingRequestDto.Adapt<UpdateFamilyTrackingCommand>()));

        [HttpPost]
        //[AllowAnonymous]
        [Route("AddDriverType")]
        public async Task<IActionResult> AddDriverType([FromBody] AddDriverTypeRequestDto addNTDto) => Ok(await Mediator.Send(addNTDto.Adapt<AddDriverTypeCommand>()));

        [HttpPost]
        //[AllowAnonymous]
        [Route("AddDeductionAmount")]
        public async Task<IActionResult> AddDeductionAmount([FromBody] AddDeductionAmountRequestDto addDeductionAmountRequestDto) => Ok(await Mediator.Send(addDeductionAmountRequestDto.Adapt<AddDeductionAmountCommand>())); [HttpPost]

        [HttpPost]
        //[AllowAnonymous]
        [Route("AddSearchLocation")]
        public async Task<IActionResult> AddSearchLocation([FromBody] AddSearchLocationRequestDto addSearchLocationRequestDto) => Ok(await Mediator.Send(addSearchLocationRequestDto.Adapt<AddSearchLocationCommand>()));

        [HttpPost]
        //[AllowAnonymous]
        [Route("UpdateTrackingTime")]
        public async Task<IActionResult> UpdateTrackingTime([FromBody] UpdateTrackingTimeRequestDto updateTrackingTimeRequestDto) => Ok(await Mediator.Send(updateTrackingTimeRequestDto.Adapt<UpdateTrackingTimeCommand>()));

        [HttpPost]
        //[AllowAnonymous]
        [Route("AddUpdateBranch")]
        public async Task<IActionResult> AddUpdateBranch([FromBody] AddUpdateBranchRequestDto addUpdateBranchRequestDto) => Ok(await Mediator.Send(addUpdateBranchRequestDto.Adapt<AddUpdateBranchCommand>()));

        [HttpPost]
        //[AllowAnonymous]
        [Route("UpdateChargeStructure")]
        public async Task<IActionResult> updateChargeStructure([FromBody] UpdateChargeStructureRequestDto updateChargeStructureRequestDto)
        {
            CommonResultResponseDto<string> result = await Mediator.Send(updateChargeStructureRequestDto.Adapt<UpdateChargeStructureCommand>());
            return Ok(result);
        }

        [HttpPost]
        //[AllowAnonymous]
        [Route("AddUpdateGrade")]
        public async Task<IActionResult> AddUpdateGrade([FromBody] AddUpdateGradeRequestDto addUpdateGradeRequestDto) => Ok(await Mediator.Send(addUpdateGradeRequestDto.Adapt<AddUpdateGradeCommand>()));

        [HttpPost]
        //[AllowAnonymous]
        [Route("AddSeasonFolder")]
        public async Task<IActionResult> AddSeasonFolder([FromBody] AddSeasonFolderRequestDto addSeasonFolderRequestDto) => Ok(await Mediator.Send(addSeasonFolderRequestDto.Adapt<AddSeasonFolderCommand>()));


        [HttpPost]
        //[AllowAnonymous]
        [Route("DeleteStreetAreaMapping")]
        public async Task<IActionResult> DeleteStreetAreaMapping([FromQuery] DeleteStreetAreaMappingRequestDto deleteStreetAreaMappingRequestDto)
        {
            CommonResultResponseDto<string> result = await Mediator.Send(deleteStreetAreaMappingRequestDto.Adapt<DeleteStreetAreaMappingCommand>());
            return Ok(result);
        }

        [HttpPost]
        //[AllowAnonymous]
        [Route("DeleteAreas")]
        public async Task<IActionResult> DeleteAreas([FromQuery] DeleteAreasRequestDto DeleteAreasRequestDto)
        {
            CommonResultResponseDto<string> result = await Mediator.Send(DeleteAreasRequestDto.Adapt<DeleteAreasCommand>());
            return Ok(result);
        }

        [HttpPost]
        //[AllowAnonymous]
        [Route("DeleteSchoolYears")]
        public async Task<IActionResult> DeleteSchoolYears([FromQuery] DeleteSchoolYearsRequestDto deleteSchoolYearsRequestDto)
        {
            CommonResultResponseDto<string> result = await Mediator.Send(deleteSchoolYearsRequestDto.Adapt<DeleteSchoolYearsCommand>());
            return Ok(result);
        }

        [HttpPost]
        //[AllowAnonymous]
        [Route("DeleteGender")]
        public async Task<IActionResult> DeleteGender([FromQuery] DeleteGenderRequestDto deleteGenderRequestDto)
        {
            CommonResultResponseDto<string> result = await Mediator.Send(deleteGenderRequestDto.Adapt<DeleteGenderCommand>());
            return Ok(result);
        }

        [HttpPost]
        //[AllowAnonymous]
        [Route("DeleteDistrict")]
        public async Task<IActionResult> DeleteDistrict([FromQuery] DeleteDistrictRequestDto deleteDistrictRequestDto)
        {
            CommonResultResponseDto<string> result = await Mediator.Send(deleteDistrictRequestDto.Adapt<DeleteDistrictCommand>());
            return Ok(result);
        }

        [HttpPost]
        //[AllowAnonymous]
        [Route("DeleteSchools")]
        public async Task<IActionResult> DeleteSchools([FromQuery] DeleteSchoolsRequestDto deleteSchoolsRequestDto)
        {
            CommonResultResponseDto<string> result = await Mediator.Send(deleteSchoolsRequestDto.Adapt<DeleteSchoolsCommand>());
            return Ok(result);
        }

        [HttpPost]
        //[AllowAnonymous]
        [Route("DeleteBuilding")]
        public async Task<IActionResult> DeleteBuilding([FromQuery] DeleteBuildingRequestDto deleteBuildingRequestDto)
        {
            CommonResultResponseDto<string> result = await Mediator.Send(deleteBuildingRequestDto.Adapt<DeleteBuildingCommand>());
            return Ok(result);
        }

        [HttpPost]
        //[AllowAnonymous]
        [Route("DeleteCC")]
        public async Task<IActionResult> DeleteCC([FromQuery] DeleteCCRequestDto DeleteCCRequestDto)
        {
            CommonResultResponseDto<string> result = await Mediator.Send(DeleteCCRequestDto.Adapt<DeleteCCCommand>());
            return Ok(result);
        }

        [HttpPost]
        //[AllowAnonymous]
        [Route("DeleteChargeStructure")]
        public async Task<IActionResult> DeleteChargeStructure([FromQuery] DeleteChargeStructureRequestDto deleteChargeStructureRequestDto)
        {
            CommonResultResponseDto<string> result = await Mediator.Send(deleteChargeStructureRequestDto.Adapt<DeleteChargeStructureCommand>());
            return Ok(result);
        }

        [HttpPost]
        //[AllowAnonymous]
        [Route("DeleteNt")]
        public async Task<IActionResult> DeleteNt([FromQuery] DeleteNtRequestDto deleteNtRequestDto)
        {
            CommonResultResponseDto<string> result = await Mediator.Send(deleteNtRequestDto.Adapt<DeleteNtCommand>());
            return Ok(result);
        }

        [HttpPost]
        //[AllowAnonymous]
        [Route("DeleteRouteType")]
        public async Task<IActionResult> DeleteRouteType([FromQuery] DeleteBusTypeRequestDto deleteBusTypeRequestDto)
        {
            CommonResultResponseDto<string> result = await Mediator.Send(deleteBusTypeRequestDto.Adapt<DeleteBusTypeCommand>());
            return Ok(result);
        }

        [HttpPost]
        //[AllowAnonymous]
        [Route("DeleteRunType")]
        public async Task<IActionResult> DeleteRunType([FromQuery] DeleteRunTypeRequestDto deleteBusTypeRequestDto)
        {
            CommonResultResponseDto<string> result = await Mediator.Send(deleteBusTypeRequestDto.Adapt<DeleteRunTypeCommand>());
            return Ok(result);
        }

        [HttpPost]
        //[AllowAnonymous]
        [Route("DeleteDriverType")]
        public async Task<IActionResult> DeleteDriverType([FromQuery] DeleteDriverTypeRequestDto deleteDriverTypeRequestDto)
        {
            CommonResultResponseDto<string> result = await Mediator.Send(deleteDriverTypeRequestDto.Adapt<DeleteDriverTypeCommand>());
            return Ok(result);
        }
        [HttpPost]
        //[AllowAnonymous]
        [Route("DeleteBranch")]
        public async Task<IActionResult> DeleteBranch([FromQuery] DeleteBranchRequestDto deleteBranchRequestDto)
        {
            CommonResultResponseDto<string> result = await Mediator.Send(deleteBranchRequestDto.Adapt<DeleteBranchCommand>());
            return Ok(result);
        }

        [HttpPost]
        //[AllowAnonymous]
        [Route("DeleteGrade")]
        public async Task<IActionResult> DeleteGrade([FromQuery] DeleteGradeRequestDto deleteGradeRequestDto)
        {
            CommonResultResponseDto<string> result = await Mediator.Send(deleteGradeRequestDto.Adapt<DeleteGradeCommand>());
            return Ok(result);
        }

        [HttpPost]
        //[AllowAnonymous]
        [Route("DeleteSeasonFolder")]
        public async Task<IActionResult> DeleteSeasonFolder([FromQuery] DeleteSeasonFolderRequestDto deleteGradeRequestDto)
        {
            CommonResultResponseDto<string> result = await Mediator.Send(deleteGradeRequestDto.Adapt<DeleteSeasonFolderCommand>());
            return Ok(result);
        }

        [HttpPost]
        //[AllowAnonymous]
        [Route("UpdateFamilyDetail")]
        public async Task<IActionResult> UpdateFamilyDetail([FromBody] UpdateFamilyDetailRequestDto updateFamilyDetailRequestDto) => Ok(await Mediator.Send(updateFamilyDetailRequestDto.Adapt<UpdateFamilyDetailCommand>()));


        [HttpPost]
        [Route("UpdateProfile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileRequestDto updateProfileRequestDto) => Ok(await Mediator.Send(updateProfileRequestDto.Adapt<UpdateProfileRequestDtoCommand>()));

        [HttpPost]
        [Route("ImportKml")]
        //[AllowAnonymous]
        public async Task<IActionResult> SaveImportedKml([FromForm] ImportKmlCommand importKmlCommand) => Ok(await Mediator.Send(importKmlCommand));

        [HttpPost]
        //[AllowAnonymous]
        [Route("UpdateGradeMapping")]
        public async Task<IActionResult> UpdateGradeMapping([FromBody] UpdateGradeMappingDto updateGradeMappingDto) => Ok(await Mediator.Send(updateGradeMappingDto.Adapt<UpdateGradeMappingCommand>()));


        [HttpPost]
        //[AllowAnonymous]
        [Route("UpdateBulkGrade")]
        public async Task<IActionResult> UpdateBulkGrade([FromBody] UpdateBulkGradeDto updateBulkGradeDto) => Ok(await Mediator.Send(updateBulkGradeDto.Adapt<UpdateBulkGradeCommand>()));

        [HttpPost]
        //[AllowAnonymous]
        [Route("SaveRouteTypeRequiredRules")]
        public async Task<IActionResult> SaveRouteTypeRequiredRules([FromBody] RouteTypeRequiredRulesDto routeTypeRequiredRulesDto) => Ok(await Mediator.Send(routeTypeRequiredRulesDto.Adapt<RouteTypeRequiredRulesCommand>()));

        [HttpPost]
        [Route("AddMessage")]
        public async Task<IActionResult> AddMessage([FromBody] AddMessageRequestDto request)
        {
            return Ok(await Mediator.Send(request.Adapt<AddMessagesCommand>()));
        }

        [HttpPost]
        [Route("DeleteMessage")]
        public async Task<IActionResult> DeleteMessage([FromQuery] DeleteMessageRequestDto request)
        {
            return Ok(await Mediator.Send(request.Adapt<DeleteMessageCommand>()));
        }

        [HttpPost]
        [Route("UpdateDayStatus")]
        public async Task<IActionResult> UpdateDayStatus([FromBody] UpdateDayStatusCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
        [HttpPost]
        [Route("UpdateRouteTypeExclusivePay")]
        public async Task<IActionResult> UpdateRouteTypeExclusivePay([FromBody] UpdateRouteTypeExclusivePayRequestDto request)
        {
            return Ok(await Mediator.Send(
                request.Adapt<UpdateRouteTypeExclusivePayCommand>()));
        }

        [HttpPost]
        [Route("AddUpdatePredefinedColor")]
        public async Task<IActionResult> AddUpdatePredefinedColor([FromBody] AddUpdatePredefinedColorDto request)
        {
            return Ok(await Mediator.Send(request.Adapt<AddUpdatePredefinedColorCommand>()));
        }

        [HttpPost]
        [Route("DeletePredefinedColor")]
        public async Task<IActionResult> DeletePredefinedColor([FromQuery] DeletePredefinedColorRequestDto request)
        {
            return Ok(await Mediator.Send(request.Adapt<DeletePredefinedColorCommand>()));
        }

       
        [HttpPost]
        [Route("UpdateBusCharge")]
        public async Task<IActionResult> UpdateBusCharge([FromBody] UpdateBusChargeRequestDto request)
        {
            return Ok(await Mediator.Send(request.Adapt<UpdateBusChargeCommand>()));
        }
        #endregion Commands
    }
}

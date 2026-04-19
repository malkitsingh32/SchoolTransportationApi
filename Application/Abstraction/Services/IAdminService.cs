using Application.Common.Dtos;
using Application.Common.Response;
using Application.Handler.Admin.Command.ImportKml;
using Application.Handler.Students.Queries.ExportStudentsList;
using DTO.Request.Admin;
using DTO.Response;
using DTO.Response.Admin;
using DTO.Response.SystemValues;
using DTO.Response.User;

namespace Application.Abstraction.Services
{
    public interface IAdminService
    {
        Task<CommonResultResponseDto<PaginatedList<GetStreetAndAreaMappedResponseDto>>> GetStreetAndAreaMapped(string filterModel, ServerRowsRequest commonRequest, string getSort);
        Task<CommonResultResponseDto<PaginatedList<GetAreasResponseDto>>> GetAreas(string filterModel, ServerRowsRequest commonRequest, string getSort);
        Task<CommonResultResponseDto<PaginatedList<GetGenderResponseDto>>> GetGender(string filterModel, ServerRowsRequest commonRequest, string getSort);
        Task<CommonResultResponseDto<PaginatedList<GetSchoolYearsResponseDto>>> GetSchoolYears(string filterModel, ServerRowsRequest commonRequest, string getSort);
        Task<CommonResultResponseDto<PaginatedList<GetDistrictResponseDto>>> GetDistrict(string filterModel, ServerRowsRequest commonRequest, string getSort);
        Task<CommonResultResponseDto<PaginatedList<GetSchoolsResponseDto>>> GetSchools(string filterModel, ServerRowsRequest commonRequest, string getSort);
        Task<CommonResultResponseDto<PaginatedList<GetBuildingResponseDto>>> GetBuilding(string filterModel, ServerRowsRequest commonRequest, string getSort);
        Task<CommonResultResponseDto<PaginatedList<GetCcResponseDto>>> GetCc(string filterModel, ServerRowsRequest commonRequest, string getSort);
        Task<CommonResultResponseDto<PaginatedList<GetChargeStructureResponseDto>>> GetChargeStructure(string filterModel, ServerRowsRequest commonRequest, string getSort);
        Task<CommonResultResponseDto<PaginatedList<GetNTResponseDto>>> GetNT(string filterModel, ServerRowsRequest commonRequest, string getSort);
        Task<CommonResultResponseDto<PaginatedList<GetBusTypeResponseDto>>> GetBusType(string filterModel, ServerRowsRequest commonRequest, string getSort);
        Task<CommonResultResponseDto<PaginatedList<GetRunTypeResponseDto>>> GetRunType(string filterModel, ServerRowsRequest commonRequest, string getSort);
        Task<CommonResultResponseDto<PaginatedList<GetDriverTypeResponseDto>>> GetDriverType(string filterModel, ServerRowsRequest commonRequest, string getSort);
        Task<CommonResultResponseDto<PaginatedList<GetDecductionAmountResponseDto>>> GetDecductionAmount(string filterModel, ServerRowsRequest commonRequest, string getSort);
        Task<CommonResultResponseDto<PaginatedList<GetSearchLocationResponseDto>>> GetSearchLocation(string filterModel, ServerRowsRequest commonRequest, string getSort);
        Task<CommonResultResponseDto<PaginatedList<GetSeasonFolderResponseDto>>> GetSeasonFolder(string filterModel, ServerRowsRequest commonRequest, string getSort);
        Task<CommonResultResponseDto<IList<GetAllRolesResponseDto>>> GetRoles();
        Task<CommonResultResponseDto<IList<GetPredefinedSMSMessagesResponseDto>>> GetPredefinedSMSMessages();
        Task<CommonResultResponseDto<List<Permissions>>> GetPermissionsByRoleId(int roleId);
        Task<CommonResultResponseDto<PaginatedList<GetBranchResponseDto>>> GetBranch(string filterModel, ServerRowsRequest commonRequest, string getSort);
        Task<CommonResultResponseDto<PaginatedList<GetAllGradeResponseDto>>> GetAllGrade(string filterModel, ServerRowsRequest commonRequest, string getSort);
        Task<CommonResultResponseDto<string>> UpdatePermissionByRoleId(int permissionId, int roleId, string permissionType, bool canView, bool canEdit);
        Task<CommonResultResponseDto<string>> AddRole(string roleName);
        Task<CommonResultResponseDto<string>> AddStreetsAreaMapping(int id, string StreetName, int AreaId, int userId, int districtId);
        Task<CommonResultResponseDto<string>> AddAreas(int id, string areaName, int userId, string shortName);
        Task<CommonResultResponseDto<string>> AddSchoolYears(int id, int schoolYear, int numberOfStudents, int userId);
        Task<CommonResultResponseDto<string>> AddGender(int id, string gender, int userId);
        Task<CommonResultResponseDto<string>> AddDistrict(int id, string districtName, int userId);
        Task<CommonResultResponseDto<string>> AddSchool(int id, string schoolName, string legalName, int userId);
        Task<CommonResultResponseDto<string>> AddBuilding(int id, string address, int schoolId, int userId, string buildingName);
        Task<CommonResultResponseDto<string>> AddCC(int id, int cardnoxId, int familyId, int userId);
        Task<CommonResultResponseDto<string>> AddChargeStructure(int id, int DistrictId, int? NtId, bool IsFunded, int userId, decimal price, int? SchoolId);
        Task<CommonResultResponseDto<string>> AddNT(int id, string nTName, int userId);
        Task<CommonResultResponseDto<string>> AddBusType(AddBusTypeRequestDto addBusTypeRequestDto);
        Task<CommonResultResponseDto<string>> AddRunType(AddRunTypeRequestDto addRunTypeRequestDto);
        Task<CommonResultResponseDto<string>> UpdateFamilyTracking(UpdateFamilyTrackingRequestDto updateFamilyTrackingRequestDto);
        Task<CommonResultResponseDto<string>> AddDriverType(int id, string driverTypeName, decimal payRate, int userId);
        Task<CommonResultResponseDto<string>> AddDeductionAmount(int id, decimal amount, int userId);
        Task<CommonResultResponseDto<string>> AddSearchLocation(int id, string currentLacation, string currentLacationLongLat, int userId);
        Task<CommonResultResponseDto<string>> UpdateTrackingTime(UpdateTrackingTimeRequestDto updateTrackingTimeRequestDto);
        Task<CommonResultResponseDto<string>> AddUpdateBranch(AddUpdateBranchRequestDto addUpdateBranchRequestDto);
        Task<CommonResultResponseDto<string>> AddUpdateGrade(AddUpdateGradeRequestDto addUpdateGradeRequestDto);
        Task<CommonResultResponseDto<string>> AddSeasonFolder(AddSeasonFolderRequestDto addSeasonFolderRequestDto);
        Task<CommonResultResponseDto<IList<GetAllDistrictsResponseDto>>> GetAllDistricts();
        Task<CommonResultResponseDto<GetTrackingTimeQueryResponseDto>> GetTrackingTime();
        Task<CommonResultResponseDto<IList<GetAllNTResponseDto>>> GetAllNT();
        Task<CommonResultResponseDto<IList<GetAllSchoolsResponseDto>>> GetAllSchools();
        Task<CommonResultResponseDto<string>> UpdateChargeStructure(int id, bool isFunded);
        Task<CommonResultResponseDto<IList<GetAreaListResponseDto>>> GetAreaList();
        Task<CommonResultResponseDto<string>> DeleteStreetAreaMapping(int id);
        Task<CommonResultResponseDto<string>> DeleteArea(int id, bool isDelete);
        Task<CommonResultResponseDto<string>> DeleteSchoolYears(int id);
        Task<CommonResultResponseDto<string>> DeleteGender(int id, bool isDelete);
        Task<CommonResultResponseDto<string>> DeleteDistrict(int id, bool isDelete);
        Task<CommonResultResponseDto<string>> DeleteSchools(int id, bool isDelete);
        Task<CommonResultResponseDto<string>> DeleteBuilding(int id, bool isDelete);
        Task<CommonResultResponseDto<string>> DeleteCC(int id);
        Task<CommonResultResponseDto<string>> DeleteChargeStructure(int id);
        Task<CommonResultResponseDto<string>> DeleteNt(int id, bool isDelete);
        Task<CommonResultResponseDto<string>> DeleteRunType(DeleteRunTypeRequestDto deleteRunTypeRequestDto);
        Task<CommonResultResponseDto<string>> DeleteBusType(int id, bool isDelete);
        Task<CommonResultResponseDto<string>> DeleteDriverType(int id, bool isDelete);
        Task<CommonResultResponseDto<string>> DeleteBranch(int id, bool isDelete);
        Task<CommonResultResponseDto<string>> DeleteGrade(DeleteGradeRequestDto deleteGradeRequestDto);
        Task<CommonResultResponseDto<string>> DeleteSeasonFolder(DeleteSeasonFolderRequestDto deleteSeasonFolderRequestDto);

        Task<CommonResultResponseDto<IList<GetAllGendersResponseDto>>> GetAllGenders();
        Task<CommonResultResponseDto<IList<GetAllBusTypeResponseDto>>> GetAllBusType();
        Task<CommonResultResponseDto<IList<GetAllRunTypeResponseDto>>> GetAllRunType();
        Task<CommonResultResponseDto<IList<GetBuildingListResponseDto>>> GetBuildingList();
        Task<CommonResultResponseDto<IList<GetSeasonFolderResponseDto>>> GetAllSeasonFolder();
        Task<CommonResultResponseDto<IList<GetStatesResponseDto>>> GetStates(int id);
        Task<CommonResultResponseDto<IList<GetCitiesResponseDto>>> GetCities(int id);
        Task<CommonResultResponseDto<IList<GetRouteTypeRequiredRulesResponseDto>>> GetRouteTypeRequiredRules(int? routeTypeId);
        Task<CommonResultResponseDto<string>> AddLogs(string message, string from, string messageType);
        Task<CommonResultResponseDto<PaginatedList<GetAllFamilyDetailResponseDto>>> GetAllFamilyDetail(string filterModel, ServerRowsRequest commonRequest, string getSort);
        Task<CommonResultResponseDto<string>> UpdateFamilyDetail(UpdateFamilyDetailRequestDto updateFamilyDetailRequestDto);
        Task<CommonResultResponseDto<string>> UpdateProfile(UpdateProfileRequestDto UpdateProfileRequestDto);
        Task<CommonResultResponseDto<string>> ImportKml(ImportKmlCommand importKmlCommand);
        Task<CommonResultResponseDto<string>> UpdateGradeMapping(UpdateGradeMappingDto updateGradeMappingDto);
        Task<CommonResultResponseDto<string>> UpdateBulkGrade(UpdateBulkGradeDto updateBulkGradeDto);
        Task<CommonResultResponseDto<string>> SetRouteTypeRequiredRules(RouteTypeRequiredRulesDto routeTypeRequiredRulesDto);
        Task<ExportFileResult> ExportFamilyList();
        Task<CommonResultResponseDto<PaginatedList<GetMessageResponseDto>>> GetMessages(string filterModel, ServerRowsRequest commonRequest, string getSort);
        Task<CommonResultResponseDto<string>> AddMessage(AddMessageRequestDto request);
        Task<CommonResultResponseDto<string>> DeleteMessage(DeleteMessageRequestDto request);
        Task<List<GetDaysResponseDto>> GetDay();
        Task<CommonResultResponseDto<PaginatedList<GetDaysResponseDto>>> GetDays(string filterModel, ServerRowsRequest commonRequest, string getSort);
        Task<CommonResultResponseDto<int>> UpdateDayStatus(UpdateDayStatusRequestDto request);
        Task<IList<RouteTypeDayDto>> GetRouteTypeDays(int routeTypeId);
        Task<CommonResultResponseDto<string>> UpdateRouteTypeExclusivePay(UpdateRouteTypeExclusivePayRequestDto request);
        Task<CommonResultResponseDto<PaginatedList<PredefinedColorDto>>> GetPredefinedColors(string filterModel,  ServerRowsRequest commonRequest, string getSort );

        Task<CommonResultResponseDto<string>> AddUpdatePredefinedColor(AddUpdatePredefinedColorDto dto);

        Task<CommonResultResponseDto<string>> DeletePredefinedColor(int id);
        Task<CommonResultResponseDto<IList<PredefinedColorDto>>> GetPredefinedColorsDropdown();
        Task<CommonResultResponseDto<string>> UpdateBusCharge(UpdateBusChargeRequestDto updateBusChargeRequestDto);
        Task<CommonResultResponseDto<GetBusChargeQueryResponseDto>> GetBusCharge();
    }

    }




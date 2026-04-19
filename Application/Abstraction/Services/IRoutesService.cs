using Application.Common.Dtos;
using Application.Common.Response;
using Application.Handler.Routes.Queries.GetRoutes;
using Application.Handler.Routes.Queries.GetRoutesLists;
using DTO.Request.Routes;
using DTO.Response;
using DTO.Response.Admin;
using DTO.Response.Routes;
using DTO.Response.Streets;

namespace Application.Abstraction.Services
{
    public interface IRoutesService
    {
        Task<CommonResultResponseDto<PaginatedList<GetRoutesResponseDto>>> GetRoutes(string filterModel, ServerRowsRequest commonRequest, string getSort, GetRoutesRequestDto getRoutesRequestDto);
        Task<CommonResultResponseDto<PaginatedList<GetRoutesResponseDto>>> GetRoutesByTabs(string filterModel, ServerRowsRequest commonRequest, string getSort, GetRoutesRequestDto getRoutesRequestDto);
        Task<CommonResultResponseDto<PaginatedList<GetRoutesResponseDto>>> GetRoutesDetailsByTypeId(string filterModel, ServerRowsRequest commonRequest, string getSort,int routeTypeId);
        Task<CommonResultResponseDto<string>> DeleteRoutes(int id, int? deleteAll, DateTime? routeDate);
        Task<CommonResultResponseDto<string>> UndoRoutes(int id, DateTime? routeDate);
        Task<CommonResultResponseDto<string>> DeleteStudentFromRoute(int studentId, int routeId);
        Task<CommonResultResponseDto<string>> AddUpdateRoutesDetails(AddUpdateRoutesDetailsRequestDto addUpdateRoutesDetailsRequestDto);
        Task<CommonResultResponseDto<string>> UpdateBulkRoutesDetails(UpdateBulkRoutesDetailsRequestDto updateBulkRoutesDetailsRequestDto);
        Task<CommonResultResponseDto<string>> AddUpdateBulkRoutes(UpdateBulkRoutesRequestDto apdateBulkRoutesRequestDto);
        Task<CommonResultResponseDto<IList<GetRoutesListResponseDto>>> GetRoutesList();
        Task<CommonResultResponseDto<IList<GetRoutesListsResponseDto>>> GetRoutesLists(GetRoutesListsRequestDto getRoutesListsRequestDto );
        Task<CommonResultResponseDto<IList<GetAddressResponseDto>>> GetAddress();
        Task<CommonResultResponseDto<byte[]>> DownloadPrintOrder(DownloadPrintForRoutesRequestDto downloadPrintForRoutesRequestDto);
        Task<CommonResultResponseDto<PaginatedList<GetHistoryByTabResponseDto>>> GetHistoryByTab(string filterModel, ServerRowsRequest commonRequest, string getSort, string tab, int id);
        Task<CommonResultResponseDto<IList<GetSchoolBuildingBranchDetailsResponseDto>>> GetSchoolBuildingBranchDetails();
        Task<CommonResultResponseDto<IList<string>>> AddBulkRoutesDetails(AddBulkRoutesDetailsRequestDto addBulkRoutesDetailsRequest);
        Task<CommonResultResponseDto<PaginatedList<GetSchoolBuildingBranchListResponseDto>>> GetSchoolBuildingBranchList(string filterModel, ServerRowsRequest commonRequest, string getSort);
        Task<CommonResultResponseDto<PaginatedList<GetStudentsWithChangedAddressResponseDto>>> GetStudentsWithChangedAddress(string filterModel, ServerRowsRequest commonRequest, string getSort, string? routeTypeIds, int? genderId);
        Task<CommonResultResponseDto<string>> UpdateSchoolBuildingBranchMapping(UpdateSchoolBuildingBranchMappingDto updateSchoolBuildingBranchMappingDto);
        Task<CommonResultResponseDto<PaginatedList<GetSchoolBuildingBranchResponseDto>>> GetSchoolBuildingBranchByRouteId(string filterModel, ServerRowsRequest commonRequest, string getSort,int routeId);
        Task<CommonResultResponseDto<IList<GetAreaListResponseDto>>> GetAreasBySchoolAndGrade(GetAreasBySchoolAndGradeRequestDto getAreasBySchoolAndGradeRequestDto);
        Task<CommonResultResponseDto<IList<GetRoutesListResponseDto>>> GetFilteredRoutesForBusChange(GetFilteredRoutesForBusChangeRequestDto getFilteredRoutesForBusChangeRequestDto);
        Task<CommonResultResponseDto<IList<GetRoutesDetailListResponseDto>>> GetRoutesDetailList();
        Task<CommonResultResponseDto<IList<RouteTypeStudentResponseDto>>> CheckRouteTypeStudent(CheckRouteTypeStudentRequestDto checkRouteTypeStudentRequestDto);
        Task<CommonResultResponseDto<string>> UpdateStop(UpdateStopDto updateStopDto);
        Task<CommonResultResponseDto<string>> UpdateTempBusDriverDetails(UpdateTempBusDriverDetailsDto updateTempBusDriverDetailsDto);
        Task<CommonResultResponseDto<string>> UpdateTodayDriver(UpdateTodayDriverDto updateTodayDriverDto);
        Task<CommonResultResponseDto<string>> UpdateRouteGroup(UpdateRouteGroupDto updateRouteGroupDto);
        Task<CommonResultResponseDto<string>> UpdateDelayRoute(UpdateDelayRouteDto updateDelayRouteDto);

    }
}

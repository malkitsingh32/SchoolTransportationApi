using Application.Common.Dtos;
using Application.Handler.Routes.Queries.GetRoutes;
using Application.Handler.Routes.Queries.GetRoutesLists;
using DTO.Request.Routes;
using DTO.Response.Admin;
using DTO.Response.Routes;
using DTO.Response.Streets;

namespace Application.Abstraction.Repositories
{
    public interface IRoutesRepository
    {
        Task<(List<GetRoutesResponseDto>, int)> GetRoutes(string filterModel, ServerRowsRequest commonRequest, string getSort, GetRoutesRequestDto getRoutesRequestDto);
        Task<(List<GetRoutesResponseDto>, int)> GetRoutesByTabs(string filterModel, ServerRowsRequest commonRequest, string getSort, GetRoutesRequestDto getRoutesRequestDto);
        Task<(List<GetRoutesResponseDto>, int)> GetRoutesDetailsByTypeId(string filterModel, ServerRowsRequest commonRequest, string getSort,int routeTypeId);
        Task<int> DeleteRoutes(int id, int? deleteAll, DateTime? routeDate);
        Task<int> UndoRoutes(int id, DateTime? routeDate);
        Task<int> DeleteStudentFromRoute(int studentId, int routeId);
        Task<int> AddUpdateRoutesDetails(AddUpdateRoutesDetailsRequestDto addUpdateRoutesDetailsRequestDto);
        Task<int> AddUpdateBulkRoutes(UpdateBulkRoutesRequestDto updateBulkRoutesRequestDto);
        Task<int> UpdateStop(UpdateStopDto updateStopDto);
        Task<int> UpdateTempBusDriverDetails(UpdateTempBusDriverDetailsDto updateTempBusDriverDetailsDto);
        Task<int> UpdateRouteGroup(UpdateRouteGroupDto updateRouteGroupDto);
        Task<int> UpdateDelayRoute(UpdateDelayRouteDto updateDelayRouteDto);
        Task<int> UpdateTodayDriver(UpdateTodayDriverDto updateTodayDriverDto);
        Task<int> UpdateBulkRoutesDetails(UpdateBulkRoutesDetailsRequestDto updateBulkRoutesDetailsRequestDto);
        Task<IList<GetRoutesListResponseDto>> GetRoutesList();
        Task<IList<GetRoutesListsResponseDto>> GetRoutesLists(GetRoutesListsRequestDto getRoutesListsRequestDto);
        Task<IList<GetAddressResponseDto>> GetAddress();
        Task<IList<GetRoutesResponseDto>> GetStudentByRouteId(DownloadPrintForRoutesRequestDto downloadPrintForRoutesRequestDto);
        Task<IList<DownloadPrintOrderResponseDto>> DownloadPrintOrder(DownloadPrintForRoutesRequestDto downloadPrintForRoutesRequestDto);
        Task<(List<GetHistoryByTabResponseDto>, int)> GetHistoryByTab(string filterModel, ServerRowsRequest commonRequest, string getSort, string tab, int id);
        Task<IList<GetSchoolBuildingBranchDetailsResponseDto>> GetSchoolBuildingBranchDetails();
        Task<IList<InsertedRouteResponseDto>> AddBulkRoutesDetails(AddBulkRoutesDetailsRequestDto addBulkRoutesDetailsRequest,string studentBusXml, string overrideXml);
        Task<(List<GetSchoolBuildingBranchListResponseDto>, int)> GetSchoolBuildingBranchList(string filterModel, ServerRowsRequest commonRequest,string getSort);
        Task<(List<GetStudentsWithChangedAddressResponseDto>, int)> GetStudentsWithChangedAddress(string filterModel, ServerRowsRequest commonRequest,string getSort, string? routeTypeIds, int? genderId);
        Task<int> UpdateSchoolBuildingBranchMapping(string schoolBuildingBranchJson, string routeIdsJson);
        Task<(List<GetSchoolBuildingBranchResponseDto>, int)> GetSchoolBuildingBranchByRouteId(string filterModel, ServerRowsRequest commonRequest, string getSort,int routeId);
        Task<IList<GetAreaListResponseDto>> GetAreasBySchoolAndGrade(GetAreasBySchoolAndGradeRequestDto getAreasBySchoolAndGradeRequestDto);
        Task<IList<GetRoutesListResponseDto>> GetFilteredRoutesForBusChange(GetFilteredRoutesForBusChangeRequestDto getFilteredRoutesForBusChangeRequestDto);
        Task<IList<GetRoutesDetailListResponseDto>> GetRoutesDetailList();
        Task<IList<RouteTypeStudentResponseDto>> CheckRouteTypeStudent(CheckRouteTypeStudentRequestDto checkRouteTypeStudentRequestDto, string studentBusXml);


    }
}

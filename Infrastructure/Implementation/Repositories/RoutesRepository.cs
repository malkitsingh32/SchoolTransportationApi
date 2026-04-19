using Application.Abstraction.DataBase;
using Application.Abstraction.Repositories;
using Application.Common.Dtos;
using Application.Handler.Routes.Queries.GetRoutes;
using Application.Handler.Routes.Queries.GetRoutesLists;
using Dapper;
using DTO.Request.Routes;
using DTO.Response.Admin;
using DTO.Response.Routes;
using DTO.Response.Streets;
using Newtonsoft.Json;
using System.Data;

namespace Infrastructure.Implementation.Repositories
{
    public class RoutesRepository : IRoutesRepository
    {
        private readonly IDbContext _dbContext;
        private readonly IParameterManager _parameterManager;
        public RoutesRepository(IDbContext dbContext, IParameterManager parameterManager)
        {
            _dbContext = dbContext;
            _parameterManager = parameterManager;
        }

        public async Task<int> AddUpdateRoutesDetails(AddUpdateRoutesDetailsRequestDto addUpdateRoutesDetailsRequestDto)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_AddUpdateRoutes",
            _parameterManager.Get("RouteID", addUpdateRoutesDetailsRequestDto.RouteID, ParameterDirection.Input, DbType.Int32),
            _parameterManager.Get("RouteGroupingID", addUpdateRoutesDetailsRequestDto.RouteGroupID, ParameterDirection.Input, DbType.Guid),
            _parameterManager.Get("RouteNumber", addUpdateRoutesDetailsRequestDto.RouteNumber, ParameterDirection.Input, DbType.String),
            _parameterManager.Get("Time", addUpdateRoutesDetailsRequestDto.Time, ParameterDirection.Input, DbType.String),
            _parameterManager.Get("Mosdos", addUpdateRoutesDetailsRequestDto.Mosdos, ParameterDirection.Input, DbType.String),
            _parameterManager.Get("Price", addUpdateRoutesDetailsRequestDto.Price, ParameterDirection.Input, DbType.Decimal),
            _parameterManager.Get("RouteName", addUpdateRoutesDetailsRequestDto.RouteName, ParameterDirection.Input, DbType.String),
            _parameterManager.Get("Grade", addUpdateRoutesDetailsRequestDto.Grade, ParameterDirection.Input, DbType.String),
            _parameterManager.Get("Type", addUpdateRoutesDetailsRequestDto.Type, ParameterDirection.Input, DbType.Int32),
            _parameterManager.Get("Days", addUpdateRoutesDetailsRequestDto.Days, ParameterDirection.Input, DbType.String),
            _parameterManager.Get("DefaultDriver", addUpdateRoutesDetailsRequestDto.DefaultDriver, ParameterDirection.Input, DbType.Int64),
            _parameterManager.Get("TodaysDriver", addUpdateRoutesDetailsRequestDto.TodaysDriver, ParameterDirection.Input, DbType.Int64),
            _parameterManager.Get("SeasonFolderId", addUpdateRoutesDetailsRequestDto.SeasonFolderId, ParameterDirection.Input, DbType.Int64),
            _parameterManager.Get("TodaysBus", addUpdateRoutesDetailsRequestDto.TodaysBus, ParameterDirection.Input, DbType.Int64),
            _parameterManager.Get("DropOffBuilding", addUpdateRoutesDetailsRequestDto.DropOffBuilding, ParameterDirection.Input, DbType.String),
            _parameterManager.Get("UserId", addUpdateRoutesDetailsRequestDto.UserId, ParameterDirection.Input, DbType.Int32),
            _parameterManager.Get("RouteDate", addUpdateRoutesDetailsRequestDto.RouteDate, ParameterDirection.Input, DbType.DateTime),
            _parameterManager.Get("StartDate", addUpdateRoutesDetailsRequestDto.StartDate, ParameterDirection.Input, DbType.DateTime),
            _parameterManager.Get("EndDate", addUpdateRoutesDetailsRequestDto.EndDate, ParameterDirection.Input, DbType.DateTime),
            _parameterManager.Get("RouteDate", addUpdateRoutesDetailsRequestDto.RouteDate, ParameterDirection.Input, DbType.DateTime),
            _parameterManager.Get("PhoneNumber", addUpdateRoutesDetailsRequestDto.PhoneNumber, ParameterDirection.Input, DbType.String),
            _parameterManager.Get("PickUp", addUpdateRoutesDetailsRequestDto.PickUp, ParameterDirection.Input, DbType.String),
              _parameterManager.Get("DriverNotes", addUpdateRoutesDetailsRequestDto.DriverNotes, ParameterDirection.Input, DbType.String),
              _parameterManager.Get("BusTeacherPhone", addUpdateRoutesDetailsRequestDto.BusTeacherPhone, ParameterDirection.Input, DbType.String),
              _parameterManager.Get("BusTeacherName", addUpdateRoutesDetailsRequestDto.BusTeacherName, ParameterDirection.Input, DbType.String),
              _parameterManager.Get("ExclusivelyPay", addUpdateRoutesDetailsRequestDto.ExclusivelyPay, ParameterDirection.Input, DbType.String),
              _parameterManager.Get("Color", addUpdateRoutesDetailsRequestDto.Color, ParameterDirection.Input, DbType.String),
            _parameterManager.Get("Branch", addUpdateRoutesDetailsRequestDto.branch, ParameterDirection.Input, DbType.String),
            _parameterManager.Get("IsFuture", addUpdateRoutesDetailsRequestDto.IsFuture, ParameterDirection.Input, DbType.Boolean),
            _parameterManager.Get("ConfirmRoute", addUpdateRoutesDetailsRequestDto.ConfirmRoute, ParameterDirection.Input, DbType.String));
        }

        public async Task<int> AddUpdateBulkRoutes(UpdateBulkRoutesRequestDto updateBulkRoutesRequestDto)
        {
            var tvp = new DataTable();
            tvp.Columns.Add("RouteGroupID", typeof(Guid));
            tvp.Columns.Add("RouteDate", typeof(DateTime));
            foreach (var id in updateBulkRoutesRequestDto.Route)
            {
                tvp.Rows.Add(id.RouteGroupId, id.RouteDate);
            }
            return await _dbContext.ExecuteStoredProcedure<int>("usp_UpdateBulkRoutes",
            _parameterManager.Get("RouteDatailTable", tvp, ParameterDirection.Input, DbType.Object),
            _parameterManager.Get("Time", updateBulkRoutesRequestDto.Time, ParameterDirection.Input, DbType.String),
            _parameterManager.Get("TodaysDriver", updateBulkRoutesRequestDto.TodaysDriver, ParameterDirection.Input, DbType.Int64),
            _parameterManager.Get("RouteType", updateBulkRoutesRequestDto.RouteType, ParameterDirection.Input, DbType.Int64));
        }
        public async Task<int> UpdateStop(UpdateStopDto updateStopDto)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_UpdateStop",
            _parameterManager.Get("RouteId", updateStopDto.RouteId, ParameterDirection.Input, DbType.Int64),
            _parameterManager.Get("StudentId", updateStopDto.StudentId, ParameterDirection.Input, DbType.Int64),
            _parameterManager.Get("RowNumber", updateStopDto.RowNumber, ParameterDirection.Input, DbType.Int64),
            _parameterManager.Get("UniqueId", updateStopDto.UniqueId, ParameterDirection.Input, DbType.String));
        }

        public async Task<int> DeleteRoutes(int id, int? deleteAll, DateTime? routeDate)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_DeleteRoutes",
            _parameterManager.Get("Id", id, ParameterDirection.Input, DbType.Int32),
            _parameterManager.Get("DeleteAll", deleteAll, ParameterDirection.Input, DbType.Int32),
            _parameterManager.Get("RouteDate", routeDate, ParameterDirection.Input, DbType.DateTime));
        }
        public async Task<int> UndoRoutes(int id, DateTime? routeDate)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_UndoRoutes",
            _parameterManager.Get("Id", id, ParameterDirection.Input, DbType.Int32),
             _parameterManager.Get("RouteDate", routeDate, ParameterDirection.Input, DbType.DateTime));
        }
        public async Task<int> DeleteStudentFromRoute(int studentId, int routeId)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_DeleteStudentFromRoute",
            _parameterManager.Get("StudentId", studentId, ParameterDirection.Input, DbType.Int32),
            _parameterManager.Get("RouteId", routeId, ParameterDirection.Input, DbType.Int32));
        }

        public async Task<(List<GetRoutesResponseDto>, int)> GetRoutes(string filterModel, ServerRowsRequest commonRequest, string getSort, GetRoutesRequestDto getRoutesRequestDto)
        {
            List<GetRoutesResponseDto> contact;
            int total = 0;
            using (var dbConnection = _dbContext.GetDbConnection())
            {

                var result = await dbConnection.QueryMultipleAsync(
            "usp_GetRoutesDetails", _dbContext.GetDapperDynamicParameters
            (_parameterManager.Get("@StartRow", commonRequest.StartRow),
              _parameterManager.Get("@EndRow", commonRequest.EndRow),
              _parameterManager.Get("@FilterModel", filterModel),
              _parameterManager.Get("@OrderBy", getSort),
              _parameterManager.Get("@SearchText", commonRequest.SearchText),
              _parameterManager.Get("@BusDetailId", getRoutesRequestDto.BusDetailId),
              _parameterManager.Get("@StreetId", getRoutesRequestDto.StreetId),
              _parameterManager.Get("@DriverId", getRoutesRequestDto.DriverId),
              _parameterManager.Get("@StudentId", getRoutesRequestDto.StudentId),
              _parameterManager.Get("@SeasonFolderId", getRoutesRequestDto.SeasonFolderId),
              _parameterManager.Get("@IsActiveRoutes", getRoutesRequestDto.IsActiveRoutes),
              _parameterManager.Get("@Grade", getRoutesRequestDto.Grade),
              _parameterManager.Get("@School", getRoutesRequestDto.School),
              _parameterManager.Get("@Gender", getRoutesRequestDto.Gender),
              _parameterManager.Get("@RouteId", getRoutesRequestDto.RouteId),
              _parameterManager.Get("@Role", getRoutesRequestDto.Role)
                ),
            commandType: CommandType.StoredProcedure);
                total = result.Read<int>().FirstOrDefault();
                contact = result.Read<GetRoutesResponseDto>().ToList();
                dbConnection.Close();
            }
            return (contact, total);
        }
        public async Task<(List<GetRoutesResponseDto>, int)> GetRoutesByTabs(string filterModel, ServerRowsRequest commonRequest, string getSort, GetRoutesRequestDto getRoutesRequestDto)
        {
            List<GetRoutesResponseDto> contact;
            int total = 0;
            using (var dbConnection = _dbContext.GetDbConnection())
            {

                var result = await dbConnection.QueryMultipleAsync(
            "usp_GetRoutesByTabs", _dbContext.GetDapperDynamicParameters
            (_parameterManager.Get("@StartRow", commonRequest.StartRow),
              _parameterManager.Get("@EndRow", commonRequest.EndRow),
              _parameterManager.Get("@FilterModel", filterModel),
              _parameterManager.Get("@OrderBy", getSort),
              _parameterManager.Get("@SearchText", commonRequest.SearchText),
              _parameterManager.Get("@BusDetailId", getRoutesRequestDto.BusDetailId),
              _parameterManager.Get("@StreetId", getRoutesRequestDto.StreetId),
              _parameterManager.Get("@DriverId", getRoutesRequestDto.DriverId),
              _parameterManager.Get("@StudentId", getRoutesRequestDto.StudentId),
              _parameterManager.Get("@SeasonFolderId", getRoutesRequestDto.SeasonFolderId),
              _parameterManager.Get("@Grade", getRoutesRequestDto.Grade),
              _parameterManager.Get("@Role", getRoutesRequestDto.Role)
                ),
            commandType: CommandType.StoredProcedure);
                total = result.Read<int>().FirstOrDefault();
                contact = result.Read<GetRoutesResponseDto>().ToList();
                dbConnection.Close();
            }
            return (contact, total);
        }

        public async Task<(List<GetRoutesResponseDto>, int)> GetRoutesDetailsByTypeId(string filterModel, ServerRowsRequest commonRequest, string getSort, int routeTypeId)
        {
            List<GetRoutesResponseDto> contact;
            int total = 0;
            using (var dbConnection = _dbContext.GetDbConnection())
            {

                var result = await dbConnection.QueryMultipleAsync(
            "usp_GetRoutesDetailsByTypeId", _dbContext.GetDapperDynamicParameters
            (_parameterManager.Get("@StartRow", commonRequest.StartRow),
              _parameterManager.Get("@EndRow", commonRequest.EndRow),
              _parameterManager.Get("@FilterModel", filterModel),
              _parameterManager.Get("@OrderBy", getSort),
              _parameterManager.Get("@SearchText", commonRequest.SearchText),
                 _parameterManager.Get("@RouteTypeId", routeTypeId)
            ),
            commandType: CommandType.StoredProcedure);
                total = result.Read<int>().FirstOrDefault();
                contact = result.Read<GetRoutesResponseDto>().ToList();
                dbConnection.Close();
            }
            return (contact, total);
        }

        public async Task<IList<GetRoutesListResponseDto>> GetRoutesList()
        {
            return await _dbContext.ExecuteStoredProcedureList<GetRoutesListResponseDto>("usp_GetRoutesList");
        }

        public async Task<(List<GetHistoryByTabResponseDto>, int)> GetHistoryByTab(string filterModel, ServerRowsRequest commonRequest, string getSort, string tab, int id)
        {
            List<GetHistoryByTabResponseDto> contact;
            int total = 0;
            using (var dbConnection = _dbContext.GetDbConnection())
            {

                var result = await dbConnection.QueryMultipleAsync(
            "usp_GetHistoryByTab", _dbContext.GetDapperDynamicParameters
            (_parameterManager.Get("@StartRow", commonRequest.StartRow),
              _parameterManager.Get("@EndRow", commonRequest.EndRow),
              _parameterManager.Get("@FilterModel", filterModel),
              _parameterManager.Get("@OrderBy", getSort),
              _parameterManager.Get("@SearchText", commonRequest.SearchText),
                 _parameterManager.Get("@Id", id),
                 _parameterManager.Get("@Tab", tab)
            ),
            commandType: CommandType.StoredProcedure);
                total = result.Read<int>().FirstOrDefault();
                contact = result.Read<GetHistoryByTabResponseDto>().ToList();
                dbConnection.Close();
            }
            return (contact, total);
        }

        public async Task<IList<GetSchoolBuildingBranchDetailsResponseDto>> GetSchoolBuildingBranchDetails()
        {
            return await _dbContext.ExecuteStoredProcedureList<GetSchoolBuildingBranchDetailsResponseDto>("usp_GetSchoolBuildingBranchDetails");
        }

        public async Task<IList<InsertedRouteResponseDto>> AddBulkRoutesDetails(AddBulkRoutesDetailsRequestDto addBulkRoutesDetailsRequest, string studentBusXml, string overrideXml)
        {
            return await _dbContext.ExecuteStoredProcedureList<InsertedRouteResponseDto>("usp_AddBulkRoutes",
             _parameterManager.Get("RouteID", addBulkRoutesDetailsRequest.RouteId, ParameterDirection.Input, DbType.Int32),
            _parameterManager.Get("RouteNumber", addBulkRoutesDetailsRequest.RouteNumber, ParameterDirection.Input, DbType.String),
            _parameterManager.Get("Time", addBulkRoutesDetailsRequest.Times, ParameterDirection.Input, DbType.String),
            _parameterManager.Get("Mosdos", addBulkRoutesDetailsRequest.School, ParameterDirection.Input, DbType.String),
            _parameterManager.Get("RouteName", addBulkRoutesDetailsRequest.RouteName, ParameterDirection.Input, DbType.String),
            _parameterManager.Get("Grade", addBulkRoutesDetailsRequest.Grade, ParameterDirection.Input, DbType.String),
            _parameterManager.Get("Type", addBulkRoutesDetailsRequest.Type, ParameterDirection.Input, DbType.Int32),
            _parameterManager.Get("Days", addBulkRoutesDetailsRequest.Days, ParameterDirection.Input, DbType.String),
            _parameterManager.Get("DropOffBuilding", addBulkRoutesDetailsRequest.DropOffBuilding, ParameterDirection.Input, DbType.String),
            _parameterManager.Get("UserId", addBulkRoutesDetailsRequest.CreatedBy, ParameterDirection.Input, DbType.Int32),
            _parameterManager.Get("SeasonFolderId", addBulkRoutesDetailsRequest.SeasonFolderId, ParameterDirection.Input, DbType.Int32),
            _parameterManager.Get("StartDate", addBulkRoutesDetailsRequest.StartDate, ParameterDirection.Input, DbType.DateTime),
            _parameterManager.Get("EndDate", addBulkRoutesDetailsRequest.EndDate, ParameterDirection.Input, DbType.DateTime),
            _parameterManager.Get("TotalBuses", addBulkRoutesDetailsRequest.TotalBuses, ParameterDirection.Input, DbType.String),
            _parameterManager.Get("Areas", addBulkRoutesDetailsRequest.Areas, ParameterDirection.Input, DbType.String),
            _parameterManager.Get("Gender", addBulkRoutesDetailsRequest.Gender, ParameterDirection.Input, DbType.String),
            _parameterManager.Get("BusTeacherName", addBulkRoutesDetailsRequest.BusTeacherName, ParameterDirection.Input, DbType.String),
            _parameterManager.Get("BusTeacherPhone", addBulkRoutesDetailsRequest.BusTeacherPhone, ParameterDirection.Input, DbType.String),
            _parameterManager.Get("Students", studentBusXml, ParameterDirection.Input, DbType.String),
            _parameterManager.Get("Branch", addBulkRoutesDetailsRequest.Branch, ParameterDirection.Input, DbType.String),
            _parameterManager.Get("IsDraftRoute", addBulkRoutesDetailsRequest.IsDraftRoute, ParameterDirection.Input, DbType.Boolean),
            _parameterManager.Get("OverrideStudentList", overrideXml, ParameterDirection.Input, DbType.String),
            _parameterManager.Get("PickUp", addBulkRoutesDetailsRequest.PickUp, ParameterDirection.Input, DbType.Int32),
            _parameterManager.Get("ExclusivelyPay", addBulkRoutesDetailsRequest.ExclusivelyPay, ParameterDirection.Input, DbType.Boolean),
            _parameterManager.Get("Color", addBulkRoutesDetailsRequest.Color, ParameterDirection.Input, DbType.String)
            );
        }

        public async Task<(List<GetSchoolBuildingBranchListResponseDto>, int)> GetSchoolBuildingBranchList(string filterModel, ServerRowsRequest commonRequest, string getSort)
        {
            List<GetSchoolBuildingBranchListResponseDto> contact;
            int total = 0;
            using (var dbConnection = _dbContext.GetDbConnection())
            {
                var result = await dbConnection.QueryMultipleAsync(
                "usp_GetSchoolBuildingBranchList", _dbContext.GetDapperDynamicParameters
                    (_parameterManager.Get("@StartRow", commonRequest.StartRow),
                     _parameterManager.Get("@EndRow", commonRequest.EndRow),
                     _parameterManager.Get("@FilterModel", filterModel),
                     _parameterManager.Get("@OrderBy", getSort),
                     _parameterManager.Get("@SearchText", commonRequest.SearchText)
                    ),
                commandType: CommandType.StoredProcedure);
                total = result.Read<int>().FirstOrDefault();
                contact = result.Read<GetSchoolBuildingBranchListResponseDto>().ToList();
                dbConnection.Close();
            }
            return (contact, total);
        }

        public async Task<int> UpdateSchoolBuildingBranchMapping(string schoolBuildingBranchJson, string routeIdsJson)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_UpdateSchoolBuildingBranchMapping",
                    _parameterManager.Get("SchoolJSON", schoolBuildingBranchJson, ParameterDirection.Input, DbType.String),
                    _parameterManager.Get("RouteIdsJSON", routeIdsJson, ParameterDirection.Input, DbType.String)
               );
        }

        public async Task<(List<GetSchoolBuildingBranchResponseDto>, int)> GetSchoolBuildingBranchByRouteId(string filterModel, ServerRowsRequest commonRequest, string getSort, int routeId)
        {
            List<GetSchoolBuildingBranchResponseDto> school;
            int total = 0;
            using (var dbConnection = _dbContext.GetDbConnection())
            {

                var result = await dbConnection.QueryMultipleAsync(
            "usp_GetSchoolBuildingBranchByRouteId", _dbContext.GetDapperDynamicParameters
            (_parameterManager.Get("@StartRow", commonRequest.StartRow),
              _parameterManager.Get("@EndRow", commonRequest.EndRow),
              _parameterManager.Get("@FilterModel", filterModel),
              _parameterManager.Get("@OrderBy", getSort),
              _parameterManager.Get("@SearchText", commonRequest.SearchText),
                 _parameterManager.Get("@RouteId", routeId)
            ),
            commandType: CommandType.StoredProcedure);
                total = result.Read<int>().FirstOrDefault();
                school = result.Read<GetSchoolBuildingBranchResponseDto>().ToList();
                dbConnection.Close();
            }
            return (school, total);
        }

        public async Task<IList<GetAddressResponseDto>> GetAddress()
        {
            return await _dbContext.ExecuteStoredProcedureList<GetAddressResponseDto>("usp_GetAddress");
        }

        public async Task<IList<DownloadPrintOrderResponseDto>> DownloadPrintOrder(DownloadPrintForRoutesRequestDto downloadPrintForRoutesRequestDto)
        {
            List<DownloadPrintOrderResponseDto> print;
            using (var dbConnection = _dbContext.GetDbConnection())
            {
                var result = await dbConnection.QueryMultipleAsync("usp_GetPrintForRoutes", _dbContext.GetDapperDynamicParameters(
                       _parameterManager.Get("RouteId", downloadPrintForRoutesRequestDto.RouteId)
                    ),
                    commandType: CommandType.StoredProcedure);
                print = result.Read<DownloadPrintOrderResponseDto>().ToList();
                dbConnection.Close();
            }
            return print;
        }

        public async Task<IList<GetRoutesResponseDto>> GetStudentByRouteId(DownloadPrintForRoutesRequestDto downloadPrintForRoutesRequestDto)
        {
            List<GetRoutesResponseDto> print;
            using (var dbConnection = _dbContext.GetDbConnection())
            {
                var result = await dbConnection.QueryMultipleAsync("usp_GetStudentByRouteId", _dbContext.GetDapperDynamicParameters(
                       _parameterManager.Get("RouteId", downloadPrintForRoutesRequestDto.RouteId),
                       _parameterManager.Get("Date", downloadPrintForRoutesRequestDto.Date)
                    ),
                    commandType: CommandType.StoredProcedure);
                print = result.Read<GetRoutesResponseDto>().ToList();
                dbConnection.Close();
            }
            return print;
        }

        public async Task<IList<GetAreaListResponseDto>> GetAreasBySchoolAndGrade(GetAreasBySchoolAndGradeRequestDto getAreasBySchoolAndGradeRequestDto)
        {
            return await _dbContext.ExecuteStoredProcedureList<GetAreaListResponseDto>("usp_GetAreasBySchoolAndGrade",
             _parameterManager.Get("School", getAreasBySchoolAndGradeRequestDto.School, ParameterDirection.Input, DbType.String),
             _parameterManager.Get("Grade", getAreasBySchoolAndGradeRequestDto.Grade, ParameterDirection.Input, DbType.String),
             _parameterManager.Get("Building", getAreasBySchoolAndGradeRequestDto.Building, ParameterDirection.Input, DbType.String),
             _parameterManager.Get("Branch", getAreasBySchoolAndGradeRequestDto.Branch, ParameterDirection.Input, DbType.String),
             _parameterManager.Get("Gender", getAreasBySchoolAndGradeRequestDto.Gender, ParameterDirection.Input, DbType.Int32)
         );
        }
        public async Task<IList<GetRoutesListResponseDto>> GetFilteredRoutesForBusChange(GetFilteredRoutesForBusChangeRequestDto getFilteredRoutesForBusChangeRequestDto)
        {
            return await _dbContext.ExecuteStoredProcedureList<GetRoutesListResponseDto>("usp_GetFilteredRoutesForBusChange",
             _parameterManager.Get("Area", getFilteredRoutesForBusChangeRequestDto.Area, ParameterDirection.Input, DbType.String),
             _parameterManager.Get("SchoolId", getFilteredRoutesForBusChangeRequestDto.SchoolId, ParameterDirection.Input, DbType.String),
             _parameterManager.Get("Gender", getFilteredRoutesForBusChangeRequestDto.Gender, ParameterDirection.Input, DbType.String),
             _parameterManager.Get("Grade", getFilteredRoutesForBusChangeRequestDto.Grade, ParameterDirection.Input, DbType.String),
             _parameterManager.Get("Branch", getFilteredRoutesForBusChangeRequestDto.Branch, ParameterDirection.Input, DbType.Int32),
             _parameterManager.Get("StartDate", getFilteredRoutesForBusChangeRequestDto.StartDate, ParameterDirection.Input, DbType.DateTime),
             _parameterManager.Get("EndDate", getFilteredRoutesForBusChangeRequestDto.EndDate, ParameterDirection.Input, DbType.DateTime),
             _parameterManager.Get("StartFrom", getFilteredRoutesForBusChangeRequestDto.StartFrom, ParameterDirection.Input, DbType.String),
             _parameterManager.Get("Include", getFilteredRoutesForBusChangeRequestDto.Include, ParameterDirection.Input, DbType.String)
         );
        }

        public async Task<IList<GetRoutesDetailListResponseDto>> GetRoutesDetailList()
        {
            return await _dbContext.ExecuteStoredProcedureList<GetRoutesDetailListResponseDto>("usp_GetRoutesDetailList");
        }

        public async Task<IList<RouteTypeStudentResponseDto>> CheckRouteTypeStudent(CheckRouteTypeStudentRequestDto checkRouteTypeStudentRequestDto, string studentBusXml)
        {
            return await _dbContext.ExecuteStoredProcedureList<RouteTypeStudentResponseDto>("usp_checkRouteTypeStudent",
            _parameterManager.Get("Type", checkRouteTypeStudentRequestDto.Type, ParameterDirection.Input, DbType.Int32),
            _parameterManager.Get("StartDate", checkRouteTypeStudentRequestDto.StartDate, ParameterDirection.Input, DbType.DateTime),
            _parameterManager.Get("EndDate", checkRouteTypeStudentRequestDto.EndDate, ParameterDirection.Input, DbType.DateTime),
            _parameterManager.Get("Students", studentBusXml, ParameterDirection.Input, DbType.String));
        }

        public async Task<int> UpdateBulkRoutesDetails(UpdateBulkRoutesDetailsRequestDto updateBulkRoutesDetailsRequestDto)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_UpdateBulkRoutesDetails",
            _parameterManager.Get("RouteID", updateBulkRoutesDetailsRequestDto.RouteID, ParameterDirection.Input, DbType.Int32),
            _parameterManager.Get("RouteNumber", updateBulkRoutesDetailsRequestDto.RouteNumber, ParameterDirection.Input, DbType.String),
            _parameterManager.Get("Time", updateBulkRoutesDetailsRequestDto.Time, ParameterDirection.Input, DbType.String),
            _parameterManager.Get("Mosdos", updateBulkRoutesDetailsRequestDto.Mosdos, ParameterDirection.Input, DbType.String),
            _parameterManager.Get("RouteName", updateBulkRoutesDetailsRequestDto.RouteName, ParameterDirection.Input, DbType.String),
            _parameterManager.Get("Grade", updateBulkRoutesDetailsRequestDto.Grade, ParameterDirection.Input, DbType.String),
            _parameterManager.Get("Type", updateBulkRoutesDetailsRequestDto.Type, ParameterDirection.Input, DbType.Int32),
            _parameterManager.Get("Days", updateBulkRoutesDetailsRequestDto.Days, ParameterDirection.Input, DbType.String),
            _parameterManager.Get("DropOffBuilding", updateBulkRoutesDetailsRequestDto.DropOffBuilding, ParameterDirection.Input, DbType.String),
            _parameterManager.Get("UserId", updateBulkRoutesDetailsRequestDto.UserId, ParameterDirection.Input, DbType.Int32),
            _parameterManager.Get("StartDate", updateBulkRoutesDetailsRequestDto.StartDate, ParameterDirection.Input, DbType.DateTime),
            _parameterManager.Get("EndDate", updateBulkRoutesDetailsRequestDto.EndDate, ParameterDirection.Input, DbType.DateTime),
            _parameterManager.Get("TotalBuses", updateBulkRoutesDetailsRequestDto.TotalBuses, ParameterDirection.Input, DbType.String),
            _parameterManager.Get("Areas", updateBulkRoutesDetailsRequestDto.Areas, ParameterDirection.Input, DbType.String),
            _parameterManager.Get("Branch", updateBulkRoutesDetailsRequestDto.Branch, ParameterDirection.Input, DbType.String),
            _parameterManager.Get("PickUp", updateBulkRoutesDetailsRequestDto.PickUp, ParameterDirection.Input, DbType.Int32),
            _parameterManager.Get("SeasonFolderId", updateBulkRoutesDetailsRequestDto.SeasonFolderId, ParameterDirection.Input, DbType.Int32),
            _parameterManager.Get("ConfirmRoute", updateBulkRoutesDetailsRequestDto.ConfirmRoute, ParameterDirection.Input, DbType.Boolean),
            _parameterManager.Get("PRICE", updateBulkRoutesDetailsRequestDto.Price, ParameterDirection.Input, DbType.Decimal),
            _parameterManager.Get("TodaysBus", updateBulkRoutesDetailsRequestDto.TodaysBus, ParameterDirection.Input, DbType.Int32),
            _parameterManager.Get("RouteGroupingID", updateBulkRoutesDetailsRequestDto.RouteGroupingID, ParameterDirection.Input, DbType.Guid),
            _parameterManager.Get("PhoneNumber", updateBulkRoutesDetailsRequestDto.PhoneNumber, ParameterDirection.Input, DbType.String),
            _parameterManager.Get("DriverNotes", updateBulkRoutesDetailsRequestDto.DriverNotes, ParameterDirection.Input, DbType.String)
        );

        }

        public async Task<IList<GetRoutesListsResponseDto>> GetRoutesLists(GetRoutesListsRequestDto request)
        {
            return await _dbContext.ExecuteStoredProcedureList<GetRoutesListsResponseDto>("usp_GetRoutesLists",
                _parameterManager.Get("@SearchText", request.SearchText, ParameterDirection.Input, DbType.String),
                _parameterManager.Get("@TodaysDriver", request.TodaysDriver, ParameterDirection.Input, DbType.Int32),
                _parameterManager.Get("@DefaultDriver", request.DefaultDriver, ParameterDirection.Input, DbType.Int32),
                _parameterManager.Get("@Type", request.Type, ParameterDirection.Input, DbType.Int32),
                _parameterManager.Get("@SeasonFolderId", request.SeasonFolderId, ParameterDirection.Input, DbType.Int32),
                _parameterManager.Get("@Gender", request.Gender, ParameterDirection.Input, DbType.Int32),
                _parameterManager.Get("@DriverId", request.DriverId, ParameterDirection.Input, DbType.Int32),
                _parameterManager.Get("@School", request.School, ParameterDirection.Input, DbType.String),
                _parameterManager.Get("@Role", request.Role, ParameterDirection.Input, DbType.String),
                _parameterManager.Get("@Grade", request.Grade, ParameterDirection.Input, DbType.String),
                _parameterManager.Get("@StartDate", request.StartDate, ParameterDirection.Input, DbType.Date),
                _parameterManager.Get("@EndDate", request.EndDate, ParameterDirection.Input, DbType.Date)
            );
        }

        public async Task<(List<GetStudentsWithChangedAddressResponseDto>, int)> GetStudentsWithChangedAddress(string filterModel, ServerRowsRequest commonRequest, string getSort, string? routeTypeIds, int? genderId)
        {
            List<GetStudentsWithChangedAddressResponseDto> address;
            int total = 0;
            using (var dbConnection = _dbContext.GetDbConnection())
            {
                var result = await dbConnection.QueryMultipleAsync(
                "usp_GetStudentsWithChangedAddress", _dbContext.GetDapperDynamicParameters
                    (_parameterManager.Get("@StartRow", commonRequest.StartRow),
                     _parameterManager.Get("@EndRow", commonRequest.EndRow),
                     _parameterManager.Get("@FilterModel", filterModel),
                     _parameterManager.Get("@OrderBy", getSort),
                     _parameterManager.Get("@SearchText", commonRequest.SearchText),
                     _parameterManager.Get("@RouteTypeIds", routeTypeIds),
                     _parameterManager.Get("@GenderId", genderId)
                    ),
                commandType: CommandType.StoredProcedure);
                total = result.Read<int>().FirstOrDefault();
                address = result.Read<GetStudentsWithChangedAddressResponseDto>().ToList();
                dbConnection.Close();
            }
            return (address, total);
        }
        public async Task<int> UpdateTempBusDriverDetails(UpdateTempBusDriverDetailsDto updateTempBusDriverDetailsDto)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_UpdateTempBusDriverDetails",
             _parameterManager.Get("DriverID", updateTempBusDriverDetailsDto.DriverID, ParameterDirection.Input, DbType.Int32),
            _parameterManager.Get("TempBus", updateTempBusDriverDetailsDto.TempBus, ParameterDirection.Input, DbType.String),
            _parameterManager.Get("RunType", updateTempBusDriverDetailsDto.RunType, ParameterDirection.Input, DbType.String),
            _parameterManager.Get("TempBusStartTime", updateTempBusDriverDetailsDto.TempBusStartTime, ParameterDirection.Input, DbType.DateTime),
            _parameterManager.Get("TempBusEndTime", updateTempBusDriverDetailsDto.TempBusEndTime, ParameterDirection.Input, DbType.DateTime));
        }

        public async Task<int> UpdateTodayDriver(UpdateTodayDriverDto updateTodayDriverDto)
        {
            var routeJson = JsonConvert.SerializeObject(updateTodayDriverDto.RouteJson);

            return await _dbContext.ExecuteStoredProcedure<int>("usp_UpdateTodayDriver",
           _parameterManager.Get("RouteJson", routeJson, ParameterDirection.Input, DbType.String),
            _parameterManager.Get("DriverID", updateTodayDriverDto.DriverID, ParameterDirection.Input, DbType.Int32),
            _parameterManager.Get("StartDate", updateTodayDriverDto.StartDate, ParameterDirection.Input, DbType.DateTime),
            _parameterManager.Get("EndDate", updateTodayDriverDto.EndDate, ParameterDirection.Input, DbType.DateTime));
        }
        public async Task<int> UpdateRouteGroup(UpdateRouteGroupDto updateRouteGroupDto)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_UpdateRouteGroup",
         _parameterManager.Get("RouteId", updateRouteGroupDto.RouteId, ParameterDirection.Input, DbType.String),
          _parameterManager.Get("NewDriverId", updateRouteGroupDto.NewDriverId, ParameterDirection.Input, DbType.Int32),
          _parameterManager.Get("StartDate", updateRouteGroupDto.StartDate, ParameterDirection.Input, DbType.DateTime),
          _parameterManager.Get("EndDate", updateRouteGroupDto.EndDate, ParameterDirection.Input, DbType.DateTime));
        }

        public async Task<int> UpdateDelayRoute(UpdateDelayRouteDto updateDelayRouteDto)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_UpdateDelayRoute",
          _parameterManager.Get("RouteId", updateDelayRouteDto.RouteId, ParameterDirection.Input, DbType.String),
          _parameterManager.Get("Gender", updateDelayRouteDto.Gender, ParameterDirection.Input, DbType.String),
           _parameterManager.Get("Grade", updateDelayRouteDto.Grade, ParameterDirection.Input, DbType.String),
           _parameterManager.Get("School", updateDelayRouteDto.School, ParameterDirection.Input, DbType.String),
           _parameterManager.Get("Time", updateDelayRouteDto.Time, ParameterDirection.Input, DbType.String),
           _parameterManager.Get("StartDate", updateDelayRouteDto.StartDate, ParameterDirection.Input, DbType.DateTime),
          _parameterManager.Get("EndDate", updateDelayRouteDto.EndDate, ParameterDirection.Input, DbType.DateTime));
        }
    }
}

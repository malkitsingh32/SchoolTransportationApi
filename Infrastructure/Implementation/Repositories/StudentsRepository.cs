
using Application.Abstraction.DataBase;
using Application.Abstraction.Repositories;
using Application.Common.Dtos;
using Application.Handler.Students.Queries.ExportStudentsList;
using Dapper;
using DTO.Request.Students;
using DTO.Response.Students;
using Newtonsoft.Json;
using System.Data;

namespace Infrastructure.Implementation.Repositories
{
    public class StudentsRepository : IStudentsRepository
    {
        private readonly IDbContext _dbContext;
        private readonly IParameterManager _parameterManager;
        public StudentsRepository(IDbContext dbContext, IParameterManager parameterManager)
        {
            _dbContext = dbContext;
            _parameterManager = parameterManager;
        }
     
        public async Task<int> AddUpdateStudents(AddUpdateStudentsDto addUpdateStudentsDto)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_AddUpdateStudents",
                 _parameterManager.Get("studentID", addUpdateStudentsDto.StudentID, ParameterDirection.Input, DbType.Int32),
                 _parameterManager.Get("firstName", addUpdateStudentsDto.FirstName, ParameterDirection.Input, DbType.String),
                 _parameterManager.Get("lastName", addUpdateStudentsDto.LastName, ParameterDirection.Input, DbType.String),
                 _parameterManager.Get("fatherFirstName", addUpdateStudentsDto.FatherFirstName, ParameterDirection.Input, DbType.String),
                 _parameterManager.Get("motherFirstName", addUpdateStudentsDto.MotherFirstName, ParameterDirection.Input, DbType.String),
                 _parameterManager.Get("StudentLegalName", addUpdateStudentsDto.StudentLegalName, ParameterDirection.Input, DbType.String),
                 _parameterManager.Get("dob", addUpdateStudentsDto.DOB, ParameterDirection.Input, DbType.DateTime),
                 _parameterManager.Get("address", addUpdateStudentsDto.Address, ParameterDirection.Input, DbType.String),
                 _parameterManager.Get("grade", addUpdateStudentsDto.Grade, ParameterDirection.Input, DbType.Int32),
                 _parameterManager.Get("area", addUpdateStudentsDto.Area, ParameterDirection.Input, DbType.Int32),
                 _parameterManager.Get("district", addUpdateStudentsDto.District, ParameterDirection.Input, DbType.Int32),
                 _parameterManager.Get("buildingSys", addUpdateStudentsDto.BuildingSys, ParameterDirection.Input, DbType.Int32),
                 _parameterManager.Get("nT", addUpdateStudentsDto.NT, ParameterDirection.Input, DbType.String),
                 _parameterManager.Get("mWFamilyID", addUpdateStudentsDto.MWFamilyID, ParameterDirection.Input, DbType.String),
                 _parameterManager.Get("mWStudentID", addUpdateStudentsDto.MWStudentID, ParameterDirection.Input, DbType.String),
                 _parameterManager.Get("schoolFamilyID", addUpdateStudentsDto.SchoolFamilyID, ParameterDirection.Input, DbType.String),
                 _parameterManager.Get("schoolStudentID", addUpdateStudentsDto.SchoolStudentID, ParameterDirection.Input, DbType.String),
                 _parameterManager.Get("homeNumber", addUpdateStudentsDto.HomeNumber, ParameterDirection.Input, DbType.String),
                 _parameterManager.Get("fatherCell", addUpdateStudentsDto.FatherCell, ParameterDirection.Input, DbType.String),
                 _parameterManager.Get("motherCell", addUpdateStudentsDto.MotherCell, ParameterDirection.Input, DbType.String),
                 _parameterManager.Get("familyThirdCell", addUpdateStudentsDto.FamilyThirdCell, ParameterDirection.Input, DbType.String),
                 _parameterManager.Get("email", addUpdateStudentsDto.Email, ParameterDirection.Input, DbType.String),
                 _parameterManager.Get("gender", addUpdateStudentsDto.Gender, ParameterDirection.Input, DbType.Int32),
                 _parameterManager.Get("UserId", addUpdateStudentsDto.UserId, ParameterDirection.Input, DbType.Int32),
                _parameterManager.Get("State", addUpdateStudentsDto.State, ParameterDirection.Input, DbType.Int32),
                _parameterManager.Get("City", addUpdateStudentsDto.City, ParameterDirection.Input, DbType.Int32),
                _parameterManager.Get("ZipCode", addUpdateStudentsDto.Zipcode, ParameterDirection.Input, DbType.Int32),
                  _parameterManager.Get("RouteId", addUpdateStudentsDto.RouteID, ParameterDirection.Input, DbType.Int32),
                   _parameterManager.Get("StreetId", addUpdateStudentsDto.StreetId, ParameterDirection.Input, DbType.Int32),
                    _parameterManager.Get("Branch", addUpdateStudentsDto.Branch, ParameterDirection.Input, DbType.Int32),
                    _parameterManager.Get("Mosdos", addUpdateStudentsDto.Mosdos, ParameterDirection.Input, DbType.Int32),
                    _parameterManager.Get("AutoFillFamilyID", addUpdateStudentsDto.AutoFillFamilyID, ParameterDirection.Input, DbType.Int32),
                    _parameterManager.Get("FamilyID", addUpdateStudentsDto.FamilyID, ParameterDirection.Input, DbType.Int32),
                    _parameterManager.Get("StreetNumber", addUpdateStudentsDto.StreetNumber, ParameterDirection.Input, DbType.Int32),
                   _parameterManager.Get("Unit", addUpdateStudentsDto.Unit, ParameterDirection.Input, DbType.String),
                     _parameterManager.Get("IsUpdate", addUpdateStudentsDto.isUpdate, ParameterDirection.Input, DbType.Boolean),
                 _parameterManager.Get("StartDate", addUpdateStudentsDto.StartDate, ParameterDirection.Input, DbType.DateTime),
                 _parameterManager.Get("FundStartDate", addUpdateStudentsDto.FundStartDate, ParameterDirection.Input, DbType.DateTime),
                     _parameterManager.Get("IsUnknown", addUpdateStudentsDto.IsUnknown, ParameterDirection.Input, DbType.Boolean),
                     _parameterManager.Get("Isfunded", addUpdateStudentsDto.Isfunded, ParameterDirection.Input, DbType.Boolean),
                   _parameterManager.Get("LatLong", addUpdateStudentsDto.LatLong, ParameterDirection.Input, DbType.String),
                   _parameterManager.Get("IsFromBusChange", addUpdateStudentsDto.IsFromBusChange, ParameterDirection.Input, DbType.String)

     );
        }

        public async Task<int> DeleteStudent(int id, bool IsFromRoute, int type)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_DeleteStudent",
         _parameterManager.Get("Id", id, ParameterDirection.Input, DbType.Int32),
         _parameterManager.Get("IsFromRoute", IsFromRoute, ParameterDirection.Input, DbType.Boolean),
         _parameterManager.Get("Type", type, ParameterDirection.Input, DbType.Int32)
         );
        }

        public async Task<(List<GetStudentsResponseDto>, int)> GetStudents(string filterModel, ServerRowsRequest commonRequest, int routId, string getSort, int streetId, int familyId, string ntId, string dob, string? district, string schoolStudentId, string schoolId, string grade, string? gender)
    {
        List<GetStudentsResponseDto> contact;
        int total = 0;
        using (var dbConnection = _dbContext.GetDbConnection())
        {
            var result = await dbConnection.QueryMultipleAsync(
            "usp_GetStudents", _dbContext.GetDapperDynamicParameters
                (_parameterManager.Get("@StartRow", commonRequest.StartRow),
                 _parameterManager.Get("@EndRow", commonRequest.EndRow),
                 _parameterManager.Get("@FilterModel", filterModel),
                 _parameterManager.Get("@OrderBy", getSort),
                 _parameterManager.Get("@SearchText", commonRequest.SearchText),
                 _parameterManager.Get("@RouteId", routId),
                  _parameterManager.Get("@StreetId", streetId),
                  _parameterManager.Get("@FamilyId", familyId),
                  _parameterManager.Get("@NtId", ntId),
                  _parameterManager.Get("@Dob", dob),
                  _parameterManager.Get("@District", district),
                  _parameterManager.Get("@SchoolStudentId", schoolStudentId),
                  _parameterManager.Get("@SchoolId", schoolId),
                  _parameterManager.Get("@Grade", grade),
                  _parameterManager.Get("@Gender", gender)
                ),
            commandType: CommandType.StoredProcedure);
            total = result.Read<int>().FirstOrDefault();
            contact = result.Read<GetStudentsResponseDto>().ToList();
            dbConnection.Close();
        }
        return (contact, total);
    }
        
        public async Task<(List<GetStudentsResponseDto>, int)> GetStudentsForBulkRoute(string filterModel, ServerRowsRequest commonRequest, string getSort, string area, string school, string grade, int? gender, string building, string branch, string street, int userId, string uniqueId, int? routeId)
    {
        List<GetStudentsResponseDto> contact;
        int total = 0;
        using (var dbConnection = _dbContext.GetDbConnection())
        {
            var result = await dbConnection.QueryMultipleAsync(
            "usp_GetStudentsForBulkRoute", _dbContext.GetDapperDynamicParameters
                (_parameterManager.Get("@StartRow", commonRequest.StartRow),
                 _parameterManager.Get("@EndRow", commonRequest.EndRow),
                 _parameterManager.Get("@FilterModel", filterModel),
                 _parameterManager.Get("@OrderBy", getSort),
                 _parameterManager.Get("@SearchText", commonRequest.SearchText),
                 _parameterManager.Get("@Area", area),
                  _parameterManager.Get("@School", school),
                  _parameterManager.Get("@Grade", grade),
                  _parameterManager.Get("@Gender", gender),
                  _parameterManager.Get("@Building", building),
                  _parameterManager.Get("@Branch", branch),
                  _parameterManager.Get("@StreetName", street),
                  _parameterManager.Get("@UniqueId", uniqueId),
                  _parameterManager.Get("@UserId", userId),
                  _parameterManager.Get("@RouteId", routeId)
                ),
            commandType: CommandType.StoredProcedure);
            total = result.Read<int>().FirstOrDefault();
            contact = result.Read<GetStudentsResponseDto>().ToList();
            dbConnection.Close();
        }
        return (contact, total);
    }

        public async Task<(List<GetStudentsResponseDto>, int)> GetUnassignedStudents(string filterModel, ServerRowsRequest commonRequest, int routId, string getSort, int streetId, int familyId, string? routeTypeId, int? genderId)
        {
            List<GetStudentsResponseDto> contact;
            int total = 0;
            using (var dbConnection = _dbContext.GetDbConnection())
            {
                var result = await dbConnection.QueryMultipleAsync(
                "usp_GetUnasignedStudentsByRouteTypeRules", _dbContext.GetDapperDynamicParameters
                    (_parameterManager.Get("@StartRow", commonRequest.StartRow),
                     _parameterManager.Get("@EndRow", commonRequest.EndRow),
                     _parameterManager.Get("@FilterModel", filterModel),
                     _parameterManager.Get("@OrderBy", getSort),
                     _parameterManager.Get("@SearchText", commonRequest.SearchText),
                     _parameterManager.Get("@RouteId", routId),
                      _parameterManager.Get("@StreetId", streetId),
                      _parameterManager.Get("@FamilyId", familyId),
                      _parameterManager.Get("@GenderId", genderId),
                      _parameterManager.Get("@RouteTypeIds", routeTypeId)
                    ),
                commandType: CommandType.StoredProcedure);
                total = result.Read<int>().FirstOrDefault();
                contact = result.Read<GetStudentsResponseDto>().ToList();
                dbConnection.Close();
            }
            return (contact, total);
        }

        public async Task<IList<GetStudentsResponseDto>> GetStudentsList()
        {
            return await _dbContext.ExecuteStoredProcedureList<GetStudentsResponseDto>("usp_GetStudentsList");
        } 
        
        public async Task<IList<GetStudentsResponseDto>> GetStudentsWithBusChangeList()
        {
            return await _dbContext.ExecuteStoredProcedureList<GetStudentsResponseDto>("usp_GetStudentsWithBusChangeList");
        } 
        public async Task<IList<GetStudentsResponseDto>> GetStudentsByPhoneNumber(string phoneNumber)
        {
            return await _dbContext.ExecuteStoredProcedureList<GetStudentsResponseDto>("usp_GetStudentsByPhoneNumber",
                _parameterManager.Get("PhoneNumber", phoneNumber, ParameterDirection.Input, DbType.String)
            );
        }
        public async Task<IList<GetStudentsResponseDto>> GetStudentRouteInfo(int id)
        {
            return await _dbContext.ExecuteStoredProcedureList<GetStudentsResponseDto>("usp_GetStudentsInfoById",
                _parameterManager.Get("StudentId", id, ParameterDirection.Input, DbType.Int32)
            );
        }

        public async Task<IList<GetStudentsResponseDto>> GetRouteInfo(int id, string? phoneNumber, bool? isTeacher)
        {
            return await _dbContext.ExecuteStoredProcedureList<GetStudentsResponseDto>("usp_GetRouteInfo",
                _parameterManager.Get("StudentId", id, ParameterDirection.Input, DbType.Int32),
                _parameterManager.Get("PhoneNumber", phoneNumber, ParameterDirection.Input, DbType.String),
                _parameterManager.Get("isTeacher", isTeacher, ParameterDirection.Input, DbType.Boolean)
            );
        }
        public async Task<bool> GetRecordsWithin30Minutes()
        {
            try
            {
                return await _dbContext.ExecuteStoredProcedure<bool>("usp_HasRouteWithin30Minutes");
            }
            catch (Exception ex)
            {
               return false; // or throw, depending on your error-handling policy
            }
        }

        public async Task<IList<GetFamilyListResponseDto>> GetFamilyList()
        {
            return await _dbContext.ExecuteStoredProcedureList<GetFamilyListResponseDto>("usp_GetFamilyList");
        }

        public async Task<IList<GetFamilyDetailsResponseDto>> GetFamilyDetails(GetFamilyDetailsRequestDto getFamilyDetailsRequestDto)
        {
            return await _dbContext.ExecuteStoredProcedureList<GetFamilyDetailsResponseDto>("usp_GetFamilyDetails",
            _parameterManager.Get("@Phone", getFamilyDetailsRequestDto.Phone),
            _parameterManager.Get("@Address", getFamilyDetailsRequestDto.Address)
           );
        }
        public async Task<IList<GetAllBranchResponseDto>> GetAllBranch()
        {
            return await _dbContext.ExecuteStoredProcedureList<GetAllBranchResponseDto>("usp_GetAllBranch");
        }

        public async Task<IList<GetBranchByBuildingIdResponseDto>> GetBranchByBuildingId(GetBranchByBuildingIdRequestDto getBranchByBuildingIdRequestDto)
        {
            return await _dbContext.ExecuteStoredProcedureList<GetBranchByBuildingIdResponseDto>("usp_GetBranchByBuildingId",
              _parameterManager.Get("BuildingId", getBranchByBuildingIdRequestDto.BuildingId, ParameterDirection.Input, DbType.Int32)
          );
        }

        public async Task<IList<SearchLocationResponseDto>> SearchLocation(SearchLocationRequestDto searchLocationRequestDto)
        {
            return await _dbContext.ExecuteStoredProcedureList<SearchLocationResponseDto>("usp_SearchLocation",
            _parameterManager.Get("@Area", searchLocationRequestDto.Area),
            _parameterManager.Get("@School", searchLocationRequestDto.School),
            _parameterManager.Get("@Grade", searchLocationRequestDto.Grade),
            _parameterManager.Get("@Gender", searchLocationRequestDto.Gender),
            _parameterManager.Get("@Building", searchLocationRequestDto.Building),
            _parameterManager.Get("@Branch", searchLocationRequestDto.Branch),
            _parameterManager.Get("@StreetName", searchLocationRequestDto.Street),
            _parameterManager.Get("@UniqueId", searchLocationRequestDto.UniqueId),
            _parameterManager.Get("@Filter", searchLocationRequestDto.Filter),
            _parameterManager.Get("@UserId", searchLocationRequestDto.UserId)
           );
        }

        public async Task<int> AddUpdateBulkStudentsRouteId(AddUpdateBulkStudentsRouteIdDto addUpdateBulkStudentsRouteIdDto, string overrideXml)
        {
            var jsonString = JsonConvert.SerializeObject(addUpdateBulkStudentsRouteIdDto.studentsDetailLists);

            return await _dbContext.ExecuteStoredProcedure<int>(
                "usp_AddUpdateBulkStudentsRouteId",
                _parameterManager.Get("studentsDetailLists", jsonString, ParameterDirection.Input, DbType.String),
                _parameterManager.Get("AddStudent", addUpdateBulkStudentsRouteIdDto.AddStudent, ParameterDirection.Input, DbType.Boolean),
                _parameterManager.Get("OverrideStudentList", overrideXml, ParameterDirection.Input, DbType.String)


            );
        }

        public async Task<IList<GetGradeListResponseDto>> GetGradeList()
        {
            return await _dbContext.ExecuteStoredProcedureList<GetGradeListResponseDto>("usp_GetGradeList");
        }

        public async Task<int> AssignRouteToStudent(AssignRouteToStudentRequestDto assignRouteToStudentRequestDto)
        {
            // Student TVP
            var studentTable = new DataTable();
            studentTable.Columns.Add("StudentId", typeof(int));
            studentTable.Columns.Add("StreetNumber", typeof(string));
            studentTable.Columns.Add("Address", typeof(string));

            foreach (var student in assignRouteToStudentRequestDto.Students)
            {
                studentTable.Rows.Add(
                    student.StudentId,
                    student.StreetNumber,
                    student.Address
                );
            }

            var routeTable = new DataTable();
            routeTable.Columns.Add("RouteID", typeof(int));

            foreach (var id in assignRouteToStudentRequestDto.RouteId)
                routeTable.Rows.Add(id);

            return await _dbContext.ExecuteStoredProcedure<int>(
                "usp_AssignRouteToUnassignedStudents",
                _parameterManager.Get("StudentDetails", studentTable, ParameterDirection.Input, DbType.Object),
                _parameterManager.Get("RouteIds", routeTable, ParameterDirection.Input, DbType.Object)
            );
        }


        public async Task<int> UpdateStudentsIndex(UpdateStudentsIndexRquestDto updateStudentsIndexRquestDto)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_UpdateStudentRowNumber",
                _parameterManager.Get("StudentId", updateStudentsIndexRquestDto.StudentId, ParameterDirection.Input, DbType.Int32),
                _parameterManager.Get("NewRowNumber", updateStudentsIndexRquestDto.RowNumber, ParameterDirection.Input, DbType.Int32)
            );
        }

        public async Task<IList<ExportStudentsListDto>> ExportStudentsList(ExportStudentsListQuery exportStudentsListQuery)
        {
            return await _dbContext.ExecuteStoredProcedureList<ExportStudentsListDto>("usp_ExportStudentsList",
           _parameterManager.Get("@NtId", exportStudentsListQuery.NtId),
          _parameterManager.Get("@Dob", exportStudentsListQuery.Dob),
          _parameterManager.Get("@District", exportStudentsListQuery.District),
          _parameterManager.Get("@SchoolStudentId", exportStudentsListQuery.SchoolStudentId),
          _parameterManager.Get("@SchoolId", exportStudentsListQuery.SchoolId),
          _parameterManager.Get("@Grade", exportStudentsListQuery.Grade),
          _parameterManager.Get("@Gender", exportStudentsListQuery.Gender),
          _parameterManager.Get("@SearchText", exportStudentsListQuery.SearchText)
          );
        }
        
        public async Task<int> UpdateBusStopIndex(string BusStopIndexJson)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_UpdateBusStopIndex",
                     _parameterManager.Get("BusStopIndexJSON", BusStopIndexJson, ParameterDirection.Input, DbType.String)
            );
        }

        public async Task<int> UpdateStudentRouteNote(UpdateStudentRouteNoteDto updateStudentRouteNoteDto)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_UpdateStudentRouteNote",
               _parameterManager.Get("StudentId", updateStudentRouteNoteDto.StudentId, ParameterDirection.Input, DbType.Int32),
               _parameterManager.Get("RouteId", updateStudentRouteNoteDto.RouteId, ParameterDirection.Input, DbType.Int32),
               _parameterManager.Get("Note", updateStudentRouteNoteDto.Note, ParameterDirection.Input, DbType.String)

           );
        }

        public async Task<int> UpdateBulkNtStatus(UpdateBulkNtStatusRequestDto updateBulkNtStatusRequestDto)
            {
            var ntIdsCsv = string.Join(",", updateBulkNtStatusRequestDto.NtIds);
            var studentIdsCsv = string.Join(",", updateBulkNtStatusRequestDto.StudentIds);

            return await _dbContext.ExecuteStoredProcedure<int>("usp_UpdateBulkNtStatus",
                _parameterManager.Get("NtIds", ntIdsCsv, ParameterDirection.Input, DbType.String),
                _parameterManager.Get("StudentIds", studentIdsCsv, ParameterDirection.Input, DbType.String)
            );
        }

        public async Task<IList<UpdateFundedDetailsFromExcelResponseDto>> UpdateFundedDetailsFromExcel(int districtId, DateTime firstDayOfSelectedMonth, string fundedDetailsJson)
        {
            return await _dbContext.ExecuteStoredProcedureList<UpdateFundedDetailsFromExcelResponseDto>(
                "usp_UpdateFundedDetailsFromExcel",
                _parameterManager.Get("@DistrictId", districtId, ParameterDirection.Input, DbType.Int32),
                _parameterManager.Get("@FirstDayOfSelectedMonth", firstDayOfSelectedMonth, ParameterDirection.Input, DbType.Date),
                _parameterManager.Get("@FundedDetailsJson", fundedDetailsJson, ParameterDirection.Input, DbType.String)
            );
        }

        public async Task<IList<GetGradeBranchListResponseDto>> GetGradeBranchList()
        {
            return await _dbContext.ExecuteStoredProcedureList<GetGradeBranchListResponseDto>("usp_GetGradeBranchList");
        }

       public async Task <ImportStudentsResult> ImportBulkStudents(ImportBulkStudentsRequestDto importBulkStudentsRequestDto)
        {
            var jsonString = JsonConvert.SerializeObject(importBulkStudentsRequestDto.ImportBulkStudentsLists);
            ImportStudentsResult getPrintOrderByOrderIdResponse = new();
            using (var dbConnection = _dbContext.GetDbConnection())
            {
                var result = await dbConnection.QueryMultipleAsync(
                    "usp_ImportStudentsFromJson", _dbContext.GetDapperDynamicParameters(
                        _parameterManager.Get("ImportBulkStudentsLists", jsonString)
                    ),
                    commandType: CommandType.StoredProcedure);
                getPrintOrderByOrderIdResponse.Inserted = result.Read<InsertedStudentResult>().ToList();
                getPrintOrderByOrderIdResponse.Failed = result.Read<FailedStudentResult>().ToList();
            }
            return (getPrintOrderByOrderIdResponse);
        }

        public async Task<int> MergeFamilyByAutoFillFamilyId(AddUpdateStudentsDto addUpdateStudentsDto)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_MergeFamilyByAutoFillFamilyId",
                   _parameterManager.Get("AutoFillFamilyId", addUpdateStudentsDto.AutoFillFamilyID, ParameterDirection.Input, DbType.Int32));
        }

        public async Task<IList<AssignChangeAddressStudentResponseDto>> AssignChangeAddressStudent(AssignChangeAddressStudentRequestDto assignChangeAddressStudentRequestDto)
        {
            // Student TVP
            var studentTable = new DataTable();
            studentTable.Columns.Add("StudentId", typeof(int));
            studentTable.Columns.Add("StreetNumber", typeof(string));
            studentTable.Columns.Add("Address", typeof(string));

            foreach (var student in assignChangeAddressStudentRequestDto.Students)
            {
                studentTable.Rows.Add(
                    student.StudentId,
                    student.StreetNumber,
                    student.Address
                );
            }

            var routeTable = new DataTable();
            routeTable.Columns.Add("RouteID", typeof(int));

            foreach (var id in assignChangeAddressStudentRequestDto.RouteId)
                routeTable.Rows.Add(id);

            return await _dbContext.ExecuteStoredProcedureList<AssignChangeAddressStudentResponseDto>(
                "usp_AssignChangeAddressStudent",
                _parameterManager.Get("StudentDetails", studentTable, ParameterDirection.Input, DbType.Object),
                _parameterManager.Get("RouteIds", routeTable, ParameterDirection.Input, DbType.Object),
                _parameterManager.Get("IsAssignStudent", assignChangeAddressStudentRequestDto.IsAssignStudent, ParameterDirection.Input, DbType.Boolean)
            );
        }
        public async Task<IList<GetFatherCellsListResponseDto>> GetFatherCellsByRouteId(string routeId)
        {
            return await _dbContext.ExecuteStoredProcedureList<GetFatherCellsListResponseDto>("usp_GetFatherCellByRouteId",
               _parameterManager.Get("RouteIds", routeId, ParameterDirection.Input, DbType.String)
           );
        }

       public async Task<(List<GetStudentsByFamilyIdResponseDto>, int)> GetStudentsByFamilyId(string filterModel, ServerRowsRequest commonRequest, string getSort, string familyId)
        {
            List<GetStudentsByFamilyIdResponseDto> contact;
            int total = 0;
            using (var dbConnection = _dbContext.GetDbConnection())
            {
                var result = await dbConnection.QueryMultipleAsync(
                "usp_GetStudentsByFamilyId", _dbContext.GetDapperDynamicParameters
                    (_parameterManager.Get("@StartRow", commonRequest.StartRow),
                     _parameterManager.Get("@EndRow", commonRequest.EndRow),
                     _parameterManager.Get("@FilterModel", filterModel),
                     _parameterManager.Get("@OrderBy", getSort),
                     _parameterManager.Get("@SearchText", commonRequest.SearchText),
                      _parameterManager.Get("@FamilyId", familyId)
                    ),
                commandType: CommandType.StoredProcedure);
                total = result.Read<int>().FirstOrDefault();
                contact = result.Read<GetStudentsByFamilyIdResponseDto>().ToList();
                dbConnection.Close();
            }
            return (contact, total);
        }

        public async Task<(List<GetFamilyChargesByFamilyIdResponseDto>, int)> GetFamilyChargesByFamilyId(string filterModel,ServerRowsRequest commonRequest,string getSort,string familyId)        
        {
            List<GetFamilyChargesByFamilyIdResponseDto> response = new();
            int total = 0;

            using (var dbConnection = _dbContext.GetDbConnection())
            {
                var result = await dbConnection.QueryMultipleAsync(
                    "usp_GetFamilyChargesByFamilyId",
                    _dbContext.GetDapperDynamicParameters(
                        _parameterManager.Get("@StartRow", commonRequest.StartRow),
                        _parameterManager.Get("@EndRow", commonRequest.EndRow),
                        _parameterManager.Get("@FilterModel", filterModel),
                        _parameterManager.Get("@OrderBy", getSort),
                        _parameterManager.Get("@SearchText", commonRequest.SearchText),
                        _parameterManager.Get("@FamilyId", familyId)
                    ),
                    commandType: CommandType.StoredProcedure);

                total = result.Read<int>().FirstOrDefault();

                var charges = result.Read<ChargeMasterDto>().ToList();
                var details = result.Read<ChargeDetailDto>().ToList();

                response = charges.Select(c => new GetFamilyChargesByFamilyIdResponseDto
                {
                    ChargeId = c.Id,
                    ChargeDate = c.Date,
                    Amount = c.Amount,
                    //ProcessDate = c.ProcessDateTime,
                    Balance = c.Balance,
                    Description = c.Description,
                    Details = details.Where(d => d.ChargeId == c.Id).Select(d => new GetFamilyChargesDetailsDto
                    {
                        StudentName = d.Student,
                        DOB = d.DOB,
                        Funding = d.Funding,
                        Type = d.Type,
                        Amount = d.Amount,
                        ProcessDateTime = d.ProcessDateTime,
                        Status = d.Status,
                        ChargeId = d.ChargeId
                    }).ToList()
                }).ToList();
            }

            return (response, total);
        }
        public async Task<GetStudentByIdResponse> GetStudentsById(int studentId)
        {
            return await _dbContext.ExecuteStoredProcedure<GetStudentByIdResponse>("usp_GetStudentsById",
               _parameterManager.Get("@StundentId", studentId, ParameterDirection.Input, DbType.Int32)
           );
        }

        public async Task<IList<TeacherPhoneResponseDto>> GetTeacherByPhoneNumber(string phoneNumber)
        {
            return await _dbContext.ExecuteStoredProcedureList<TeacherPhoneResponseDto>("usp_GetTeacherByPhoneNumber",
                _parameterManager.Get("PhoneNumber", phoneNumber, ParameterDirection.Input, DbType.String)
                );
        }
        public async Task<int> AddFamilyCharge(AddFamilyChargeDto dto)
        {
            return await _dbContext.ExecuteStoredProcedure<int>(
                "usp_AddFamilyCharge",
                _parameterManager.Get("@FamilyId", dto.FamilyId, ParameterDirection.Input, DbType.Int32),
                _parameterManager.Get("@Amount", dto.Amount, ParameterDirection.Input, DbType.Decimal),
                _parameterManager.Get("@Description", dto.Description, ParameterDirection.Input, DbType.String)
            );
        }

        public async Task<List<ChargeDropdownDto>> GetFamilyChargesForDropdown(int familyId)
        {
            var result = await _dbContext.ExecuteStoredProcedureList<ChargeDropdownDto>(
                "usp_GetFamilyChargesForDropdown",
                _parameterManager.Get("@FamilyId", familyId, ParameterDirection.Input, DbType.Int32)
            );

            return result.ToList();
        }
    }
}

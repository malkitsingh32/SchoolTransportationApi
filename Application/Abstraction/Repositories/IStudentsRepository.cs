using Application.Common.Dtos;
using Application.Common.Response;
using Application.Handler.Students.Queries.ExportStudentsList;
using DTO.Request.Students;
using DTO.Response.Students;

namespace Application.Abstraction.Repositories
{
    public interface IStudentsRepository
    {
        Task<(List<GetStudentsResponseDto>, int)> GetStudents(string filterModel, ServerRowsRequest commonRequest, int routId, string getSort, int streetId, int familyId, string ntId, string dob, string? district, string schoolStudentId, string schoolId, string grade, string? gender);
        Task<(List<GetStudentsResponseDto>, int)> GetStudentsForBulkRoute(string filterModel, ServerRowsRequest commonRequest, string getSort, string area, string school, string grade, int? gender, string building, string branch, string street, int userId, string uniqueId, int? routeId);
        Task<(List<GetStudentsResponseDto>, int)> GetUnassignedStudents(string filterModel, ServerRowsRequest commonRequest, int routId, string getSort, int streetId, int familyId, string? routeTypeId, int? genderId);
        Task<int> AddUpdateStudents(AddUpdateStudentsDto addUpdateStudentsDto);
        Task<int> MergeFamilyByAutoFillFamilyId(AddUpdateStudentsDto addUpdateStudentsDto);
        Task<int> DeleteStudent(int id, bool IsFromRoute, int type);
        Task<IList<GetStudentsResponseDto>> GetStudentsList();
        Task<IList<GetStudentsResponseDto>> GetStudentsWithBusChangeList();
        Task<IList<GetStudentsResponseDto>> GetStudentsByPhoneNumber(string phoneNumber);
        Task<IList<GetStudentsResponseDto>> GetStudentRouteInfo(int id);
        Task<IList<GetStudentsResponseDto>> GetRouteInfo(int id, string? phoneNumber, bool? isTeacher);
        Task<bool> GetRecordsWithin30Minutes();
        Task<IList<GetFamilyListResponseDto>> GetFamilyList();
        Task<IList<GetFamilyDetailsResponseDto>> GetFamilyDetails(GetFamilyDetailsRequestDto getFamilyDetailsRequestDto);
        Task<IList<GetAllBranchResponseDto>> GetAllBranch();
        Task<IList<GetGradeListResponseDto>> GetGradeList();
        Task<IList<GetGradeBranchListResponseDto>> GetGradeBranchList();
        Task<IList<SearchLocationResponseDto>> SearchLocation(SearchLocationRequestDto searchLocationRequestDto);
        Task<IList<GetBranchByBuildingIdResponseDto>> GetBranchByBuildingId(GetBranchByBuildingIdRequestDto getBranchByBuildingIdRequestDto);
        Task<int> AddUpdateBulkStudentsRouteId(AddUpdateBulkStudentsRouteIdDto addUpdateBulkStudentsRouteIdDto,string overrideXml);
        Task<int> AssignRouteToStudent(AssignRouteToStudentRequestDto assignRouteToStudentRequestDto);
        Task<IList<AssignChangeAddressStudentResponseDto>> AssignChangeAddressStudent(AssignChangeAddressStudentRequestDto assignChangeAddressStudentRequestDto);
        Task<int> UpdateStudentsIndex(UpdateStudentsIndexRquestDto updateStudentsIndexRquestDto);
        Task<IList<ExportStudentsListDto>> ExportStudentsList(ExportStudentsListQuery exportStudentsListQuery);
        Task<int> UpdateBusStopIndex(string BusStopIndexJson);
        Task<int> UpdateStudentRouteNote(UpdateStudentRouteNoteDto updateStudentRouteNoteDto);
        Task<int> UpdateBulkNtStatus(UpdateBulkNtStatusRequestDto updateBulkNtStatusRequestDto);
        Task <IList<UpdateFundedDetailsFromExcelResponseDto>> UpdateFundedDetailsFromExcel(int districtId, DateTime firstDayOfSelectedMonth, string fundedDetailsJson);
        Task<ImportStudentsResult> ImportBulkStudents(ImportBulkStudentsRequestDto importBulkStudentsRequestDto);
        Task<IList<GetFatherCellsListResponseDto>> GetFatherCellsByRouteId(string routeId);
        Task<GetStudentByIdResponse> GetStudentsById(int routeId);
        Task<(List<GetStudentsByFamilyIdResponseDto>, int)>GetStudentsByFamilyId(string filterModel, ServerRowsRequest commonRequest, string getSort, string familyId);
        Task<(List<GetFamilyChargesByFamilyIdResponseDto>, int)> GetFamilyChargesByFamilyId(string filterModel, ServerRowsRequest commonRequest, string getSort, string familyId);
        Task<IList<TeacherPhoneResponseDto>> GetTeacherByPhoneNumber(string phoneNumber);
        Task<int> AddFamilyCharge(AddFamilyChargeDto dto);
        Task<List<ChargeDropdownDto>> GetFamilyChargesForDropdown(int familyId);


    }
}


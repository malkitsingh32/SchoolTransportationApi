using Application.Common.Dtos;
using Application.Common.Response;
using Application.Handler.Students.Queries.ExportStudentsList;
using DTO.Request.Students;
using DTO.Response;
using DTO.Response.Students;

namespace Application.Abstraction.Services
{
    public interface IStudentsService
    {
        Task<CommonResultResponseDto<PaginatedList<GetStudentsResponseDto>>> GetStudents(string filterModel, ServerRowsRequest commonRequest, int routeId, string getSort, int streetId, int familyId, string ntId, string dob, string? district, string schoolStudentId, string schoolId, string grade, string? gender);
        Task<CommonResultResponseDto<PaginatedList<GetStudentsResponseDto>>> GetStudentsForBulkRoute(string filterModel, ServerRowsRequest commonRequest, string getSort, string area, string school, string grade, int? gender, string building, string branch, string street, int userId, string uniqueId, int? routeId);
        Task<CommonResultResponseDto<PaginatedList<GetStudentsResponseDto>>> GetUnassignedStudents(string filterModel, ServerRowsRequest commonRequest, int routeId, string getSort, int streetId, int familyId, string? routeTypeId, int? genderId);
        Task<CommonResultResponseDto<string>> AddUpdateStudents(AddUpdateStudentsDto addUpdateStudentsDto);
        Task<CommonResultResponseDto<string>> DeleteStudent(int id, bool IsFromRoute, int type);
        Task<CommonResultResponseDto<IList<GetStudentsResponseDto>>> GetStudentsList();
        Task<CommonResultResponseDto<IList<GetStudentsResponseDto>>> GetStudentsWithBusChangeList();
        Task<CommonResultResponseDto<IList<GetFamilyListResponseDto>>> GetFamilyList();
        Task<CommonResultResponseDto<IList<GetStudentsResponseDto>>> GetStudentsByPhoneNumber(string phoneNumber);
        Task<CommonResultResponseDto<IList<TeacherPhoneResponseDto>>> GetTeacherByPhoneNumber(string phoneNumber);

        Task<CommonResultResponseDto<bool>> GetRecordsWithin30Minutes();
        Task<CommonResultResponseDto<IList<GetStudentsResponseDto>>> GetStudentRouteInfo(int id);
        Task<CommonResultResponseDto<IList<GetStudentsResponseDto>>> GetRouteInfo(int id, string? phoneNumber, bool? isTeacher);
        Task<CommonResultResponseDto<IList<GetFamilyDetailsResponseDto>>> GetFamilyDetails(GetFamilyDetailsRequestDto getBooksDetailRequestDto);
        Task<CommonResultResponseDto<IList<GetAllBranchResponseDto>>> GetAllBranch();
        Task<CommonResultResponseDto<IList<GetGradeListResponseDto>>> GetGradeList();
        Task<CommonResultResponseDto<IList<SearchLocationResult>>> SearchLocation(SearchLocationRequestDto searchLocationRequestDto);
        Task<CommonResultResponseDto<IList<GetBranchByBuildingIdResponseDto>>> GetBranchByBuildingId(GetBranchByBuildingIdRequestDto getBranchByBuildingIdRequestDto);
        Task<CommonResultResponseDto<string>> AddUpdateBulkStudentsRouteId(AddUpdateBulkStudentsRouteIdDto addUpdateBulkStudentsRouteIdDto);
        Task<CommonResultResponseDto<string>> AssignRouteToStudent(AssignRouteToStudentRequestDto assignRouteToStudentRequestDto);
        Task<CommonResultResponseDto<IList<AssignChangeAddressStudentResponseDto>>> AssignChangeAddressStudent(AssignChangeAddressStudentRequestDto assignChangeAddressStudentRequestDto);
        Task<CommonResultResponseDto<string>> UpdateStudentsIndex(UpdateStudentsIndexRquestDto updateStudentsIndexRquestDto);
        Task<ExportFileResult> ExportStudentsList(ExportStudentsListQuery exportStudentsListQuery);
        Task<CommonResultResponseDto<string>> UpdateBusStopIndex(UpdateBusStopIndexDto updateBusStopIndexDto);
        Task<CommonResultResponseDto<string>> UpdateStudentRouteNote(UpdateStudentRouteNoteDto updateStudentRouteNoteDto);
        Task<CommonResultResponseDto<string>> UpdateBulkNtStatus(UpdateBulkNtStatusRequestDto updateBulkNtStatusRequestDto);
        Task<CommonResultResponseDto<UpdateFundedDetailsFromExcelResultDto>> UpdateFundedDetailsFromExcel(UpdateFundedDetailsFromExcelRequestDto updateFundedDetailsFromExcelRequestDto);
        Task<CommonResultResponseDto<IList<GetGradeBranchListResponseDto>>> GetGradeBranchList();
        Task<CommonResultResponseDto<ImportStudentsResult>> ImportBulkStudents(ImportBulkStudentsRequestDto importBulkStudentsRequestDto);
        Task<CommonResultResponseDto<IList<GetFatherCellsListResponseDto>>> GetFatherCellsByRouteId(string routeId);
        Task<byte[]> DownloadStudentApplicationForm(int studentId);

        Task<CommonResultResponseDto<PaginatedList<GetStudentsByFamilyIdResponseDto>>> GetStudentsByFamilyId(string filterModel, ServerRowsRequest commonRequest, string getSort, string familyId);
        Task<CommonResultResponseDto<PaginatedList<GetFamilyChargesByFamilyIdResponseDto>>> GetFamilyChargesByFamilyId(string filterModel, ServerRowsRequest commonRequest, string getSort, string familyId);
        Task<CommonResultResponseDto<string>> AddFamilyCharge(AddFamilyChargeDto dto);
        Task<List<ChargeDropdownDto>> GetFamilyChargesForDropdown(int familyId);
    }

}

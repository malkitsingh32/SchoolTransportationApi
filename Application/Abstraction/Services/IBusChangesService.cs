using Application.Common.Dtos;
using Application.Common.Response;
using DTO.Request.BusChanges;
using DTO.Response;
using DTO.Response.BusChanges;
using DTO.Response.Students;

namespace Application.Abstraction.Services
{
    public interface IBusChangesService
    {
        Task<CommonResultResponseDto<PaginatedList<GetBusChangesDto>>> GetBusChanges(string filterModel, ServerRowsRequest commonRequest, string getSort, int? studentId);
        Task<CommonResultResponseDto<string>> AddUpdateBusChanges(AddUpdateBusChangesRequestDto addUpdateBusChangesRequestDto);
        Task<CommonResultResponseDto<PaginatedList<GetStudentsResponseDto>>> GetBusChangeStudents(string filterModel, ServerRowsRequest commonRequest, int routeId, string getSort, int streetId, int familyId, string ntId, string dob, string? district, string schoolStudentId, string schoolId, string grade, string? gender);
        Task<CommonResultResponseDto<PaginatedList<GetBusChargesByFamilyIdResponseDto>>> GetBusChargesByFamilyId(string filterModel, ServerRowsRequest commonRequest, string getSort, string familyId);
    }
}

using Application.Common.Dtos;
using DTO.Request.BusChanges;
using DTO.Response.BusChanges;
using DTO.Response.Students;

namespace Application.Abstraction.Repositories
{
    public interface IBusChangesRepository
    {
        Task<(List<GetBusChangesDto>, int)> GetBusChanges(string filterModel, ServerRowsRequest commonRequest, string getSort, int? studentId);
        Task<int> AddUpdateBusChanges(AddUpdateBusChangesRequestDto addUpdateBusChangesRequestDto);
        Task<(List<GetStudentsResponseDto>, int)> GetBusChangeStudents(string filterModel, ServerRowsRequest commonRequest, int routId, string getSort, int streetId, int familyId, string ntId, string dob, string? district, string schoolStudentId, string schoolId, string grade, string? gender);
        Task<(List<GetBusChargesByFamilyIdResponseDto>, int)> GetBusChargesByFamilyId(string filterModel, ServerRowsRequest commonRequest, string getSort, string familyId);
    }
}

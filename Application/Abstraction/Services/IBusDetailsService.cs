using Application.Common.Dtos;
using Application.Common.Response;
using DTO.Request.BusDetails;
using DTO.Response;
using DTO.Response.Bus;
using DTO.Response.BusStatus;

namespace Application.Abstraction.Services
{
    public interface IBusDetailsService
    {
        Task<CommonResultResponseDto<IList<GetBusesResponseDto>>> GetAllBusDetails();
        Task<CommonResultResponseDto<PaginatedList<GetBusesResponseDto>>> GetBuses(string filterModel, ServerRowsRequest commonRequest, string getSort, int routeId);
        Task<CommonResultResponseDto<IList<CheckDriverHasBusResponseDto>>> AddUpdateBusDetails(AddUpdateBusDetailsRequestDto addUpdateBusDetailsRequestDto);
        Task<CommonResultResponseDto<string>> DeleteBusDetails(int id);
        Task<CommonResultResponseDto<IList<GetBusStatusResponseDto>>> GetBusStatus();
        Task<CommonResultResponseDto<IList<BusPosition>>> GetRecentBusPositions(string busName, int routeId);
    }
}

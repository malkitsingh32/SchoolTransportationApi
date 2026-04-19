using Application.Common.Dtos;
using DTO.Request.BusDetails;
using DTO.Response.Bus;
using DTO.Response.BusStatus;

namespace Application.Abstraction.Repositories
{
    public interface IBusDetailsRepository
    {
        Task<IList<GetBusesResponseDto>> GetAllBusDetails();
        Task<(List<GetBusesResponseDto>, int)> GetBuses(string filterModel, ServerRowsRequest commonRequest, string getSort, int routId);
        Task<int> AddUpdateBusDetails(AddUpdateBusDetailsRequestDto addUpdateBusDetailsRequestDto);
        Task<IList<CheckDriverHasBusResponseDto>> CheckDriverHasBus(AddUpdateBusDetailsRequestDto addUpdateBusDetailsRequestDto);
        Task<IList<CheckDriverHasBusResponseDto>> CheckDriverHasTempBus(AddUpdateBusDetailsRequestDto addUpdateBusDetailsRequestDto);
        Task<string> DeleteBusDetails(int id);
        Task<IList<GetBusStatusResponseDto>> GetBusStatus();
        Task<IList<BusPosition>> GetRecentBusPositions(string busName, int routeId);
    }
}

using DTO.Response.BackgroundServices;
using DTO.Response.Routes;

namespace Application.Abstraction.Repositories
{
    public interface IBackgroundRepository
    {
        Task<int> AddHistory(string addHistoryRequestDto);
        Task<int> AddBuLocationData(string addBuLocationData);
        Task<int> AddCardknoxCustomers(string cardknoxCustomers);
        Task<int> AddTransaction(string transaction);
        Task<int> AddPayroll();
        Task<IList<GetHistoryResponseDto>> GetHistory();
        Task<IList<GetMonthlyChargesByFamilyIdResponseDto>> getMonthlyChargesByFamilyId();
        Task<int> InsertNextDayRouteDetails();
        Task<int> RemoveExpiredTempBusDrivers();
        Task<IList<GetRoutesResponseDto>> GetTodayUpcomingRoutes();
        Task<int> InsertStopPassedByBusOnRoute(int routeId, DateTime? routeDate, int lastPassedStop);
    }
}

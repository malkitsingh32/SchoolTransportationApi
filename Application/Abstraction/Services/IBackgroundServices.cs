using DTO.Request.BackgroundServices;
using DTO.Response;
using DTO.Response.BackgroundServices;
using DTO.Response.CardknoxCustomers;
using DTO.Response.Routes;

namespace Application.Abstraction.Services
{
    public interface IBackgroundServices
    {
        Task<CommonResultResponseDto<string>> AddHistory(AddHistoryRequestDto addHistoryRequestDto);
        Task<CommonResultResponseDto<string>> AddBuLocationData(BusLocationResponseDto busLocationResponseDto);
        Task<CommonResultResponseDto<string>> AddPayroll();
        Task<IList<GetHistoryResponseDto>> GetHistory();
        Task<IList<GetMonthlyChargesByFamilyIdResponseDto>> getMonthlyChargesByFamilyId();
        Task<CommonResultResponseDto<string>> AddCardknoxCustomers(List<CardknoxCustomersResponseDto> cardknoxCustomersResponseDto);
        Task<CommonResultResponseDto<string>> AddTransaction(List<TransactionDto> cardknoxCustomersResponseDto);
        Task<CommonResultResponseDto<string>> InsertNextDayRouteDetails();
        Task<IList<GetRoutesResponseDto>> GetTodayUpcomingRoutes();
        Task<CommonResultResponseDto<string>> RemoveExpiredTempBusDrivers();
        Task<CommonResultResponseDto<string>> InsertStopPassedByBusOnRoute(int routeId, DateTime? routeDate, int lastPassedStop);
    }
}

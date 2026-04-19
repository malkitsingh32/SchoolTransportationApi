using Application.Abstraction.DataBase;
using Application.Abstraction.Repositories;
using DTO.Response.BackgroundServices;
using DTO.Response.Routes;
using System.Data;

namespace Infrastructure.Implementation.Repositories
{
    public class BackgroundRepository : IBackgroundRepository
    {
        private readonly IDbContext _dbContext;
        private readonly IParameterManager _parameterManager;
        public BackgroundRepository(IDbContext dbContext, IParameterManager parameterManager)
        {
            _dbContext = dbContext;
            _parameterManager = parameterManager;
        }

        public async Task<int> AddHistory(string addHistoryRequestDto)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_AddHistory",
            _parameterManager.Get("@HistoryXML", addHistoryRequestDto));
        }
        
        public async Task<int> AddBuLocationData(string addBuLocationData)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_AddBuLocationData",
            _parameterManager.Get("@BusLocationXML", addBuLocationData));
        }
        
        public async Task<int> AddCardknoxCustomers(string cardknoxCustomers)
        {
            try
            {
                return await _dbContext.ExecuteStoredProcedure<int>("usp_AddCardknoxCustomers",
                _parameterManager.Get("@CardknoxCustomerXML", cardknoxCustomers));
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public async Task<int> AddTransaction(string transactions)
        {
            try
            {
                return await _dbContext.ExecuteStoredProcedure<int>("usp_AddOrUpdateTransactions",
                _parameterManager.Get("@transactionsXML", transactions));
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<int> AddPayroll()
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_GetDriverWeeklyPayroll");
        }

        public async Task<IList<GetHistoryResponseDto>> GetHistory()
        {
            var history =  await _dbContext.ExecuteStoredProcedureList<GetHistoryResponseDto>("usp_GetHistory");
            return history;
        }
        
        public async Task<IList<GetMonthlyChargesByFamilyIdResponseDto>> getMonthlyChargesByFamilyId()
        {
            var history =  await _dbContext.ExecuteStoredProcedureList<GetMonthlyChargesByFamilyIdResponseDto>("usp_GetMonthlyChargesByFamilyId");
            return history;
        }

        public async Task<int> InsertNextDayRouteDetails()
        {
            return await _dbContext.ExecuteStoredProcedure<int>("CreateRouteDetails");
        }

        public async Task<IList<GetRoutesResponseDto>> GetTodayUpcomingRoutes()
        {
            var todayRoutesRes = await _dbContext.ExecuteStoredProcedureList<GetRoutesResponseDto>("usp_GetTodayUpcomingRoutes");
            return todayRoutesRes;
        }

        public async Task<int> InsertStopPassedByBusOnRoute(int routeId, DateTime? routeDate, int lastPassedStop)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_InsertStopPassedByBusOnRoute",
             _parameterManager.Get("@RouteId", routeId, ParameterDirection.Input, DbType.Int32),
             _parameterManager.Get("@RouteDate", routeDate, ParameterDirection.Input, DbType.DateTime),
             _parameterManager.Get("@StopPassed", lastPassedStop, ParameterDirection.Input, DbType.Int32));
        }

        public async Task<int> RemoveExpiredTempBusDrivers()
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_RemoveExpiredTempBusDrivers");
        }
    }
}

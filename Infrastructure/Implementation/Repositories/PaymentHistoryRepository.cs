using Application.Abstraction.DataBase;
using Application.Abstraction.Repositories;
using DocumentFormat.OpenXml.Office2010.Excel;
using DTO.Request.CardknoxPaymentMethod;
using System.Data;

namespace Infrastructure.Implementation.Repositories
{
    public class PaymentHistoryRepository : IPaymentHistoryRepository
    {
        private readonly IDbContext _dbContext;
        private readonly IParameterManager _parameterManager;
        public PaymentHistoryRepository(IDbContext dbContext, IParameterManager parameterManager)
        {
            _dbContext = dbContext;
            _parameterManager = parameterManager;
        }

        public async Task<bool> AlreadyChargedThisMonth(string customerId, int month, int year)
        {
            return await _dbContext.ExecuteStoredProcedure<bool>("usp_AlreadyChargedThisMonth",
                _parameterManager.Get("CustomerId", customerId, ParameterDirection.Input, DbType.String),
                _parameterManager.Get("Month", month, ParameterDirection.Input, DbType.Int32),
                _parameterManager.Get("Year", year, ParameterDirection.Input, DbType.Int32)
            );
        }

        public async Task SaveSuccess(PaymentTransactions entity)
        {
            await _dbContext.ExecuteStoredProcedure<int>(
                "usp_SavePaymentSuccess",
                _parameterManager.Get("CustomerId", entity.CustomerId),
                _parameterManager.Get("Amount", entity.Amount),
                _parameterManager.Get("GatewayRefNum", entity.GatewayRefNum),
                _parameterManager.Get("FullResponse", entity.FullResponse),
                _parameterManager.Get("Description", entity.Description),
                _parameterManager.Get("IsManual", entity.IsManual),
        _parameterManager.Get("CheckNumber", entity.CheckNumber),
        _parameterManager.Get("CheckDate", entity.CheckDate),
        _parameterManager.Get("PaymentMethodId", entity.PaymentMethodId),
        _parameterManager.Get("CardNumber", entity.CardNumber),
        _parameterManager.Get("IsBusChange", entity.IsBusChange),
        _parameterManager.Get("ChargeId", entity.ChargeId),
        _parameterManager.Get("Status", entity.Status)
            );
        }

        public async Task SaveFailure(PaymentTransactions entity)
        {
            await _dbContext.ExecuteStoredProcedure<int>(
                "usp_SavePaymentFailure",
                _parameterManager.Get("CustomerId", entity.CustomerId),
                _parameterManager.Get("Amount", entity.Amount),
                _parameterManager.Get("ErrorMessage", entity.ErrorMessage),
                _parameterManager.Get("FullResponse", entity.FullResponse),
                _parameterManager.Get("AttemptCount", entity.AttemptCount),
                 _parameterManager.Get("Description", entity.Description),
                _parameterManager.Get("IsManual", entity.IsManual),
                _parameterManager.Get("PaymentMethodId", entity.PaymentMethodId),
                _parameterManager.Get("CardNumber", entity.CardNumber),
                _parameterManager.Get("IsBusChange", entity.IsBusChange),
                _parameterManager.Get("ChargeId", entity.ChargeId),
                _parameterManager.Get("Status", entity.Status)
            );
        }
        public async Task IncrementRetry(int Id)
        {
            await _dbContext.ExecuteStoredProcedure<int>("usp_IncrementRetry",
                _parameterManager.Get("Id", Id)
            );
        }

        public async Task<IList<PaymentTransactions>> GetFailedPayments()
        {
            return await _dbContext.ExecuteStoredProcedureList<PaymentTransactions>("usp_GetFailedPayments");
        }

        public async Task<bool> HasPendingFailure(string customerId, int month, int year)
        {
            return await _dbContext.ExecuteStoredProcedure<bool>(
                "usp_HasPendingFailure",
                _parameterManager.Get("CustomerId", customerId, ParameterDirection.Input, DbType.String),
                _parameterManager.Get("Month", month, ParameterDirection.Input, DbType.Int32),
                _parameterManager.Get("Year", year, ParameterDirection.Input, DbType.Int32)
            );
        }
    }
}

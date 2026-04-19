using Application.Abstraction.DataBase;
using Application.Abstraction.Repositories;
using Application.Common.Dtos;
using Dapper;
using DTO.Request.Payments;
using DTO.Response.Payments;
using System.Data;

namespace Infrastructure.Implementation.Repositories
{
    public class PaymentsRepository : IPaymentsRepository
    {
        private readonly IDbContext _dbContext;
        private readonly IParameterManager _parameterManager;
        public PaymentsRepository(IDbContext dbContext, IParameterManager parameterManager)
        {
            _dbContext = dbContext;
            _parameterManager = parameterManager;
        }

        public async Task<(List<GetAllTransactionsResponseDto>, int)> GetAllTransactions(string filterModel, ServerRowsRequest commonRequest, string getSort)
        {
            List<GetAllTransactionsResponseDto> transactions;
            int total = 0;
            using (var dbConnection = _dbContext.GetDbConnection())
            {

                var result = await dbConnection.QueryMultipleAsync("usp_GetAllTransactions", _dbContext.GetDapperDynamicParameters
            (_parameterManager.Get("@StartRow", commonRequest.StartRow),
              _parameterManager.Get("@EndRow", commonRequest.EndRow),
              _parameterManager.Get("@FilterModel", filterModel),
              _parameterManager.Get("@OrderBy", getSort),
              _parameterManager.Get("@SearchText", commonRequest.SearchText)
            ),
            commandType: CommandType.StoredProcedure);
                total = result.Read<int>().FirstOrDefault();
                transactions = result.Read<GetAllTransactionsResponseDto>().ToList();
                dbConnection.Close();
            }
            return (transactions, total);
        }

        public async Task<(List<GetPaymentsResponseDto>, int)> GetPayments(string filterModel, ServerRowsRequest commonRequest, string getSort, int studentId)
        {
            List<GetPaymentsResponseDto> contact;
            int total = 0;
            using (var dbConnection = _dbContext.GetDbConnection())
            {

                var result = await dbConnection.QueryMultipleAsync(
            "usp_GetPayments", _dbContext.GetDapperDynamicParameters
            (_parameterManager.Get("@StartRow", commonRequest.StartRow),
              _parameterManager.Get("@EndRow", commonRequest.EndRow),
              _parameterManager.Get("@FilterModel", filterModel),
              _parameterManager.Get("@OrderBy", getSort),
              _parameterManager.Get("@SearchText", commonRequest.SearchText),
              _parameterManager.Get("@StudentId", studentId)
            ),
            commandType: CommandType.StoredProcedure);
                total = result.Read<int>().FirstOrDefault();
                contact = result.Read<GetPaymentsResponseDto>().ToList();
                dbConnection.Close();
            }
            return (contact, total);
        }

        public async Task<int> RecodePayment(RecodePaymentRequestDto recodePaymentRequestDto)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_RecodePayment",
                 _parameterManager.Get("ChargeId", recodePaymentRequestDto.ChargeId, ParameterDirection.Input, DbType.Int32),
                 _parameterManager.Get("PaymentType", recodePaymentRequestDto.PaymentType, ParameterDirection.Input, DbType.String),
                 _parameterManager.Get("Amount", recodePaymentRequestDto.Amount, ParameterDirection.Input, DbType.String),
                 _parameterManager.Get("UserId", recodePaymentRequestDto.UserId, ParameterDirection.Input, DbType.String)
            );
        }
    }
}

using Application.Abstraction.DataBase;
using Application.Abstraction.Repositories;
using Application.Common.Dtos;
using Dapper;
using DTO.Request.CardknoxPaymentMethod;
using DTO.Response.CardknoxPaymentMethod;
using System.Data;

namespace Infrastructure.Implementation.Repositories
{
    public class CardknoxRepository : ICardknoxRepository
    {
        private readonly IDbContext _dbContext;
        private readonly IParameterManager _parameterManager;
        public CardknoxRepository(IDbContext dbContext, IParameterManager parameterManager)
        {
            _dbContext = dbContext;
            _parameterManager = parameterManager;
        }

        public async Task<int> AddCardknoxPaymentMethod(AddCardknoxPaymentMethodDto addCardknoxPaymentMethodDto)
        {
            return await _dbContext.ExecuteStoredProcedure<int>(
                "usp_AddCardknoxPaymentMethod",
                _parameterManager.Get("FamilyId", addCardknoxPaymentMethodDto.FamilyId, ParameterDirection.Input, DbType.Int32),
                _parameterManager.Get("CustomerId", addCardknoxPaymentMethodDto.CustomerId, ParameterDirection.Input, DbType.String),
                _parameterManager.Get("CardHolderName", addCardknoxPaymentMethodDto.CardHolderName, ParameterDirection.Input, DbType.String),
                _parameterManager.Get("Last4", addCardknoxPaymentMethodDto.Last4, ParameterDirection.Input, DbType.String),
                _parameterManager.Get("Token", addCardknoxPaymentMethodDto.Token, ParameterDirection.Input, DbType.String),
                _parameterManager.Get("SecurityCode", addCardknoxPaymentMethodDto.SecurityCode, ParameterDirection.Input, DbType.String),
                _parameterManager.Get("ExpDate", addCardknoxPaymentMethodDto.ExpDate, ParameterDirection.Input, DbType.String),
                _parameterManager.Get("BillingAddress", addCardknoxPaymentMethodDto.BillingAddress, ParameterDirection.Input, DbType.String),
                _parameterManager.Get("IsDefault", addCardknoxPaymentMethodDto.IsDefault, ParameterDirection.Input, DbType.Boolean),
                _parameterManager.Get("Zipcode", addCardknoxPaymentMethodDto.Zipcode, ParameterDirection.Input, DbType.String),
                _parameterManager.Get("CardType", addCardknoxPaymentMethodDto.CardType, ParameterDirection.Input, DbType.String),
                _parameterManager.Get("PaymentMethodId", addCardknoxPaymentMethodDto.PaymentMethodId, ParameterDirection.Input, DbType.String)
            );
        }

        public async Task<int> DeleteCardknoxPayment(DeleteCardknoxPaymentRequestDto deleteCardknoxPaymentRequestDto)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_DeleteCardknoxPayment",
           _parameterManager.Get("Id", deleteCardknoxPaymentRequestDto.Id, ParameterDirection.Input, DbType.Int32)
           );
        }

        public async Task<(List<GetCardknoxPaymentMethodByFamilyIdResponseDto>, int)> GetCardknoxPaymentMethodByFamilyId(string filterModel, ServerRowsRequest commonRequest, string getSort, string familyId)
        {
            List<GetCardknoxPaymentMethodByFamilyIdResponseDto> contact;
            int total = 0;
            using (var dbConnection = _dbContext.GetDbConnection())
            {
                var result = await dbConnection.QueryMultipleAsync(
                "usp_GetCardknoxPaymentMethodByFamilyId", _dbContext.GetDapperDynamicParameters
                    (_parameterManager.Get("@StartRow", commonRequest.StartRow),
                     _parameterManager.Get("@EndRow", commonRequest.EndRow),
                     _parameterManager.Get("@FilterModel", filterModel),
                     _parameterManager.Get("@OrderBy", getSort),
                     _parameterManager.Get("@SearchText", commonRequest.SearchText),
                      _parameterManager.Get("@FamilyId", familyId)
                    ),
                commandType: CommandType.StoredProcedure);
                total = result.Read<int>().FirstOrDefault();
                contact = result.Read<GetCardknoxPaymentMethodByFamilyIdResponseDto>().ToList();
                dbConnection.Close();
            }
            return (contact, total);
        }
        public async Task<(List<GetTransactionByCustomerIdResponseDto>, int)> GetTransactionByCustomerId(string filterModel, ServerRowsRequest commonRequest, string getSort, string customerId)
        {
            List<GetTransactionByCustomerIdResponseDto> contact;
            int total = 0;
            using (var dbConnection = _dbContext.GetDbConnection())
            {
                var result = await dbConnection.QueryMultipleAsync(
                "usp_GetTransactionByCustomerId", _dbContext.GetDapperDynamicParameters
                    (_parameterManager.Get("@StartRow", commonRequest.StartRow),
                     _parameterManager.Get("@EndRow", commonRequest.EndRow),
                     _parameterManager.Get("@FilterModel", filterModel),
                     _parameterManager.Get("@OrderBy", getSort),
                     _parameterManager.Get("@SearchText", commonRequest.SearchText),
                      _parameterManager.Get("@CustomerId", customerId)
                    ),
                commandType: CommandType.StoredProcedure);
                total = result.Read<int>().FirstOrDefault();
                contact = result.Read<GetTransactionByCustomerIdResponseDto>().ToList();
                dbConnection.Close();
            }
            return (contact, total);
        }

        public async Task<IList<GetPaymentMethodResponseDto>> GetPaymentMethod(string cusomerId)
        {
            return await _dbContext.ExecuteStoredProcedureList<GetPaymentMethodResponseDto>("usp_GetPaymentMethod",
            _parameterManager.Get("@CustomerId", cusomerId, ParameterDirection.Input, DbType.String));
        }

        public async Task<IList<GetPaymentMethodListByFamilyIdResponseDto>> GetPaymentMethodListByFamilyId(int familyId)
        {
            return await _dbContext.ExecuteStoredProcedureList<GetPaymentMethodListByFamilyIdResponseDto>("usp_GetPaymentMethodListByFamilyId",
           _parameterManager.Get("@FamilyId", familyId, ParameterDirection.Input, DbType.Int64));
        }
    }
}

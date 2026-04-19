using Application.Abstraction.DataBase;
using Application.Abstraction.Repositories;
using DTO.Request.Notification;
using DTO.Response.Notification;
using System.Data;

namespace Infrastructure.Implementation.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly IDbContext _dbContext;
        private readonly IParameterManager _parameterManager;
        public NotificationRepository(IDbContext dbContext, IParameterManager parameterManager)
        {
            _dbContext = dbContext;
            _parameterManager = parameterManager;
        }
        public async Task<IList<GetBulkMessagesListResponseDto>> GetBulkMessages(SaveBulkMessageRequestDto saveBulkMessageRequestDto, int messageId)
        {
            return await _dbContext.ExecuteStoredProcedureList<GetBulkMessagesListResponseDto>(
                "usp_GetBulkMessages",

                _parameterManager.Get("RecipientType", saveBulkMessageRequestDto.RecipientType,ParameterDirection.Input,DbType.String),
                _parameterManager.Get("MessageType", saveBulkMessageRequestDto.MessageType,ParameterDirection.Input,DbType.String),
                _parameterManager.Get("MessageId", messageId, ParameterDirection.Input,DbType.String)
            );
        }

        public async Task<int> SaveBulkMessage(SaveBulkMessageRequestDto saveBulkMessageRequestDto)
        {

          return await _dbContext.ExecuteStoredProcedure<int>("usp_SaveBulkMessage",
                _parameterManager.Get("RecipientType", saveBulkMessageRequestDto.RecipientType, ParameterDirection.Input, DbType.String),
                _parameterManager.Get("MessageBody", saveBulkMessageRequestDto.MessageBody, ParameterDirection.Input, DbType.String),
                _parameterManager.Get("MessageType", saveBulkMessageRequestDto.MessageType, ParameterDirection.Input, DbType.String),
                _parameterManager.Get("ScheduledDateTime", saveBulkMessageRequestDto.ScheduledDateTime, ParameterDirection.Input, DbType.DateTime),
                _parameterManager.Get("CreatedBy", saveBulkMessageRequestDto.CreatedBy, ParameterDirection.Input, DbType.Int32)
                
            );

        }
    }
}

using Application.Common.Dtos;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.Payments;
using MediatR;

namespace Application.Handler.Payments.Queries.GetAllTransactions
{
    public class GetAllTransactionsQuery : ServerRowsRequest, IRequest<CommonResultResponseDto<PaginatedList<GetAllTransactionsResponseDto>>>
    {
        public ServerRowsRequest CommonRequest { get; set; }
    }
}


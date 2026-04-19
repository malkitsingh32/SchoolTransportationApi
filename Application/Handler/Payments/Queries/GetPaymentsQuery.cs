using Application.Common.Dtos;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.Payments;
using MediatR;

namespace Application.Handler.Payments.Queries
{
    public class GetPaymentsQuery : ServerRowsRequest, IRequest<CommonResultResponseDto<PaginatedList<GetPaymentsResponseDto>>>
    {
        public ServerRowsRequest CommonRequest { get; set; }
        public int StudentId { get; set; }
    }
}

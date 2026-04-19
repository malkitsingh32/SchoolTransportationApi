using Application.Common.Dtos;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.Charges;
using MediatR;

namespace Application.Handler.Charges.Queries
{
    public class GetChargesQuery : ServerRowsRequest, IRequest<CommonResultResponseDto<PaginatedList<GetChargesResponseDto>>>
    {
        public ServerRowsRequest CommonRequest { get; set; }
        public int StudentId { get; set; }
    }
}

using Application.Common.Dtos;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.Routes;
using MediatR;

namespace Application.Handler.Routes.Queries.GetHistoryByTab
{
    public class GetHistoryByTabQuery : ServerRowsRequest, IRequest<CommonResultResponseDto<PaginatedList<GetHistoryByTabResponseDto>>>
    {
        public ServerRowsRequest CommonRequest { get; set; }
        public string Tab { get; set; }
        public int Id { get; set; }
    }
}

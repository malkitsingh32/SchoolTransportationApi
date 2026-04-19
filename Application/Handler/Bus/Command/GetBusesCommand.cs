using Application.Common.Dtos;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.Bus;
using MediatR;

namespace Application.Handler.Bus.Command
{
    public class GetBusesCommand : ServerRowsRequest, IRequest<CommonResultResponseDto<PaginatedList<GetBusesResponseDto>>>
    {
        public ServerRowsRequest CommonRequest { get; set; }
        public int RouteId { get; set; }
    }
}

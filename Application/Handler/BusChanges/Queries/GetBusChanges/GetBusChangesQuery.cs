using Application.Common.Dtos;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.BusChanges;
using MediatR;

namespace Application.Handler.BusChanges.Queries.GetBusChanges
{
    public class GetBusChangesQuery : ServerRowsRequest, IRequest<CommonResultResponseDto<PaginatedList<GetBusChangesDto>>>
    {
        public int? StudentId { get; set; }
        public ServerRowsRequest CommonRequest { get; set; }
    }
}

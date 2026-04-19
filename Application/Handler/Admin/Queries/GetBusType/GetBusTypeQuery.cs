using Application.Common.Dtos;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.SystemValues;
using MediatR;

namespace Application.Handler.Admin.Queries.GetBusType
{
    public class GetBusTypeQuery : ServerRowsRequest, IRequest<CommonResultResponseDto<PaginatedList<GetBusTypeResponseDto>>>
    {
        public ServerRowsRequest CommonRequest { get; set; }
    }
}

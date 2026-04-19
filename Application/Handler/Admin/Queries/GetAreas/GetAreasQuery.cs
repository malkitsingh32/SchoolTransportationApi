using Application.Common.Dtos;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.SystemValues;
using MediatR;

namespace Application.Handler.Admin.Queries.GetAreas
{
    public class GetAreasQuery : ServerRowsRequest, IRequest<CommonResultResponseDto<PaginatedList<GetAreasResponseDto>>>
    {
        public ServerRowsRequest CommonRequest { get; set; }
    }
}

using Application.Common.Dtos;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.SystemValues;
using MediatR;

namespace Application.Handler.Admin.Queries.GetSearchLocation
{
    public class GetSearchLocationQuery : ServerRowsRequest, IRequest<CommonResultResponseDto<PaginatedList<GetSearchLocationResponseDto>>>
    {
        public ServerRowsRequest CommonRequest { get; set; }
    }
}

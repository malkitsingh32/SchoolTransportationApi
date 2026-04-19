using Application.Common.Dtos;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.SystemValues;
using MediatR;

namespace Application.Handler.Admin.Queries.GetDriverType
{
    public class GetDriverTypeQuery : ServerRowsRequest, IRequest<CommonResultResponseDto<PaginatedList<GetDriverTypeResponseDto>>>
    {
        public ServerRowsRequest CommonRequest { get; set; }
    }
}

using Application.Common.Dtos;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.SystemValues;
using MediatR;

namespace Application.Handler.Admin.Queries.GetDistrict
{
    public class GetDistrictQuery : ServerRowsRequest, IRequest<CommonResultResponseDto<PaginatedList<GetDistrictResponseDto>>>
    {
        public ServerRowsRequest CommonRequest { get; set; }
    }
}

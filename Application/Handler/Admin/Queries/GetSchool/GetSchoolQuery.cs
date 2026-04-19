using Application.Common.Dtos;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.SystemValues;
using MediatR;

namespace Application.Handler.Admin.Queries.GetSchool
{
    public class GetSchoolQuery : ServerRowsRequest, IRequest<CommonResultResponseDto<PaginatedList<GetSchoolsResponseDto>>>
    {
        public ServerRowsRequest CommonRequest { get; set; }
    }
}

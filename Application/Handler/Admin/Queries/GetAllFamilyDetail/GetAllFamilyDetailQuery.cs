

using Application.Common.Dtos;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.SystemValues;
using MediatR;

namespace Application.Handler.Admin.Queries.GetAllFamilyDetail
{
    public class GetAllFamilyDetailQuery : ServerRowsRequest, IRequest<CommonResultResponseDto<PaginatedList<GetAllFamilyDetailResponseDto>>>
    {
        public ServerRowsRequest CommonRequest { get; set; }
    }
}

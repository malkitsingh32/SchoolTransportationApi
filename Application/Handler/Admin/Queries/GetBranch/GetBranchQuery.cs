
using Application.Common.Dtos;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.SystemValues;
using MediatR;

namespace Application.Handler.Admin.Queries.GetBranch
{
    public class GetBranchQuery : ServerRowsRequest, IRequest<CommonResultResponseDto<PaginatedList<GetBranchResponseDto>>>
    {
        public ServerRowsRequest CommonRequest { get; set; }
    }
}

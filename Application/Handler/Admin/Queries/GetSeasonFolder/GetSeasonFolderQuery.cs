using Application.Common.Dtos;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.SystemValues;
using MediatR;

namespace Application.Handler.Admin.Queries.GetSeasonFolder
{
    public class GetSeasonFolderQuery : ServerRowsRequest, IRequest<CommonResultResponseDto<PaginatedList<GetSeasonFolderResponseDto>>>
    {
        public ServerRowsRequest CommonRequest { get; set; }
    }
}

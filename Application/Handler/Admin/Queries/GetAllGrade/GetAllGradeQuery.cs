using Application.Common.Dtos;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.SystemValues;
using MediatR;

namespace Application.Handler.Admin.Queries.GetAllGrade
{
    public class GetAllGradeQuery : ServerRowsRequest, IRequest<CommonResultResponseDto<PaginatedList<GetAllGradeResponseDto>>>
    {
        public ServerRowsRequest CommonRequest { get; set; }
    }
}

using Application.Common.Dtos;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.SystemValues;
using MediatR;

namespace Application.Handler.Admin.Queries.GetGender
{
    public class GetGenderQuery : ServerRowsRequest, IRequest<CommonResultResponseDto<PaginatedList<GetGenderResponseDto>>>
    {
        public ServerRowsRequest CommonRequest { get; set; }
    }
}

using Application.Common.Dtos;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.User;
using MediatR;

namespace Application.Handler.Admin.Queries.GetSchoolYears
{
    public class GetSchoolYearsQuery : ServerRowsRequest, IRequest<CommonResultResponseDto<PaginatedList<GetSchoolYearsResponseDto>>>
    {
        public ServerRowsRequest CommonRequest { get; set; }
    }
}

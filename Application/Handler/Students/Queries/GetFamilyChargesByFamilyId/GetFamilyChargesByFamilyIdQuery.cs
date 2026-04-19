using Application.Common.Dtos;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.Students;
using MediatR;

namespace Application.Handler.Students.Queries.GetFamilyChargesByFamilyId
{
    public class GetFamilyChargesByFamilyIdQuery : ServerRowsRequest, IRequest<CommonResultResponseDto<PaginatedList<GetFamilyChargesByFamilyIdResponseDto>>>
    {
        public string FamilyId { get; set; }
        public ServerRowsRequest CommonRequest { get; set; }

    }
}

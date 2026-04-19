using Application.Common.Dtos;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.Students;
using MediatR;

namespace Application.Handler.Students.Queries.GetStudentsByFamilyId
{
    public class GetStudentsByFamilyIdQuery : ServerRowsRequest, IRequest<CommonResultResponseDto<PaginatedList<GetStudentsByFamilyIdResponseDto>>>
    {
        public string FamilyId { get; set; }
        public ServerRowsRequest CommonRequest { get; set; }

    }
}
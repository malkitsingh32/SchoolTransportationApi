using Application.Common.Dtos;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.Students;
using MediatR;

namespace Application.Handler.Students.Command.GetUnassignedStudents
{
    public class GetUnassignedStudentsCommand : ServerRowsRequest, IRequest<CommonResultResponseDto<PaginatedList<GetStudentsResponseDto>>>
    {
        public ServerRowsRequest CommonRequest { get; set; }
        public int RouteId { get; set; }
        public int StreetId { get; set; }
        public int FamilyId { get; set; }
        public string? RouteTypeIds { get; set; }
        public int? GenderId { get; set; }

    }
}

using Application.Common.Dtos;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.Students;
using MediatR;

namespace Application.Handler.Students.Command
{
    public class GetStudentsCommand : ServerRowsRequest, IRequest<CommonResultResponseDto<PaginatedList<GetStudentsResponseDto>>>
    {
        public ServerRowsRequest CommonRequest { get; set; }
        public int RouteId { get; set; }
        public int StreetId { get; set; }
        public int FamilyId { get; set; }
        public string? NtId { get; set; }
        public string? Dob { get; set; }
        public string? District { get; set; }
        public string? SchoolStudentId { get; set; }
        public string? SchoolId { get; set; }
        public string? Grade { get; set; }
        public string? Gender { get; set; }

    }
}

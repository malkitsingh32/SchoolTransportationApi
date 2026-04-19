using Application.Common.Dtos;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.Students;
using MediatR;

namespace Application.Handler.Students.Command.GetStudentsForBulkRoutes
{
    public class GetStudentsForBulkRoutesCommand : ServerRowsRequest, IRequest<CommonResultResponseDto<PaginatedList<GetStudentsResponseDto>>>
    {
        public ServerRowsRequest CommonRequest { get; set; }
        public string? Area { get; set; }
        public string? School { get; set; }
        public string? Grade { get; set; }
        public int? Gender { get; set; }
        public string? Building { get; set; }
        public string? Branch { get; set; }
        public string? Street { get; set; }
        public string? UniqueId { get; set; }
        public int UserId { get; set; }
        public int? RouteId { get; set; }


    }
}

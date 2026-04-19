using Application.Common.Dtos;
using Application.Common.Response;
using DTO.Response.Students;
using DTO.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handler.BusChanges.Command.GetBusChangeStudentsList
{
    public class GetBusChangeStudentsListCommand : ServerRowsRequest, IRequest<CommonResultResponseDto<PaginatedList<GetStudentsResponseDto>>>
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

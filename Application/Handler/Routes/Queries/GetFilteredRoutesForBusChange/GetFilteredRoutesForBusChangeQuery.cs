using DTO.Response.Admin;
using DTO.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.Request.Routes;
using DTO.Response.Routes;

namespace Application.Handler.Routes.Queries.GetFilteredRoutesForBusChange
{
    public class GetFilteredRoutesForBusChangeQuery : IRequest<CommonResultResponseDto<IList<GetRoutesListResponseDto>>>
    {
        public int? Area { get; set; }
        public int? SchoolId { get; set; }
        public int? Gender { get; set; }
        public int? Grade { get; set; }
        public int? Branch { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Include { get; set; }
        public string? StartFrom { get; set; }
    }
}

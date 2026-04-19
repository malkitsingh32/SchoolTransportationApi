using DTO.Response.Admin;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handler.Admin.Queries.GetRouteTypeDays
{
    public class GetRouteTypeDaysQuery : IRequest<IList<RouteTypeDayDto>>
    {
        public int RouteTypeId { get; set; }
    }
}

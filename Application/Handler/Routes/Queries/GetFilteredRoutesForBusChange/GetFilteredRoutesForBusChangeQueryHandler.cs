using Application.Abstraction.Services;
using Application.Handler.Routes.Queries.GetAreasBySchoolAndGrade;
using DTO.Request.Routes;
using DTO.Response.Admin;
using DTO.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapster;
using DTO.Response.Routes;

namespace Application.Handler.Routes.Queries.GetFilteredRoutesForBusChange
{
    public class GetFilteredRoutesForBusChangeQueryHandler : IRequestHandler<GetFilteredRoutesForBusChangeQuery, CommonResultResponseDto<IList<GetRoutesListResponseDto>>>
    {
        private readonly IRoutesService _routesService;

        public GetFilteredRoutesForBusChangeQueryHandler(IRoutesService routesService)
        {
            _routesService = routesService;
        }

        public async Task<CommonResultResponseDto<IList<GetRoutesListResponseDto>>> Handle(GetFilteredRoutesForBusChangeQuery getFilteredRoutesForBusChangeQuery, CancellationToken cancellationToken)
        {
            return await _routesService.GetFilteredRoutesForBusChange(getFilteredRoutesForBusChangeQuery.Adapt<GetFilteredRoutesForBusChangeRequestDto>());
        }
    }
}

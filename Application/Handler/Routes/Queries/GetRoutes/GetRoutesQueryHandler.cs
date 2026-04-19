using Application.Abstraction.Services;
using Application.Common.Dtos;
using Application.Common.Interfaces.Common;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.Routes;
using Mapster;
using MediatR;

namespace Application.Handler.Routes.Queries.GetRoutes
{
    public class GetRoutesQueryHandler : IRequestHandler<GetRoutesQuery, CommonResultResponseDto<PaginatedList<GetRoutesResponseDto>>>
    {
        private readonly IRoutesService _routesService;
        private readonly IRequestBuilder _requestBuilder;

        public GetRoutesQueryHandler(IRoutesService routesService, IRequestBuilder requestBuilder)
        {
            _routesService = routesService;
            _requestBuilder = requestBuilder;

        }

        public async Task<CommonResultResponseDto<PaginatedList<GetRoutesResponseDto>>> Handle(GetRoutesQuery request, CancellationToken cancellationToken)
        {
            var filterModel = _requestBuilder.GetRequestBuilder(request.CommonRequest);
            return await _routesService.GetRoutes(filterModel.GetFilters(), request.CommonRequest, filterModel.GetSorts(), request.Adapt<GetRoutesRequestDto>());
        }

    }
    public class GetRoutesRequestDto
    {
        public ServerRowsRequest CommonRequest { get; set; }
        public int BusDetailId { get; set; }
        public int StreetId { get; set; }
        public int DriverId { get; set; }
        public int StudentId { get; set; }
        public int SeasonFolderId { get; set; }
        public string? Grade { get; set; }
        public string? Role { get; set; }
        public int? IsActiveRoutes { get; set; }
        public int? Gender { get; set; }
        public string? School { get; set; }
        public string? RouteId { get; set; }
    }
}

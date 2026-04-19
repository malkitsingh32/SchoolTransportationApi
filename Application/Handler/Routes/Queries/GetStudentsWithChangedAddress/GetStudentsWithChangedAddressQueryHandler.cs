using Application.Abstraction.Services;
using Application.Common.Dtos;
using Application.Common.Interfaces.Common;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.Routes;
using MediatR;

namespace Application.Handler.Routes.Queries.GetStudentsWithChangedAddress
{
    public class GetStudentsWithChangedAddressQueryHandler : IRequestHandler<GetStudentsWithChangedAddressQuery, CommonResultResponseDto<PaginatedList<GetStudentsWithChangedAddressResponseDto>>>
    {
        private readonly IRoutesService _routesService;
        private readonly IRequestBuilder _requestBuilder;

        public GetStudentsWithChangedAddressQueryHandler(IRoutesService routesService, IRequestBuilder requestBuilder)
        {
            _routesService = routesService;
            _requestBuilder = requestBuilder;

        }

        public async Task<CommonResultResponseDto<PaginatedList<GetStudentsWithChangedAddressResponseDto>>> Handle(GetStudentsWithChangedAddressQuery request, CancellationToken cancellationToken)
        {
            var filterModel = _requestBuilder.GetRequestBuilder(request.CommonRequest);
            return await _routesService.GetStudentsWithChangedAddress(filterModel.GetFilters(), request.CommonRequest, filterModel.GetSorts(), request.RouteTypeIds, request.GenderId);
        }
        public class GetStudentsWithChangedAddressRequestDto
        {
            public ServerRowsRequest CommonRequest { get; set; }
            public string? RouteTypeIds { get; set; }
            public int? GenderId { get; set; }

        }
    }
 
}

using Application.Abstraction.Services;
using Application.Common.Dtos;
using Application.Common.Interfaces.Common;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.BusChanges;
using MediatR;

namespace Application.Handler.BusChanges.Queries.GetBusChanges
{
    public class GetBusChangesQueryHandler : IRequestHandler<GetBusChangesQuery, CommonResultResponseDto<PaginatedList<GetBusChangesDto>>>
    {
        private readonly IBusChangesService _busChangesService;
        private readonly IRequestBuilder _requestBuilder;
        public GetBusChangesQueryHandler(IBusChangesService busChangesService, IRequestBuilder requestBuilder)
        {
            _busChangesService = busChangesService;
            _requestBuilder = requestBuilder;
        }

        public async Task<CommonResultResponseDto<PaginatedList<GetBusChangesDto>>> Handle(GetBusChangesQuery request, CancellationToken cancellationToken)
        {
            var filterModel = _requestBuilder.GetRequestBuilder(request.CommonRequest);
            return await _busChangesService.GetBusChanges(filterModel.GetFilters(), request.CommonRequest, filterModel.GetSorts(),request.StudentId);
        }
    }
    public class GetBusChangesRequestDto
    {
        public ServerRowsRequest CommonRequest { get; set; }
        public int? StudentId { get; set; }

    }
}

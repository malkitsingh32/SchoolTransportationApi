using Application.Abstraction.Services;
using Application.Common.Dtos;
using Application.Common.Interfaces.Common;
using Application.Common.Response;
using Application.Handler.Students.Queries.GetFamilyChargesByFamilyId;
using DTO.Response.Students;
using DTO.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.Response.BusChanges;

namespace Application.Handler.BusChanges.Command.GetBusChangeChargesByFamilyId
{
    public class GetBusChangeChargesByFamilyIdQueryHandler : IRequestHandler<GetBusChangeChargesByFamilyIdQuery, CommonResultResponseDto<PaginatedList<GetBusChargesByFamilyIdResponseDto>>>
    {
        private readonly IBusChangesService _busChangesService;
        private readonly IRequestBuilder _requestBuilder;
        public GetBusChangeChargesByFamilyIdQueryHandler(IBusChangesService busChangesService, IRequestBuilder requestBuilder)
        {
            _busChangesService = busChangesService;
            _requestBuilder = requestBuilder;
        }

        public async Task<CommonResultResponseDto<PaginatedList<GetBusChargesByFamilyIdResponseDto>>> Handle(GetBusChangeChargesByFamilyIdQuery request, CancellationToken cancellationToken)
        {
            var filterModel = _requestBuilder.GetRequestBuilder(request.CommonRequest);
            return await _busChangesService.GetBusChargesByFamilyId(filterModel.GetFilters(), request.CommonRequest, filterModel.GetSorts(), request.FamilyId);
        }
    }
    public class GetBusChargesByFamilyIdRequestDto
    {
        public ServerRowsRequest CommonRequest { get; set; }
        public string FamilyId { get; set; }
    }
}

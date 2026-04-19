using Application.Abstraction.Services;
using Application.Common.Dtos;
using Application.Common.Interfaces.Common;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.Charges;
using MediatR;

namespace Application.Handler.Charges.Queries
{
    public class GetChargesQueryHandler : IRequestHandler<GetChargesQuery, CommonResultResponseDto<PaginatedList<GetChargesResponseDto>>>
    {
        private readonly IChargesService _chargesService;
        private readonly IRequestBuilder _requestBuilder;

        public GetChargesQueryHandler(IChargesService chargesService, IRequestBuilder requestBuilder)
        {
            _chargesService = chargesService;
            _requestBuilder = requestBuilder;

        }
        public async Task<CommonResultResponseDto<PaginatedList<GetChargesResponseDto>>> Handle(GetChargesQuery request, CancellationToken cancellationToken)
        {
            var filterModel = _requestBuilder.GetRequestBuilder(request.CommonRequest);
            return await _chargesService.GetCharges(filterModel.GetFilters(), request.CommonRequest, filterModel.GetSorts(), request.StudentId);
        }
    }

    public class GetChargesRequestDto
    {
        public ServerRowsRequest CommonRequest { get; set; }
        public int StudentId { get; set; }

    }
}

using Application.Abstraction.Services;
using Application.Common.Dtos;
using Application.Common.Interfaces.Common;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.CardknoxPaymentMethod;
using MediatR;

namespace Application.Handler.CardKnoxPayment.Queries.GetCardknoxPaymentMethodByFamilyId
{
    internal class GetCardknoxPaymentMethodByFamilyIdQueryHandler : IRequestHandler<GetCardknoxPaymentMethodByFamilyIdQuery, CommonResultResponseDto<PaginatedList<GetCardknoxPaymentMethodByFamilyIdResponseDto>>>
    {
        private readonly ICardknoxService  _cardknoxService;
        private readonly IRequestBuilder _requestBuilder;
        public GetCardknoxPaymentMethodByFamilyIdQueryHandler(ICardknoxService cardknoxService, IRequestBuilder requestBuilder)
        {
            _cardknoxService = cardknoxService;
            _requestBuilder = requestBuilder;
        }

        public async Task<CommonResultResponseDto<PaginatedList<GetCardknoxPaymentMethodByFamilyIdResponseDto>>> Handle(GetCardknoxPaymentMethodByFamilyIdQuery request, CancellationToken cancellationToken)
        {
            var filterModel = _requestBuilder.GetRequestBuilder(request.CommonRequest);
            return await _cardknoxService.GetCardknoxPaymentMethodByFamilyId(filterModel.GetFilters(), request.CommonRequest, filterModel.GetSorts(), request.FamilyId);
        }
    }
    public class GetCardknoxPaymentMethodByFamilyIdRequestDto
    {
        public ServerRowsRequest CommonRequest { get; set; }
        public string FamilyId { get; set; }
    }
}

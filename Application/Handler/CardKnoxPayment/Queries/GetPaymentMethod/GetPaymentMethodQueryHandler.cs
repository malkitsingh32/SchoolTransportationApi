using Application.Abstraction.Services;
using Application.Handler.Drivers.Queries;
using DTO.Response.Driver;
using DTO.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.Response.CardknoxPaymentMethod;

namespace Application.Handler.CardKnoxPayment.Queries.GetPaymentMethod
{
    public class GetPaymentMethodQueryHandler : IRequestHandler<GetPaymentMethodQuery, CommonResultResponseDto<IList<GetPaymentMethodResponseDto>>>
    {
        private readonly IDriversService _driversService;
        private readonly ICardknoxService _cardknoxService;

        public GetPaymentMethodQueryHandler(ICardknoxService cardknoxService, IDriversService driversService)
        {
            _driversService = driversService;
            _cardknoxService = cardknoxService;
        }
        public async Task<CommonResultResponseDto<IList<GetPaymentMethodResponseDto>>> Handle(GetPaymentMethodQuery request, CancellationToken cancellationToken)
        {
            return await _cardknoxService.GetPaymentMethod(request.CusomerId);
        }
    }
}

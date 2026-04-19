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
    public class GetPaymentMethodQuery : IRequest<CommonResultResponseDto<IList<GetPaymentMethodResponseDto>>>
    {
        public string CusomerId { get; set; }
    }
}

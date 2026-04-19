using DTO.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handler.Admin.Command.UpdateRouteTypeExclusivePay
{
    public class UpdateRouteTypeExclusivePayCommand : IRequest<CommonResultResponseDto<string>>
    {
        public int Id { get; set; }
        public bool ExclusivelyPay { get; set; }
    }
}

using DTO.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handler.Admin.Command.UpdateBusCharge
{
    public class UpdateBusChargeCommand : IRequest<CommonResultResponseDto<string>>
    {
        public int BusChargeId { get; set; }
        public decimal Amount { get; set; }
    }
}

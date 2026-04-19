using DTO.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handler.Admin.Command.UpdateDayStatus
{
    public class UpdateDayStatusCommand : IRequest<CommonResultResponseDto<int>>
    {
        public int Id { get; set; }

        public bool IsActive { get; set; }
    }
}

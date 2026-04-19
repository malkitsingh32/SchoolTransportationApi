using Application.Abstraction.Services;
using DTO.Request.Admin;
using DTO.Response;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handler.Admin.Command.UpdateBusCharge
{
    public class UpdateBusChargeCommandHandler : IRequestHandler<UpdateBusChargeCommand, CommonResultResponseDto<string>>
    {
        private readonly IAdminService _adminService;

        public UpdateBusChargeCommandHandler(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<CommonResultResponseDto<string>> Handle(UpdateBusChargeCommand request, CancellationToken cancellationToken)
        {
            return await _adminService.UpdateBusCharge(request.Adapt<UpdateBusChargeRequestDto>());
        }
    }
}

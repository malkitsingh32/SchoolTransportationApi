using Application.Abstraction.Services;
using DTO.Request.Admin;
using DTO.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handler.Admin.Command.UpdateRouteTypeExclusivePay
{
    public class UpdateRouteTypeExclusivePayCommandHandler : IRequestHandler<UpdateRouteTypeExclusivePayCommand, CommonResultResponseDto<string>>
    {
        private readonly IAdminService _adminService;

        public UpdateRouteTypeExclusivePayCommandHandler(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<CommonResultResponseDto<string>> Handle(
            UpdateRouteTypeExclusivePayCommand request,
            CancellationToken cancellationToken)
        {
            var result = await _adminService.UpdateRouteTypeExclusivePay(
                new UpdateRouteTypeExclusivePayRequestDto
                {
                    Id = request.Id,
                    ExclusivelyPay = request.ExclusivelyPay
                });

            return result;
        }
    }
}

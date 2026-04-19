using Application.Abstraction.Services;
using DTO.Request.Admin;
using DTO.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handler.Admin.Command.UpdateDayStatus
{
    public class UpdateDayStatusCommandHandler : IRequestHandler<UpdateDayStatusCommand, CommonResultResponseDto<int>>
    {
        private readonly IAdminService _adminService;

        public UpdateDayStatusCommandHandler(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<CommonResultResponseDto<int>> Handle(UpdateDayStatusCommand request, CancellationToken cancellationToken)
        {
            var dto = new UpdateDayStatusRequestDto
            {
                Id = request.Id,
                IsActive = request.IsActive
            };

            return await _adminService.UpdateDayStatus(dto);
        }
    }
}

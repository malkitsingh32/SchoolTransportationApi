using Application.Abstraction.Services;
using DTO.Request.Admin;
using DTO.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handler.Admin.Command.AddUpdatePredefinedColor
{
    public class AddUpdatePredefinedColorCommandHandler: IRequestHandler<AddUpdatePredefinedColorCommand, CommonResultResponseDto<string>>
    {
        private readonly IAdminService _adminService;

        public AddUpdatePredefinedColorCommandHandler(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<CommonResultResponseDto<string>> Handle( AddUpdatePredefinedColorCommand request, CancellationToken cancellationToken)
        {
            var dto = new AddUpdatePredefinedColorDto
            {
                Id = request.Id,
                ColorName = request.ColorName,
                ColorCode = request.ColorCode
            };

            return await _adminService.AddUpdatePredefinedColor(dto);
        }
    }
}

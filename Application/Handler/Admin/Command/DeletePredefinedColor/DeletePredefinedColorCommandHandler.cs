using Application.Abstraction.Services;
using DTO.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handler.Admin.Command.DeletePredefinedColor
{
    public class DeletePredefinedColorCommandHandler: IRequestHandler<DeletePredefinedColorCommand, CommonResultResponseDto<string>>
    {
        private readonly IAdminService _adminService;

        public DeletePredefinedColorCommandHandler(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<CommonResultResponseDto<string>> Handle(
            DeletePredefinedColorCommand request,
            CancellationToken cancellationToken)
        {
            return await _adminService.DeletePredefinedColor(request.Id);
        }
    }
}

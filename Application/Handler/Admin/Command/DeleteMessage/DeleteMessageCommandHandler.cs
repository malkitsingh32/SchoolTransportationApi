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

namespace Application.Handler.Admin.Command.DeleteMessage
{
    public class DeleteMessageCommandHandler : IRequestHandler<DeleteMessageCommand, CommonResultResponseDto<string>>
    {

        private readonly IAdminService _adminService;

        public DeleteMessageCommandHandler(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<CommonResultResponseDto<string>> Handle(
            DeleteMessageCommand request,
            CancellationToken cancellationToken)
        {
            return await _adminService.DeleteMessage(request.Adapt<DeleteMessageRequestDto>());
        }
    }
}

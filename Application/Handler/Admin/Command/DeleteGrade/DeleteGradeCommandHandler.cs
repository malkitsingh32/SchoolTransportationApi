using Application.Abstraction.Services;
using DTO.Request.Admin;
using DTO.Response;
using Mapster;
using MediatR;

namespace Application.Handler.Admin.Command.DeleteGrade
{
    public class DeleteGradeCommandHandler : IRequestHandler<DeleteGradeCommand, CommonResultResponseDto<string>>
    {
        private readonly IAdminService _adminService;
        public DeleteGradeCommandHandler(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<CommonResultResponseDto<string>> Handle(DeleteGradeCommand deleteGradeCommand, CancellationToken cancellationToken)
        {
            return await _adminService.DeleteGrade(deleteGradeCommand.Adapt<DeleteGradeRequestDto>());
        }
    }
}

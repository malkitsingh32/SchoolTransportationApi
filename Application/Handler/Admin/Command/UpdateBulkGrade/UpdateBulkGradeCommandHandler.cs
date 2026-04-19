using Application.Abstraction.Services;
using DTO.Request.Admin;
using DTO.Response;
using Mapster;
using MediatR;

namespace Application.Handler.Admin.Command.UpdateBulkGrade
{
    public class UpdateBulkGradeCommandHandler : IRequestHandler<UpdateBulkGradeCommand, CommonResultResponseDto<string>>
    {
        private readonly IAdminService _adminService;
        public UpdateBulkGradeCommandHandler(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<CommonResultResponseDto<string>> Handle(UpdateBulkGradeCommand updateBulkGradeCommand, CancellationToken cancellationToken)
        {
            return await _adminService.UpdateBulkGrade(updateBulkGradeCommand.Adapt<UpdateBulkGradeDto>());
        }
    }
}

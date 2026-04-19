using Application.Abstraction.Services;
using DTO.Request.Admin;
using DTO.Response;
using Mapster;
using MediatR;

namespace Application.Handler.Admin.Command.AddUpdateGrade
{
    public class AddUpdateGradeCommandHandler : IRequestHandler<AddUpdateGradeCommand, CommonResultResponseDto<string>>
    {
        private readonly IAdminService _adminService;
        public AddUpdateGradeCommandHandler(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<CommonResultResponseDto<string>> Handle(AddUpdateGradeCommand addUpdateGradeCommand, CancellationToken cancellationToken)
        {
            return await _adminService.AddUpdateGrade(addUpdateGradeCommand.Adapt<AddUpdateGradeRequestDto>());
        }
    }
}

using Application.Abstraction.Services;
using DTO.Response;
using MediatR;

namespace Application.Handler.Admin.Command.DeleteSchoolYears
{
    public class DeleteSchoolYearsCommandHandler : IRequestHandler<DeleteSchoolYearsCommand, CommonResultResponseDto<string>>
    {
        private readonly IAdminService _adminService;
        public DeleteSchoolYearsCommandHandler(IAdminService adminService)
        {
            _adminService = adminService;
        }
        public async Task<CommonResultResponseDto<string>> Handle(DeleteSchoolYearsCommand deleteSchoolYearsCommand, CancellationToken cancellationToken)
        {
            return await _adminService.DeleteSchoolYears(deleteSchoolYearsCommand.Id);
        }
    }
}

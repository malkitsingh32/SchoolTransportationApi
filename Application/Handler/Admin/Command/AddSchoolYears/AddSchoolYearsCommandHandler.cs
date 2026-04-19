using Application.Abstraction.Services;
using DTO.Response;
using MediatR;

namespace Application.Handler.Admin.Command.AddSchoolYears
{
    public class AddSchoolYearsCommandHandler : IRequestHandler<AddSchoolYearsCommand, CommonResultResponseDto<string>>
    {
        private readonly IAdminService _adminService;
        public AddSchoolYearsCommandHandler(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<CommonResultResponseDto<string>> Handle(AddSchoolYearsCommand addSchoolYearsCommand, CancellationToken cancellationToken)
        {
            var user = await _adminService.AddSchoolYears(addSchoolYearsCommand.Id,addSchoolYearsCommand.SchoolYear , addSchoolYearsCommand.NumberOfStudents, addSchoolYearsCommand.UserId);
            return await Task.FromResult(user);
        }
    }
}

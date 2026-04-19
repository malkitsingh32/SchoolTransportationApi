using Application.Abstraction.Services;
using DTO.Response;
using MediatR;

namespace Application.Handler.Admin.Command.AddSchool
{
    public class AddSchoolCommandHandler : IRequestHandler<AddSchoolCommand, CommonResultResponseDto<string>>
    {
        private readonly IAdminService _adminService;
        public AddSchoolCommandHandler(IAdminService adminService)
        {
            _adminService = adminService;
        }
        public async Task<CommonResultResponseDto<string>> Handle(AddSchoolCommand addSchoolCommand, CancellationToken cancellationToken)
        {
            var user = await _adminService.AddSchool(addSchoolCommand.Id, addSchoolCommand.SchoolName, addSchoolCommand.LegalName, addSchoolCommand.UserId);
            return await Task.FromResult(user);
        }
    }
}


using Application.Abstraction.Services;
using DTO.Response;
using MediatR;

namespace Application.Handler.Admin.Command.DeleteSchools
{
    public class DeleteSchoolsCommandHandler : IRequestHandler<DeleteSchoolsCommand, CommonResultResponseDto<string>>
    {
        private readonly IAdminService _adminService;
        public DeleteSchoolsCommandHandler(IAdminService adminService)
        {
            _adminService = adminService;
        }
        public async Task<CommonResultResponseDto<string>> Handle(DeleteSchoolsCommand deleteSchoolsCommand, CancellationToken cancellationToken)
        {
            return await _adminService.DeleteSchools(deleteSchoolsCommand.Id,deleteSchoolsCommand.IsDelete);
        }
    }
}

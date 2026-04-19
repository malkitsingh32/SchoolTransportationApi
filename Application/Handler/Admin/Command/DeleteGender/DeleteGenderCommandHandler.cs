using Application.Abstraction.Services;
using DTO.Response;
using MediatR;

namespace Application.Handler.Admin.Command.DeleteGender
{
    public class DeleteGenderCommandHandler : IRequestHandler<DeleteGenderCommand, CommonResultResponseDto<string>>
    {
        private readonly IAdminService _adminService;
        public DeleteGenderCommandHandler(IAdminService adminService)
        {
            _adminService = adminService;
        }
        public async Task<CommonResultResponseDto<string>> Handle(DeleteGenderCommand deleteGenderCommand, CancellationToken cancellationToken)
        {
            return await _adminService.DeleteGender(deleteGenderCommand.Id, deleteGenderCommand.IsDelete);
        }
    }
}

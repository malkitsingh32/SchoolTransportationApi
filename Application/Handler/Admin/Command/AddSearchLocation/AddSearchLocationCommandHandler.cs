using Application.Abstraction.Services;
using DTO.Response;
using MediatR;

namespace Application.Handler.Admin.Command.AddSearchLocation
{
    public class AddSearchLocationCommandHandler : IRequestHandler<AddSearchLocationCommand, CommonResultResponseDto<string>>
    {
        private readonly IAdminService _adminService;
        public AddSearchLocationCommandHandler(IAdminService adminService)
        {
            _adminService = adminService;
        }
        public async Task<CommonResultResponseDto<string>> Handle(AddSearchLocationCommand addSearchLocationCommand, CancellationToken cancellationToken)
        {
            var user = await _adminService.AddSearchLocation(addSearchLocationCommand.Id, addSearchLocationCommand.CurrentLocation, addSearchLocationCommand.CurrentLocationLongLat, addSearchLocationCommand.UserId);
            return await Task.FromResult(user);
        }
    }
}

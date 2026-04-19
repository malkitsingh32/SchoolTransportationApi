using Application.Abstraction.Services;
using DTO.Response;
using DTO.Response.Admin;
using MediatR;

namespace Application.Handler.Admin.Queries.GetTrackingTime
{
    public class GetTrackingTimeQueryHandler : IRequestHandler<GetTrackingTimeQuery, CommonResultResponseDto<GetTrackingTimeQueryResponseDto>>
    {
        private readonly IAdminService _adminService;

        public GetTrackingTimeQueryHandler(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<CommonResultResponseDto<GetTrackingTimeQueryResponseDto>> Handle(GetTrackingTimeQuery request, CancellationToken cancellationToken)
        {
            return await _adminService.GetTrackingTime();
        }
    }
}

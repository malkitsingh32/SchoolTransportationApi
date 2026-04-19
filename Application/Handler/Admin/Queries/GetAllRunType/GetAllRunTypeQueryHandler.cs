using Application.Abstraction.Services;
using DTO.Response;
using DTO.Response.Admin;
using MediatR;

namespace Application.Handler.Admin.Queries.GetAllRunType
{
    public class GetAllRunTypeQueryHandler : IRequestHandler<GetAllRunTypeQuery, CommonResultResponseDto<IList<GetAllRunTypeResponseDto>>>
    {
        private readonly IAdminService _adminService;

        public GetAllRunTypeQueryHandler(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<CommonResultResponseDto<IList<GetAllRunTypeResponseDto>>> Handle(GetAllRunTypeQuery request, CancellationToken cancellationToken)
        {
            return await _adminService.GetAllRunType();

        }
    }
}

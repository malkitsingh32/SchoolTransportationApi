using Application.Abstraction.Services;
using DTO.Response;
using DTO.Response.Admin;
using MediatR;

namespace Application.Handler.Admin.Queries.GetAllNt
{
    public class GetAllNTQueryHandler : IRequestHandler<GetAllNTQuery, CommonResultResponseDto<IList<GetAllNTResponseDto>>>
    {
        private readonly IAdminService _adminService;

        public GetAllNTQueryHandler(IAdminService adminService)
        {
            _adminService = adminService;
        }
        public async Task<CommonResultResponseDto<IList<GetAllNTResponseDto>>> Handle(GetAllNTQuery request, CancellationToken cancellationToken)
        {
            return await _adminService.GetAllNT();
        }
    }
}

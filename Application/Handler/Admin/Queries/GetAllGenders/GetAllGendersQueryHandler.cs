using Application.Abstraction.Services;
using DTO.Response;
using DTO.Response.Admin;
using MediatR;

namespace Application.Handler.Admin.Queries.GetAllGenders
{
    public class GetAllGendersQueryHandler : IRequestHandler<GetAllGendersQuery, CommonResultResponseDto<IList<GetAllGendersResponseDto>>>
    {
        private readonly IAdminService _adminService;

        public GetAllGendersQueryHandler(IAdminService adminService)
        {
            _adminService = adminService;
        }
        public async Task<CommonResultResponseDto<IList<GetAllGendersResponseDto>>> Handle(GetAllGendersQuery request, CancellationToken cancellationToken)
        {
            return await _adminService.GetAllGenders();
        }
    }
}

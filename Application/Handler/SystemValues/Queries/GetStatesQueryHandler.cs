using Application.Abstraction.Services;
using DTO.Response;
using DTO.Response.SystemValues;
using MediatR;

namespace Application.Handler.SystemValues.Queries
{
    public class GetStatesQueryHandler : IRequestHandler<GetStatesQuery, CommonResultResponseDto<IList<GetStatesResponseDto>>>
    {

        private readonly IAdminService _adminService;

        public GetStatesQueryHandler(IAdminService adminService)
        {
            _adminService = adminService;
        }
        public async Task<CommonResultResponseDto<IList<GetStatesResponseDto>>> Handle(GetStatesQuery getStatesQuery, CancellationToken cancellationToken)
        {
            return await _adminService.GetStates(getStatesQuery.Id);
          
        }
    }
}

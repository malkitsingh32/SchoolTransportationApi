using Application.Abstraction.Services;
using DTO.Response;
using DTO.Response.Streets;
using MediatR;

namespace Application.Handler.Streets.Queries.GetDriversList
{
    public class GetStreetListQueryHandler : IRequestHandler<GetStreetListQuery, CommonResultResponseDto<IList<GetStreetListResponseDto>>>
    {
        private readonly IStreetsService _streetsService;

        public GetStreetListQueryHandler(IStreetsService streetsService)
        {
            _streetsService = streetsService;
        }
        public async Task<CommonResultResponseDto<IList<GetStreetListResponseDto>>> Handle(GetStreetListQuery request, CancellationToken cancellationToken)
        {
            return await _streetsService.GetStreetList();
        }
    }
}

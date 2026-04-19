using Application.Abstraction.Services;
using DTO.Response;
using DTO.Response.Routes;
using Mapster;
using MediatR;

namespace Application.Handler.Routes.Queries.GetRoutesLists
{
    public class GetRoutesListsQueryHandler : IRequestHandler<GetRoutesListsQuery, CommonResultResponseDto<IList<GetRoutesListsResponseDto>>>
    {
        private readonly IRoutesService _routesService;

        public GetRoutesListsQueryHandler(IRoutesService routesService)
        {
            _routesService = routesService;
        }

        public async Task<CommonResultResponseDto<IList<GetRoutesListsResponseDto>>> Handle(GetRoutesListsQuery getRoutesListsQuery, CancellationToken cancellationToken)
        {
            return await _routesService.GetRoutesLists(getRoutesListsQuery.Adapt<GetRoutesListsRequestDto>());
        }
    }

    public class GetRoutesListsRequestDto
    {
        public string SearchText { get; set; }
        public int? DriverId { get; set; }
        public int Type { get; set; }
        public int? SeasonFolderId { get; set; }
        public int? TodaysDriver { get; set; }
        public int? DefaultDriver { get; set; }
        public int? Gender { get; set; }
        public string? School { get; set; }
        public string? Grade { get; set; }
        public string? Role { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

}

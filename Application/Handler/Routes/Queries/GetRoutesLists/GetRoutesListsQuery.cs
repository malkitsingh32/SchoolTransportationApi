using DTO.Response;
using DTO.Response.Routes;
using MediatR;

namespace Application.Handler.Routes.Queries.GetRoutesLists
{
    public class GetRoutesListsQuery : IRequest<CommonResultResponseDto<IList<GetRoutesListsResponseDto>>>
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

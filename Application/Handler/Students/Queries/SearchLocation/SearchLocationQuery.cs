using Application.Common.Dtos;
using DTO.Response;
using MediatR;

namespace Application.Handler.Students.Queries.SearchLocation
{
    public class SearchLocationQuery : IRequest<CommonResultResponseDto<IList<SearchLocationResult>>>
    {
        public string? Area { get; set; }
        public string? School { get; set; }
        public string? Grade { get; set; }
        public int? Gender { get; set; }
        public string? Building { get; set; }
        public string? Branch { get; set; }
        public string? Street { get; set; }
        public string? UniqueId { get; set; }
        public string? Filter { get; set; }
        public int UserId { get; set; }
        public int? SearchCount { get; set; }
    }
}

using Application.Abstraction.Services;
using Application.Common.Dtos;
using DTO.Request.Students;
using DTO.Response;
using Mapster;
using MediatR;

namespace Application.Handler.Students.Queries.SearchLocation
{
    public class SearchLocationQueryHandler : IRequestHandler<SearchLocationQuery, CommonResultResponseDto<IList<SearchLocationResult>>>
    {
        private readonly IStudentsService _studentsService;
        public SearchLocationQueryHandler(IStudentsService studentsService)
        {
            _studentsService = studentsService;
        }

        public async Task<CommonResultResponseDto<IList<SearchLocationResult>>> Handle(SearchLocationQuery searchLocationQuery, CancellationToken cancellationToken)
        {
            return await _studentsService.SearchLocation(searchLocationQuery.Adapt<SearchLocationRequestDto>());

        }
    }
}
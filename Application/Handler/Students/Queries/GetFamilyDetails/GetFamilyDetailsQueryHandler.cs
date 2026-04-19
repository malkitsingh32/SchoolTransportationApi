using Application.Abstraction.Services;
using DTO.Request.Students;
using DTO.Response;
using DTO.Response.Students;
using Mapster;
using MediatR;

namespace Application.Handler.Students.Queries.GetFamilyDetails
{
    public class GetFamilyDetailsQueryHandler : IRequestHandler<GetFamilyDetailsQuery, CommonResultResponseDto<IList<GetFamilyDetailsResponseDto>>>
    {
        private readonly IStudentsService _studentsService;
        public GetFamilyDetailsQueryHandler(IStudentsService studentsService)
        {
            _studentsService = studentsService;
        }

        public async Task<CommonResultResponseDto<IList<GetFamilyDetailsResponseDto>>> Handle(GetFamilyDetailsQuery getFamilyDetailsQuery, CancellationToken cancellationToken)
        {
            return await _studentsService.GetFamilyDetails(getFamilyDetailsQuery.Adapt<GetFamilyDetailsRequestDto>());
        }
    }
}

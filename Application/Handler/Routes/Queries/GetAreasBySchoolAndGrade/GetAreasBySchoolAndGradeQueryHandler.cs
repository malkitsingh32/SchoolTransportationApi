using Application.Abstraction.Services;
using DTO.Request.Routes;
using DTO.Response;
using DTO.Response.Admin;
using Mapster;
using MediatR;

namespace Application.Handler.Routes.Queries.GetAreasBySchoolAndGrade
{
    public class GetAreasBySchoolAndGradeQueryHandler : IRequestHandler<GetAreasBySchoolAndGradeQuery, CommonResultResponseDto<IList<GetAreaListResponseDto>>>
    {
        private readonly IRoutesService _routesService;

        public GetAreasBySchoolAndGradeQueryHandler(IRoutesService routesService)
        {
            _routesService = routesService;
        }

        public async Task<CommonResultResponseDto<IList<GetAreaListResponseDto>>> Handle(GetAreasBySchoolAndGradeQuery getAreasBySchoolAndGradeQuery, CancellationToken cancellationToken)
        {
            return await _routesService.GetAreasBySchoolAndGrade(getAreasBySchoolAndGradeQuery.Adapt<GetAreasBySchoolAndGradeRequestDto>());
        }
    }
}

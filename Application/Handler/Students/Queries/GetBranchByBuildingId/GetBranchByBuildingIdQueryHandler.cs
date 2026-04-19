using Application.Abstraction.Services;
using DTO.Request.Students;
using DTO.Response;
using DTO.Response.Students;
using Mapster;
using MediatR;

namespace Application.Handler.Students.Queries.GetBranchByBuildingId
{
    public class GetBranchByBuildingIdQueryHandler : IRequestHandler<GetBranchByBuildingIdQuery, CommonResultResponseDto<IList<GetBranchByBuildingIdResponseDto>>>
    {
        private readonly IStudentsService _studentsService;
        public GetBranchByBuildingIdQueryHandler(IStudentsService studentsService)
        {
            _studentsService = studentsService;
        }

        public async Task<CommonResultResponseDto<IList<GetBranchByBuildingIdResponseDto>>> Handle(GetBranchByBuildingIdQuery getBranchByBuildingIdQuery, CancellationToken cancellationToken)
        {
            return await _studentsService.GetBranchByBuildingId(getBranchByBuildingIdQuery.Adapt<GetBranchByBuildingIdRequestDto>());
        }
    }
}

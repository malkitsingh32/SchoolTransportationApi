using DTO.Response;
using DTO.Response.Admin;
using MediatR;

namespace Application.Handler.Admin.Queries.GetBuildingList
{
    public class GetBuildingListQuery : IRequest<CommonResultResponseDto<IList<GetBuildingListResponseDto>>>
    {

    }
}

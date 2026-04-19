using DTO.Response;
using DTO.Response.Admin;
using MediatR;

namespace Application.Handler.Admin.Queries.GetAreaList
{
    public class GetAreaListQuery : IRequest<CommonResultResponseDto<IList<GetAreaListResponseDto>>>
    {
    }
}

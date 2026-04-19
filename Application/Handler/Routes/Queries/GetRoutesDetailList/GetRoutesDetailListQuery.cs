using DTO.Response;
using DTO.Response.Routes;
using MediatR;

namespace Application.Handler.Routes.Queries.GetRoutesDetailList
{
    public class GetRoutesDetailListQuery : IRequest<CommonResultResponseDto<IList<GetRoutesDetailListResponseDto>>>
    {
    }
}

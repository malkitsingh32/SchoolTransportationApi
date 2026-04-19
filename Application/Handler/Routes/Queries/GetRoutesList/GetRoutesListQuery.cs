using DTO.Response;
using DTO.Response.Routes;
using MediatR;

namespace Application.Handler.Routes.Queries.GetRoutesList
{
    public class GetRoutesListQuery : IRequest<CommonResultResponseDto<IList<GetRoutesListResponseDto>>>
    {
    }
}

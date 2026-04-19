using DTO.Response;
using MediatR;

namespace Application.Handler.Routes.Queries.InsertNextDayRouteDetails
{
    public class InsertNextDayRouteDetailsQuery : IRequest<CommonResultResponseDto<string>>
    {
    }
}

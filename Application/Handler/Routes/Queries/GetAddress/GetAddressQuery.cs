using DTO.Response;
using DTO.Response.Routes;
using MediatR;

namespace Application.Handler.Routes.Queries.GetAddress
{
    public class GetAddressQuery : IRequest<CommonResultResponseDto<IList<GetAddressResponseDto>>>
    {
    }
}

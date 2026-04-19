using DTO.Response;
using DTO.Response.Bus;
using MediatR;

namespace Application.Handler.Bus.Queries.GetAllBusDetails
{
    public class GetAllBusDetailsQuery : IRequest<CommonResultResponseDto<IList<GetBusesResponseDto>>>
    {

    }
}

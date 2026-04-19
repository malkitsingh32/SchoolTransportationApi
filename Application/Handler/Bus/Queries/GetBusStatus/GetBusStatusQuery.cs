using DTO.Response;
using DTO.Response.BusStatus;
using MediatR;

namespace Application.Handler.Bus.Queries.GetBusStatus
{
    public class GetBusStatusQuery : IRequest<CommonResultResponseDto<IList<GetBusStatusResponseDto>>>
    {
    }
}

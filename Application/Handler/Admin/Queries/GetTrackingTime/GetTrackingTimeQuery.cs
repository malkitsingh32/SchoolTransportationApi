using DTO.Response;
using DTO.Response.Admin;
using MediatR;

namespace Application.Handler.Admin.Queries.GetTrackingTime
{
    public class GetTrackingTimeQuery : IRequest<CommonResultResponseDto<GetTrackingTimeQueryResponseDto>>
    {
    }
}

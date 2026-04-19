using DTO.Response;
using DTO.Response.Driver;
using MediatR;

namespace Application.Handler.Drivers.Queries
{
    public class GetDriversListQuery : IRequest<CommonResultResponseDto<IList<GetDriversListResponseDto>>>
    {

    }
}

using DTO.Response;
using DTO.Response.Streets;
using MediatR;

namespace Application.Handler.Streets.Queries.GetDriversList
{
    public class GetStreetListQuery : IRequest<CommonResultResponseDto<IList<GetStreetListResponseDto>>>
    {

    }
}
